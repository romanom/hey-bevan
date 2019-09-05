using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

            if (theMessage.Contains(emoji))
            {

                string whoSent = @event.User; //who posted the message

                var sentToday = await getNoSentToday(whoSent);
                var noOfEmojis = theMessage.Split(emoji).Length - 1;

                //check how many they've sent today
                if (sentToday >= dailyLimit || (sentToday + noOfEmojis) >= dailyLimit) {
                    //send daily limit message                    
                    var dailyLimitMessage = string.Format("Whoops! You tried to give {0} tacos. You have {1} tacos left to give today.", noOfEmojis, dailyLimit-noOfEmojis);
                    await sendDM(whoSent, dailyLimitMessage);    
                    return bevan;            
                }

                //TODO allow multiple users
                var whoReceived = Regex.Match(theMessage, @"<@(.+?)>").Groups[1].Value;

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
                var receiverDM = string.Format("You received {0} {1}'s from <@{2}>. >{3}", noOfEmojis, emoji, whoSent, theMessage);
                await sendDM(whoReceived, receiverDM);

                Console.WriteLine(receiverDM);

                //@jp received 3 tacos from you. You have 2 tacos left to give out today. 
                var giverDM = string.Format("<@{0}> received {1} {2}'s from you. You have {3} tacos left to give out today.", whoReceived, noOfEmojis, emoji, dailyLimit-noOfEmojis);
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
                var usersItems =  responseItems.Where(x=>x["giverId"].S.Equals(giverId));
                var todays = usersItems.Where(x=> DateTime.Parse(x["timestamp"].S) >= DateTime.Today && DateTime.Parse(x["timestamp"].S) <= DateTime.Today.AddDays(1));
                var counter = todays.Select(x=> decimal.Parse(x["count"].N)).ToList();
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
    }
}
