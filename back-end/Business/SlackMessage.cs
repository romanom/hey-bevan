using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Util;
using AwsDotnetCsharp.Models;
using AwsDotnetCsharp.Repository;
using SlackAPI;

namespace AwsDotnetCsharp.Business.SlackMessage
{
    public class SlackMessage
    {

        const string emoji = ":bevan:"; //TODO move to config
        const decimal dailyLimit = 5; //TODO move to config
        private IDynamoRepository _dynamoRepository;

        public SlackMessage(IDynamoRepository dynamoRepository)
        {
            _dynamoRepository = dynamoRepository;
        }

        internal async Task<Bevan> ProcessMessage(Event @event)
        {
            var theMessage = @event.Text;
            var bevan = new Bevan();
            Console.WriteLine(" theMessage ", theMessage + " ~~~~~~~~~~ " + @event.Text);
            if (string.IsNullOrEmpty(theMessage)) {
                Console.WriteLine("Message is empty , exiting");
                return bevan;
            }
            //TODO allow multiple users
            var regexValue = Regex.Match(theMessage, @"<@(.+?)>");
            Console.WriteLine("regexValue ", regexValue);
            var whoReceived = regexValue.Groups[1].Value;
            Console.WriteLine(" whoReceived ", whoReceived);

            theMessage = theMessage.Replace("<@" + whoReceived + ">", "");
            Console.WriteLine("Event message received from slack " + theMessage + " " + @event.Channel + " " + @event.User);

            if (theMessage.Contains(emoji) && !string.IsNullOrEmpty(whoReceived))
            {

                string whoSent = @event.User; //who posted the message

                var sentToday = await getNoSentToday(whoSent);
                var noOfEmojis = theMessage.Split(emoji).Length - 1;

                //Sorry, you can only give tacos to other people on your team.
                if(whoReceived == whoSent) {
                    var selfMessage = string.Format("Sorry, you can only give {0}'s to other people on your team.", emoji);
                    await sendDM(whoSent, selfMessage);
                    return bevan;
                }
                Console.WriteLine("No of emojis received "+ noOfEmojis.ToString());
                Console.WriteLine("Total emojis sent today " + sentToday.ToString());
                Console.WriteLine("Daily limit " + dailyLimit.ToString());

                //check how many they've sent today
                //5 >= 5 || (5 - 1) >= 5)
                if (sentToday >= dailyLimit || (sentToday + noOfEmojis) > dailyLimit)
                {
                    //send daily limit message  
                    Console.WriteLine("{0} >= {1} || ({0} - {2}) >= {1}) ", sentToday, dailyLimit, noOfEmojis);
                  
                    var dailyLimitMessage = string.Format("Whoops! You tried to give {0} {2}'s. You have {1} {2}'s left to give today.", noOfEmojis, (dailyLimit - sentToday), emoji);
                    await sendDM(whoSent, dailyLimitMessage);
                    return bevan;
                }
                Console.WriteLine("{0} gave \"{1}\" emojis to {2}", whoSent, noOfEmojis, whoReceived);

                // do dynamo db inserts
                bevan = new Bevan
                {
                    BevanId = Guid.NewGuid().ToString(),
                    ReceiverId = whoReceived,
                    Count = noOfEmojis,
                    Message = theMessage,
                    Channel = @event.Channel,
                    GiverId = whoSent,
                    Timestamp = DateTime.UtcNow
                };

                await _dynamoRepository.SaveBevan(bevan);

                //You received 1 taco from @ivan in #cr-hyperion.
                //>@bevan :taco: for the awesome pitch!
                var receiverDM = string.Format("You received {0} {1}'s from <@{2}>.\n>{3}", noOfEmojis, emoji, whoSent, theMessage);
                await sendDM(whoReceived, receiverDM);

                Console.WriteLine(receiverDM);

                //@jp received 3 tacos from you. You have 2 tacos left to give out today. 
                var giverDM = string.Format("<@{0}> received {1} {2}'s from you. You have {3} {2}'s left to give out today.", whoReceived, noOfEmojis, emoji, dailyLimit - (noOfEmojis + sentToday));
                await sendDM(whoSent, giverDM);

                Console.WriteLine(giverDM);

            }

            //do nothing
            return bevan;
        }

