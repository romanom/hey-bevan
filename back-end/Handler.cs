using Amazon.Lambda.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.APIGatewayEvents;
using AwsDotnetCsharp.Infrastructure;
using AwsDotnetCsharp.Infrastructure.Configs;
using AwsDotnetCsharp.Models;
using AwsDotnetCsharp.Repository;
using Newtonsoft.Json;
using AwsDotnetCsharp.Business.SlackMessage;
using Amazon.Runtime.Internal.Transform;
using AwsDotnetCsharp.Service;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AwsDotnetCsharp
{
    public class Handler
    {

        private readonly IDynamoRepository _dynamoRepository;
        private readonly ISlackService _slackService;
        private readonly SlackMessage _slackMessage;
        private List<EventId> _eventIdList;
        public Handler()
        {
            _slackService = new SlackService();
            _dynamoRepository = new DynamoRepository();
            _slackMessage = new SlackMessage(_dynamoRepository); //hacky...
            _eventIdList = new List<EventId>();
    }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public APIGatewayProxyResponse AddBevan(APIGatewayProxyRequest request)
        {
            return new APIGatewayProxyResponse
            {
                Headers = GetCorsHeaders(),
                StatusCode = 200
            };
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Challenge(APIGatewayProxyRequest request)
        {
            var requestModel = JsonConvert.DeserializeObject<SlackRequest>(request.Body);

            return await HandleRequest(requestModel);
        }

        private async Task<APIGatewayProxyResponse> HandleRequest(SlackRequest request)
        {
            Console.WriteLine("HandleRequest invoked with Type " + request.Type);
            Console.WriteLine("HandleRequest invoked with Event text " + request.Event.Text);
            Console.WriteLine("HandleRequest invoked with Event_id " + request.Event_Id);

            switch (request.Type)
            {
                case "url_verification":
                    return new APIGatewayProxyResponse
                    {
                        Headers = GetCorsHeaders(),
                        StatusCode = 200,
                        Body = JsonConvert.SerializeObject(new
                        {
                            Challenge = request.Challenge
                        })
                    };
                case "event_callback":
                    EventId existingId = _eventIdList.Find(i => i.id == request.Event_Id);
                    Console.WriteLine("event_callback Existing in the list " + "  " + existingId?.id + "  ", existingId?.count);
                    if (existingId == null)
                    {
                        Console.WriteLine("Adding new event to list and processing the message ");
                        _eventIdList.Add(new EventId
                        {
                            id = request.Event_Id,
                            count = 1
                        });

                        await _slackMessage.ProcessMessage(request.Event);
                    }
                    else if (existingId.count > 5)
                        _eventIdList.Remove(existingId);
                    else
                    {
                        existingId.count = existingId.count + 1;
                    }

                    return new APIGatewayProxyResponse
                    {
                        Headers = GetCorsHeaders(),
                        StatusCode = 200,
                        Body = JsonConvert.SerializeObject(new
                        {
                            Message = request.Event.Text
                        })
                    };
                default:
                    return new APIGatewayProxyResponse
                    {
                        Headers = GetCorsHeaders(),
                        StatusCode = 200,
                        Body = JsonConvert.SerializeObject(new
                        {
                            Error = "unhandled"
                        })
                    };
            }
        }
        
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> tokenExchangeRedirection(APIGatewayProxyRequest request)
        {
            var code = request.QueryStringParameters["code"];

            var something = await _slackService.DoAuth(code);

            var redirectUrl = $"https://ui.hey-bevan.com?token={something.access_token}";

            var ss = JsonConvert.SerializeObject(new { redirect_url = redirectUrl });

            var header = new Dictionary<string, string> {{"location", redirectUrl}};

            return new APIGatewayProxyResponse { StatusCode = 301, Headers = header};
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> GetAll()
        {
            return new APIGatewayProxyResponse
            {
                Headers = GetCorsHeaders(),
                StatusCode = 200
            };
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> GetActivitiesByChannelId(APIGatewayProxyRequest request)
        {
            var requestModel = JsonConvert.DeserializeObject<BevanRequest>(request.Body);
            var bevanByChannel = await _dynamoRepository.GetActivitiesByChannelId(requestModel.ChannelId);
            var bevanJson = JsonConvert.SerializeObject(bevanByChannel);

            return new APIGatewayProxyResponse
            {
                Headers = GetCorsHeaders(),
                StatusCode = 200, Body = bevanJson
            };
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public APIGatewayProxyResponse GetById(string userId)
        {
            return new APIGatewayProxyResponse
            {
                Headers = GetCorsHeaders(),
                StatusCode = 200
            };
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Redeemable(APIGatewayProxyRequest request)
        {
            var requestModel = JsonConvert.DeserializeObject<RedeemableRequest>(request.Body);
            var redeemables = await _dynamoRepository.GetRedeemableByRecieverId(requestModel.ReceiverId);
            var json = JsonConvert.SerializeObject(redeemables);
            return new APIGatewayProxyResponse
            {
                Headers = GetCorsHeaders(),
                StatusCode = 200, Body = json
            };
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Channels()
        {
            List<AwsDotnetCsharp.Models.Channel> channels = await _dynamoRepository.GetChannels();
            return new APIGatewayProxyResponse
            {
                Headers = GetCorsHeaders(),
                StatusCode = 200,
                Body = JsonConvert.SerializeObject(channels)
            };
        }

        private IDictionary<string, string> GetCorsHeaders()
        {
            return new Dictionary<string, string>()
            {
                new KeyValuePair<string, string>("Access-Control-Allow-Origin", "*"),
                new KeyValuePair<string, string>("Access-Control-Allow-Credentials", "true")
            };
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> LeaderBoard(APIGatewayProxyRequest request)
        {
            LeaderboardSearchRequest leaderboardRequest = JsonConvert.DeserializeObject<LeaderboardSearchRequest>(request.Body);
            // ask to dynamo ask for my selected data
            //            var requestModel = JsonConvert.DeserializeObject<Leaderboard>(request.Body);
            List<User> users = await _dynamoRepository.GetLeaderboard(leaderboardRequest);
            return new APIGatewayProxyResponse
            {
                Headers = GetCorsHeaders(),
                StatusCode = 200,
                Body = JsonConvert.SerializeObject(users)
            };
        }


        private async Task<string> somethingAsync()
        {
            using (var client = new AmazonDynamoDBClient())
            {
                var response = await client.ScanAsync(new ScanRequest("hey-bevan-table-new-dev"));

                var heyBevanJson = JsonConvert.SerializeObject(response.Items);

                return heyBevanJson;
            }
        }
    }
}