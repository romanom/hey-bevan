using Amazon.DynamoDBv2.DataModel;

namespace AwsDotnetCsharp.Models
{
    public class Bevan
    {
        [DynamoDBHashKey]
        public string UserId { get; set; }
        [DynamoDBProperty]
        public int Count { get; set; }
        [DynamoDBProperty]
        public string Message { get; set; }
        
        [DynamoDBProperty] 
        public string GiverId { get; set; }
        
        [DynamoDBProperty] 
        public string Channel { get; set; }
    }
}