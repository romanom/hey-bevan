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
//        public async Task SaveBevan(Bevan bevan)
//        {
//            using (var context = new DynamoDBContext(_client))
//            {
//                await context.SaveAsync(bevan, _configuration);
//            }
//        }
        
        public async Task SaveBevan(Bevan bevan)
        {
            using (var client = new AmazonDynamoDBClient())
            {
                var table = Table.LoadTable(client, "hey-bevan-table-dev");

                var book = new Document
                {
                    ["userId"] = bevan.UserId, 
                    ["count"] = bevan.Count, 
                    ["message"] = bevan.Message
                };

                await table.PutItemAsync(book);                
            }
        } 
    }
}