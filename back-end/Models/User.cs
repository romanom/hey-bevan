using Amazon.DynamoDBv2.DataModel;

namespace AwsDotnetCsharp.Models
{
    public class User
    {
        [DynamoDBHashKey]
        public string guid { get; set; }
        [DynamoDBProperty]
        public string id { get; set; }
        [DynamoDBProperty]
        public string name { get; set; }
        [DynamoDBProperty]
        public string userimage { get; set; }
        
        public int totalbevans  { get; set; }
    }
}