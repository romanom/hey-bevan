using Amazon.DynamoDBv2.DataModel;

namespace AwsDotnetCsharp.Models
{
    public class Channel
    {
        [DynamoDBHashKey]
        public string guid { get; set; }

        [DynamoDBProperty]
        public string id { get; set; }

        [DynamoDBProperty]
        public string name { get; set; }
    }

}
