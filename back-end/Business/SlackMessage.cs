using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using AwsDotnetCsharp.Infrastructure;
using AwsDotnetCsharp.Infrastructure.Configs;
using AwsDotnetCsharp.Models;
using AwsDotnetCsharp.Repository;

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
                string whoReceived = theMessage.Split('<', '>')[1]; //only get first user mentioned in the message

                Console.WriteLine("{0} gave \"{1}\" emojis to {2}", whoSent, noOfEmojis, whoReceived);

                // do dynamo db inserts
                bevan = new Bevan
                {
                    UserId = whoReceived,
                    Count = noOfEmojis,
                    Message = theMessage,
                    Channel = @event.Channel,
                    GiverId = whoSent
                };

                await _dynamoRepository.SaveBevan(bevan);
            }
            
            //do nothing
            return bevan;
        }

    }
}
