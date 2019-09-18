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

        Task<IEnumerable<Activity>> GetActivitiesByChannelId(string channelId);

        Task<Redeemable> GetRedeemableByRecieverId(string userId);
        Task<List<User>> GetLeaderboard(LeaderboardSearchRequest request);
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

        private IEnumerable<SlackAPI.User> getUsers(IEnumerable<Bevan> bevanList)
        {
            List<SlackAPI.User> Users;

            var slackMsg = new SlackMessage(new DynamoRepository());

            IEnumerable<string> ids = bevanList.Select(b => b.ReceiverId);
            ids.Concat(bevanList.Select(b => b.GiverId));

            Users = slackMsg.GetUsers(ids.Distinct().ToList());
            return Users;
        }

        public async Task<List<User>> GetLeaderboard(LeaderboardSearchRequest request)
        {
            var bevanList = await GetBevanList();

            //Filter on channel
            bevanList = bevanList
                .Where(b => b.Channel.Equals(request.Channel))
                .Where(b => b.Timestamp >= request.StartDate && b.Timestamp <= request.EndDate);

            var Users = getUsers(bevanList);

            var userRet = new List<User>();
            foreach (var bevanData in Users)
            {
                var name = Users.FirstOrDefault(u => u.id.Equals(bevanData.id))?.name;
                var userImg = Users.FirstOrDefault(u => u.id.Equals(bevanData.id))?.profile.image_72;

                var sumOfBevans = request.Type == 0
                    ? bevanList.Where(b => b.ReceiverId.Equals(bevanData.id)).Sum(a => a.Count)
                    : bevanList.Where(b => b.GiverId.Equals(bevanData.id)).Sum(a => a.Count);
                
                var user = new User
                {
                    Name = name,
                    TotalBevans = sumOfBevans,
                    UserImage = userImg
                };
                
                userRet.Add(user);
            }

            return userRet.OrderByDescending(x => x.TotalBevans).ToList();
        }

        public async Task<IEnumerable<Activity>> GetActivitiesByChannelId(string channelId)
        {
            var bevanList = await GetBevanList();
            bevanList = bevanList.Where(b => b.Channel.Equals(channelId));
            var Users = getUsers(bevanList);

            var activities = new List<Activity>();
            foreach (var activityDetails in bevanList)
            {
                var receiver = Users.FirstOrDefault(u => u.id.Equals(activityDetails.ReceiverId));
                var giver = Users.FirstOrDefault(u => u.id.Equals(activityDetails.GiverId));

                var receiverName = receiver?.name;
                var receiverImg = receiver?.profile.image_72;

                var giverName = giver?.name;
                var giverImg = giver?.profile.image_72;

                var activity = new Activity
                {
                    ReceiverId = activityDetails.ReceiverId,
                    GiverId = activityDetails.GiverId,
                    Channel = activityDetails.Channel,
                    Timestamp = activityDetails.Timestamp,
                    Count = activityDetails.Count,
                    Message = activityDetails.Message,
                    GiverName =  giverName,
                    ReceiverName = receiverName,
                    ReceiverImage = receiverImg,
                    GiverImage = giverImg
                };

                activities.Add(activity);
            }

            return activities;
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