using System;
using System.Collections.Generic;
using System.Linq;
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

        Task<IEnumerable<Bevan>> GetBevansByChannel(string channelId);
    }
    
    public class DynamoRepository: IDynamoRepository
    {
        public async Task<List<string>> GetChannels()
        {
            List<string> channels;
            using (var client = new AmazonDynamoDBClient())
            {
                var response = await client.ScanAsync(new ScanRequest("hey-bevan-table-new-dev"));

                var responseItems = response.Items;
                channels = responseItems.Select(i => i["channel"].S).Distinct().ToList();

            }
            return channels;
        }
        
        private async Task<IEnumerable<Bevan>> GetBevanList()
        {
            using (var client = new AmazonDynamoDBClient())
            {
                var response = await client.ScanAsync(new ScanRequest("hey-bevan-table-new-dev"));

                return response.Items.Select(i => new Bevan
                {
                    BevanId = i["bevanId"].S,
                    ReceiverId = i["receiverId"].S,
                    Count = int.Parse(i["count"].N),
                    Message = i["message"].S,
                    GiverId = i["giverId"].S,
                    Channel = i["channel"].S,
                    Timestamp = DateTime.Parse(i["timestamp"].S)
                });
            }
        }

        public async Task<IEnumerable<Bevan>> GetBevansByChannel(string channelId)
        {
            return (await GetBevanList()).Where(b => b.Channel.Equals(channelId));
//            using (var client = new AmazonDynamoDBClient())
//            {
//                var response = await client.ScanAsync(new ScanRequest("hey-bevan-table-new-dev"));
//
//                return response.Items.Where(k => k["channel"].S.Equals(channelId)).Select(i => new Bevan
//                {
//                    BevanId = i["bevanId"].S,
//                    ReceiverId = i["receiverId"].S,
//                    Count = int.Parse(i["count"].N),
//                    Message = i["message"].S,
//                    GiverId = i["giverId"].S,
//                    Channel = i["channel"].S,
//                    Timestamp = DateTime.Parse(i["timestamp"].S)
//                });
//            }
        }

        public async Task SaveBevan(Bevan bevan)
        {
            using (var client = new AmazonDynamoDBClient())
            {
                var table = Table.LoadTable(client, "hey-bevan-table-new-dev");
                
                var book = new Document
                {
                    ["bevanId"] = bevan.BevanId,
                    ["receiverId"] = bevan.ReceiverId,
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