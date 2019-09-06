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
using AwsDotnetCsharp.Service;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AwsDotnetCsharp
{
    public class Handler
    {

        private readonly IDynamoRepository _dynamoRepository;
        private readonly ISlackService _slackService;
        private readonly SlackMessage _slackMessage;
        
        public Handler()
        {
            _slackService = new SlackService();
            _dynamoRepository = new DynamoRepository();
            _slackMessage  = new SlackMessage(_dynamoRepository); //hacky...
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public APIGatewayProxyResponse AddBevan(APIGatewayProxyRequest request)
        {
            return new APIGatewayProxyResponse { StatusCode = 200 };
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

            switch (request.Type)
            {
                case "url_verification":
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = 200,
                        Body = JsonConvert.SerializeObject(new
                        {
                            Challenge = request.Challenge
                        })
                    };
                case "event_callback":

                    await _slackMessage.ProcessMessage(request.Event);

                    return new APIGatewayProxyResponse
                    {
                        StatusCode = 200,
                        Body = JsonConvert.SerializeObject(new
                        {
                            Message = request.Event.Text
                        })
                    };
                default:
                    return new APIGatewayProxyResponse
                    {
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
            return new APIGatewayProxyResponse { StatusCode = 200};
        }
        
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> GetByChannelId(APIGatewayProxyRequest request)
        {
            var requestModel = JsonConvert.DeserializeObject<BevanRequest>(request.Body);
            var bevanByChannel = await _dynamoRepository.GetBevansByChannel(requestModel.ChannelId);
            var bevanJson = JsonConvert.SerializeObject(bevanByChannel);
           
            return new APIGatewayProxyResponse { StatusCode = 200, Body = bevanJson};
        }
        
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public APIGatewayProxyResponse GetById(string userId)
        {
            return new APIGatewayProxyResponse { StatusCode = 200 };
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Redeemable(APIGatewayProxyRequest request)
        {
            var requestModel = JsonConvert.DeserializeObject<RedeemableRequest>(request.Body);
            var redeemables = await _dynamoRepository.GetRedeemableByRecieverId(requestModel.ReceiverId);
            var json = JsonConvert.SerializeObject(redeemables);
            return new APIGatewayProxyResponse { StatusCode = 200, Body = json };
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Channels()
        {
            List<string> channels = await _dynamoRepository.GetChannels();
            return new APIGatewayProxyResponse { StatusCode = 200, Body = JsonConvert.SerializeObject(channels) };
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> LeaderBoard(APIGatewayProxyRequest request)
        {
            // ask to dynamo ask for my selected data
//            var requestModel = JsonConvert.DeserializeObject<Leaderboard>(request.Body);
            List<User> users = await _dynamoRepository.GetLeaderboard();
            return new APIGatewayProxyResponse { StatusCode = 200, Body = JsonConvert.SerializeObject(users) };
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