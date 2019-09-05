using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using AwsDotnetCsharp.Infrastructure;
using AwsDotnetCsharp.Infrastructure.Configs;
using AwsDotnetCsharp.Models;
using Newtonsoft.Json;

namespace AwsDotnetCsharp.Repository
{
    public interface IDynamoRepository
    {
        Task SaveBevan(Bevan bevan);
        Task<List<string>> GetChannels();
    }
    
    public class DynamoRepository: IDynamoRepository
    {
        private readonly AmazonDynamoDBClient _client;
        private readonly DynamoDBOperationConfig _configuration;
        
        public DynamoRepository(DynamoDbConfiguration configuration,
            IAwsClientFactory<AmazonDynamoDBClient> clientFactory)
        {
            _client = clientFactory.GetAwsClient();
            _configuration = new DynamoDBOperationConfig
            {
                OverrideTableName = configuration.TableName,
                SkipVersionCheck = true
            };
        }

        public async Task<List<string>> GetChannels()
        {
            var list = new List<string>();
            /*using (var client = new AmazonDynamoDBClient())
            {
                var request = new QueryRequest
                {
                    TableName = "hey-bevan-table-dev",
                    KeyConditionExpression = "Id = :v_Id",
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                        {":v_Id", new AttributeValue { S =  "Amazon DynamoDB#DynamoDB Thread 1" }}}
                };
                
                var response = client.Query(request);

                foreach (Dictionary<string, AttributeValue> item in response.Items)
                {
                    // Process the result.
                    list.Add(item);
                }
            }*/

            return list;
        }
        
        public async Task SaveBevan(Bevan bevan)
        {
            using (var client = new AmazonDynamoDBClient())
            {
                var table = Table.LoadTable(client, "hey-bevan-table-dev");
                
                var book = new Document
                {
                    ["userId"] = bevan.UserId, 
                    ["count"] = bevan.Count, 
                    ["message"] = bevan.Message,
                    ["giverId"] = bevan.GiverId,
                    ["channel"] = bevan.Channel,
                    ["timestamp"] = bevan.Timestamp
                };

                await table.PutItemAsync(book);                
            }
        } 
    }
}