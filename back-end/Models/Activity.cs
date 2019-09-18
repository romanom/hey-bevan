using System;
using System.Threading;
using Amazon.DynamoDBv2.DataModel;

namespace AwsDotnetCsharp.Models
{

    public class Activity
    {
        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }

        public int Count { get; set; }
        public string Message { get; set; }

        public string GiverId { get; set; }
        public string GiverName { get; set; }

        public string Channel { get; set; }

        public string ReceiverImage { get; set; }
        public string GiverImage { get; set; }

        public DateTime Timestamp { get; set; }
    }

}