        private async Task<decimal> getNoSentToday(string giverId)
        {
            decimal count = 0;
            using (var client = new AmazonDynamoDBClient())
            {
                var response = await client.ScanAsync(new ScanRequest("hey-bevan-table-new-dev"));
                var responseItems = response.Items;
                var usersItems = responseItems.Where(x => x["giverId"].S.Equals(giverId));
                var todays = usersItems.Where(x => DateTime.Parse(x["timestamp"].S) >= DateTime.Today && DateTime.Parse(x["timestamp"].S) <= DateTime.Today.AddDays(1));
                var counter = todays.Select(x => decimal.Parse(x["count"].N)).ToList();
                count = counter.Sum();
            }
            return count;
        }

        private async Task sendDM(string whoSent, string message)
        {

            var token = Environment.GetEnvironmentVariable("SLACK_ACCESS_TOKEN");
            if (token == null)
            {
                throw new Exception("Error getting slack token from ssm");
            }

            var client = new SlackTaskClient(token);          

            var response = await client.PostMessageAsync(whoSent, message, null, null, false, null, null, false, null, null, true);


            // process response from API call
            if (response.ok)
            {
                Console.WriteLine("Message sent successfully");
            }
            else
            {
                Console.WriteLine("Message sending failed. error: " + response.error);
            }
        }
    
        internal List<SlackAPI.User> GetUsers(List<string> userIds) {

            var token = Environment.GetEnvironmentVariable("SLACK_ACCESS_TOKEN");
            if (token == null)
            {
                throw new Exception("Error getting slack token from ssm");
            }

            ManualResetEventSlim clientReady = new ManualResetEventSlim(false);
            SlackSocketClient client = new SlackSocketClient(token);
            client.Connect((connected) =>{
                // This is called once the client has emitted the RTM start command
                clientReady.Set();
            }, () =>{
                // This is called once the RTM client has connected to the end point
            });
            // client.OnMessageReceived += (message) =>
            // {
            //     // Handle each message as you receive them
            // };
            clientReady.Wait();

            client.GetUserList((ulr) => { Console.WriteLine("got users"); });

            var userList = new List<SlackAPI.User>();
            foreach(var u in userIds) {
                userList.Add(client.Users.Find(x => x.id.Equals(u)));
            }

            // Release the socket.  
            client.CloseSocket(); 

            return userList; 
            
        }

        internal List<AwsDotnetCsharp.Models.Channel> GetChannels(List<string> channelIds) {

            var token = Environment.GetEnvironmentVariable("SLACK_ACCESS_TOKEN");
            if (token == null)
            {
                throw new Exception("Error getting slack token from ssm");
            }

            ManualResetEventSlim clientReady = new ManualResetEventSlim(false);
            SlackSocketClient client = new SlackSocketClient(token);
            client.Connect((connected) =>{
                // This is called once the client has emitted the RTM start command
                clientReady.Set();
            }, () =>{
                // This is called once the RTM client has connected to the end point
            });
            // client.OnMessageReceived += (message) =>
            // {
            //     // Handle each message as you receive them
            // };
            clientReady.Wait();

            client.GetChannelList((ulr) => { Console.WriteLine("got channels"); });

            var channelList = new List<AwsDotnetCsharp.Models.Channel>();
            foreach(var u in channelIds) {
                var slackChannels = client.Channels.Find(x => x.id.Equals(u));
                channelList.Add(new Models.Channel {
                    id = slackChannels.id,
                    name = slackChannels.name
                });
            }

            // Release the socket.  
            client.CloseSocket(); 

            return channelList; 
            
        }
    }
}
