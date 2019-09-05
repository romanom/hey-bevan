using System;
using AwsDotnetCsharp.Models;

namespace AwsDotnetCsharp.Business.SlackMessage
{

    public class SlackMessage
    {

        const string emoji = ":bevan:";

        internal static void PostMessage(Event @event)
        {

            Console.WriteLine("User: {0} Message: \"{1}\"", @event.User, @event.Text);

            if (@event.Text.Contains(emoji))
            {
                //do someting
                var noOfEmojis = @event.Text.Split(emoji).Length - 1;
                Console.WriteLine("{0} gave \"{1}\" emojis to someone...", @event.User, noOfEmojis);

            }

            //do nothing
        }

    }
}
