using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using AwsDotnetCsharp.Business.SlackMessage;
using AwsDotnetCsharp.Infrastructure;
using AwsDotnetCsharp.Infrastructure.Configs;
using AwsDotnetCsharp.Models;
using Newtonsoft.Json;

namespace AwsDotnetCsharp.Repository
{
    public interface IDynamoRepository
    {
        Task SaveBevan(Bevan bevan);
        Task<List<ChannelRec>> GetChannels();

        Task<IEnumerable<Bevan>> GetBevansByChannel(string channelId);

        Task<Redeemable> GetRedeemableByRecieverId(string userId);
        Task<List<User>> GetLeaderboard();
    }

    public class ChannelRec
    {
        public string Channel { get; set; }
        public string ChannelName { get; set; }
    }
    
    public class DynamoRepository: IDynamoRepository
    {
        public async Task<List<ChannelRec>> GetChannels()
        {
            List<string> channels;
            using (var client = new AmazonDynamoDBClient())
            {
                var response = await client.ScanAsync(new ScanRequest("hey-bevan-table-new-dev"));

                var responseItems = response.Items;
                channels = responseItems.Select(i => i["channel"].S).Distinct().ToList();


                
                var slackMsg = new SlackMessage(new DynamoRepository());

                return slackMsg.GetChannels(channels).Select(c => new ChannelRec
                {
                    Channel = c.id,
                    ChannelName = c.name
                }).ToList();
            }
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

        public async Task<List<User>> GetLeaderboard()
        {
//            var bevanList = (await GetBevanList()).Where(a => a.Timestamp >= startDate && a.Timestamp <= endDate);
            var bevanList = await GetBevanList();
            var userRet = new List<User>();
            List<SlackAPI.User> Users;

            var slackMsg = new SlackMessage(new DynamoRepository());

            var ids = bevanList.Select(b => b.ReceiverId).Distinct();
            
            Users = slackMsg.GetUsers(ids.ToList()) ;
            
            foreach (var bevanData in bevanList)
            {
                var userImg = Users.FirstOrDefault(u => u.id.Equals(bevanData.ReceiverId))?.profile.image_72;
                
                var sumOfBevans = bevanList.Where(b => b.ReceiverId.Equals(bevanData.ReceiverId)).Sum(a => a.Count);
                var user = new User
                {
                    Name = bevanData.BevanId,
                    TotalBevans = sumOfBevans,
                    UserImage = userImg
                };
                
                userRet.Add(user);
            }

            return userRet;
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

        public async Task<Redeemable> GetRedeemableByRecieverId(string userId)
        {
            var bevans = (await GetBevanList()).Where(b => b.ReceiverId.Equals(userId));

            return bevans.GroupBy(g => g.ReceiverId)
                .Select(s => new Redeemable
                {
                    ReceiverId = s.Key,
                    TotalCount = s.Sum(i => i.Count)
                }).FirstOrDefault();
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