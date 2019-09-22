using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DocumentModel;
using AwsDotnetCsharp.Business.SlackMessage;
using AwsDotnetCsharp.Models;

namespace AwsDotnetCsharp.Repository
{
    public interface IDynamoRepository
    {
        Task SaveBevan(Bevan bevan);
        Task<List<AwsDotnetCsharp.Models.Channel>> GetChannels();

        Task<IEnumerable<Activity>> GetActivitiesByChannelId(string channelId);

        Task<Redeemable> GetRedeemableByRecieverId(string userId);
        Task<List<User>> GetLeaderboard(LeaderboardSearchRequest request);
    }

    public class DynamoRepository: IDynamoRepository
    {
        public async Task<List<AwsDotnetCsharp.Models.Channel>> GetChannels()
        {
            using (var client = new AmazonDynamoDBClient())
            {
                var response = await client.ScanAsync(new ScanRequest("channel"));

                return response?.Items?.Select(i => new Channel
                {
                    guid = i["guid"].S,
                    id = i["id"].S,
                    name = i["name"].S
                }).Distinct().ToList();                    
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

        private async Task<IEnumerable<AwsDotnetCsharp.Models.User>> getUsers()
        {
  /*          var bevanList = await GetBevanList();
            List<string> bevanUsers = new List<string>();
            List<SlackAPI.User> Users;

            bevanList.ToList().Select(t => {
                bevanUsers.Add(t.ReceiverId);
                bevanUsers.Add(t.GiverId);
            }).Distinct();

            var slackMsg = new SlackMessage(new DynamoRepository());
            Users = slackMsg.GetUsers(ids.Distinct().ToList());
            Console.WriteLine();
*/
            using (var client = new AmazonDynamoDBClient())
            {
                var response = await client.ScanAsync(new ScanRequest("user"));

                return response?.Items?.Select(i => new User
                {
                    guid = i["guid"].S,
                    id = i["id"].S,
                    name = i["name"].S,
                    userimage = i["userimage"].S,
                    totalbevans = 0
                }).Distinct().ToList();
            }
        }

        public async Task<List<User>> GetLeaderboard(LeaderboardSearchRequest request)
        {
            var bevanList = await GetBevanList();
            Console.WriteLine("Non-filtered Bevan list count " + bevanList.Count());

            //Filter on channel
            bevanList = bevanList
                .Where(b => b.Channel.Equals(request.Channel))
                .Where(b => b.Timestamp >= request.StartDate && b.Timestamp <= request.EndDate);

            Console.WriteLine("Filtered Bevan list count " + bevanList.Count());
            var Users = await getUsers();

            var userRet = new List<User>();
            foreach (var bevanData in bevanList)
            {
                var receiverUser = Users.FirstOrDefault(u => u.id.Equals(bevanData.ReceiverId));
                var giverUser = Users.FirstOrDefault(u => u.id.Equals(bevanData.GiverId));

                //sum of bevans of receiver user
                var sumOfBevans = request.Type == 0
                    ? bevanList.Where(b => b.ReceiverId.Equals(receiverUser?.id)).Sum(a => a.Count)
                    : bevanList.Where(b => b.GiverId.Equals(receiverUser?.id)).Sum(a => a.Count);

                if (receiverUser != null)
                {
                    receiverUser.totalbevans = sumOfBevans;
                    userRet.Add(receiverUser);
                }
                
                //sum of bevans of giver user
                sumOfBevans = request.Type == 0
                    ? bevanList.Where(b => b.ReceiverId.Equals(giverUser?.id)).Sum(a => a.Count)
                    : bevanList.Where(b => b.GiverId.Equals(giverUser?.id)).Sum(a => a.Count);

                if (giverUser != null)
                {
                    giverUser.totalbevans = sumOfBevans;
                    userRet.Add(giverUser);
                }
            }
            Console.WriteLine("Leaderboard users count" + userRet.Count());
            return userRet.Distinct().OrderByDescending(x => x.totalbevans).ToList();
        }

        public async Task<IEnumerable<Activity>> GetActivitiesByChannelId(string channelId)
        {
            var bevanList = await GetBevanList();
            bevanList = bevanList.Where(b => b.Channel.Equals(channelId));
            var Users = await getUsers();

            var activities = new List<Activity>();
            foreach (var activityDetails in bevanList)
            {
                var receiver = Users.FirstOrDefault(u => u.id.Equals(activityDetails.ReceiverId));
                var giver = Users.FirstOrDefault(u => u.id.Equals(activityDetails.GiverId));

                var activity = new Activity
                {
                    ReceiverId = activityDetails.ReceiverId,
                    GiverId = activityDetails.GiverId,
                    Channel = activityDetails.Channel,
                    Timestamp = activityDetails.Timestamp,
                    Count = activityDetails.Count,
                    Message = activityDetails.Message,
                    GiverName =  giver?.name,
                    ReceiverName = receiver?.name,
                    ReceiverImage = receiver?.userimage,
                    GiverImage = giver?.userimage
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

        private async Task SaveUser(string receiverId,string giverId)
        {
            Console.WriteLine("Save User started for " + receiverId + "  " + giverId);
            var client = new AmazonDynamoDBClient();
            var response = await client.ScanAsync(new ScanRequest("user"));
            List<string> userList = new List<string>();

            //Add receiver id
            var userFromDb = response.Items?.Find(i => (i["id"].S == receiverId));
            if ((userFromDb == null) || userFromDb?.ToList().Count <= 0)
                userList.Add(receiverId);
            
            //Add giver id 
            userFromDb = response.Items?.Find(i => (i["id"].S == giverId));
            if ((userFromDb == null) || userFromDb?.ToList().Count <= 0)
                userList.Add(giverId);

            if (userList.Count > 0 )
            {
                //get user name from slack
                var slackMsg = new SlackMessage(new DynamoRepository());
                var slackUsers = slackMsg.GetUsers(userList).Distinct().ToList();
                List<AwsDotnetCsharp.Models.User> users = new List<AwsDotnetCsharp.Models.User>();
                foreach (var slackUser in slackUsers)
                {
                    Console.WriteLine("Adding new user to db : Name " + slackUser.name);
                    var newUser = new Document
                    {
                        ["guid"] = Guid.NewGuid().ToString(),
                        ["id"] = slackUser.id,
                        ["name"] = slackUser.name,
                        ["userimage"] = slackUser.profile.image_72,
                        ["totalbevans"] = 0
                    };
                    var table = Table.LoadTable(client, "user");
                    await table.PutItemAsync(newUser);
                    Console.WriteLine("User saved");
                };
            }
        }

        private async Task SaveChannel(string id)
        {
            Console.WriteLine("Save Channel started for " + id);
            var client = new AmazonDynamoDBClient();
            var response = await client.ScanAsync(new ScanRequest("channel"));
            var channelFromDb = response.Items?.Find(i => (i["id"].S == id));

            if (channelFromDb == null)
            {
                //get channel name from slack
                var slackMsg = new SlackMessage(new DynamoRepository());
                List<AwsDotnetCsharp.Models.Channel> channels = slackMsg.GetChannels(new List<string>() { id }).ToList();
 
                //add channel to channel table
                if (channels?.Count > 0)
                {
                    Console.WriteLine("Adding new channel to db : Name " + channels[0]?.name);
                    var newChannel = new Document
                    {
                        ["guid"] = Guid.NewGuid().ToString(),
                        ["id"] = id,
                        ["name"] = channels[0]?.name
                    };
                    var table = Table.LoadTable(client, "channel");
                    await table.PutItemAsync(newChannel);
                    Console.WriteLine("Channel saved");
                }
            }
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
                Console.WriteLine("Bevan saved");
                await SaveChannel(bevan.Channel);
                await SaveUser(bevan.ReceiverId, bevan.GiverId);
            }
        } 
    }
}