using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AwsDotnetCsharp.Models;
using AwsDotnetCsharp.Repository;
using SlackAPI;

namespace AwsDotnetCsharp.Business.SlackMessage
{
    public class SlackMessage
    {

        const string emoji = ":bevan:"; //TODO move to config
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

                var noOfEmojis = theMessage.Split(emoji).Length - 1;
                string whoSent = @event.User; //who posted the message

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

                await sendDM(whoSent, string.Format("You gave <@{0}> {1} {2}'s", whoReceived, noOfEmojis, emoji));

            }

            //do nothing
            return bevan;
        }

        private async Task sendDM(string whoSent, string message)
        {

            var token = Environment.GetEnvironmentVariable("SLACK_ACCESS_TOKEN");         
            if (token == null){
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
