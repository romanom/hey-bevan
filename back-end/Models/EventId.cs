using System;
using System.Threading;
using Amazon.DynamoDBv2.DataModel;

namespace AwsDotnetCsharp.Models
{

    public class EventId
    {
        public string id { get; set; }
        public int count { get; set; }
    }

}
