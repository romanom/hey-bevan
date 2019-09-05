using Amazon.Lambda.Core;
using System;
using System.Collections.Generic;
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

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AwsDotnetCsharp
{
    public class Handler
    {

      private readonly IDynamoRepository _dynamoRepository;
      public Handler()
      {
        _dynamoRepository = new DynamoRepository(new DynamoDbConfiguration
        {
          TableName = "hey-bevan-table-dev"
        }, new AwsClientFactory<AmazonDynamoDBClient>(new AwsBasicConfiguration()));
      }
      [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
      public APIGatewayProxyResponse AddBevan(APIGatewayProxyRequest request)
      {
        return new APIGatewayProxyResponse {StatusCode = 200};
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
              StatusCode = 200, Body = JsonConvert.SerializeObject(new
              {
                Challenge = request.Challenge
              })
            };
          case "event_callback":
            // do dynamo db inserts
            var bevan = new Bevan
            {
              UserId = request.Event.User,
              Count = 1,
              Message = request.Event.Text,
              Channel = request.Event.Channel,
              GiverId = "123"
            };

            await _dynamoRepository.SaveBevan(bevan);
            
            SlackMessage.PostMessage(request.Event);
          
            return new APIGatewayProxyResponse
            {
              StatusCode = 200, Body = JsonConvert.SerializeObject(new
              {
                Message = request.Event.Text
              })
            };
          default:
            return new APIGatewayProxyResponse
            {
              StatusCode = 200, Body = JsonConvert.SerializeObject(new
              {
                Error = "unhandled"
              })
            };
        }
      }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
      public APIGatewayProxyResponse GetAll()
      {
        return new APIGatewayProxyResponse {StatusCode = 200};
      }

      [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
      public APIGatewayProxyResponse GetById(string userId)
      {
        return new APIGatewayProxyResponse {StatusCode = 200};
      }
      
      [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
      public APIGatewayProxyResponse Redeemable(string userId)
      {
        // ask to dynamo ask for number of bevans given userId"
        int totalBevans = 100;
        return new APIGatewayProxyResponse {StatusCode = 200, Body = totalBevans.ToString()};
      }
      
      [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
      public APIGatewayProxyResponse Channels()
      {
        // ask to dynamo ask for my selected channels where bevans are used
        List<string> channels = new List<string>();
        return new APIGatewayProxyResponse {StatusCode = 200, Body = JsonConvert.SerializeObject(channels)};
<<<<<<< HEAD
      }
      
      [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
      public APIGatewayProxyResponse LeaderBoard(Enum recognitionRole, DateTime startDate, DateTime endDate, string channel)
      {
        // ask to dynamo ask for my selected data
        List<Users> users = new List<Users>();
        return new APIGatewayProxyResponse {StatusCode = 200, Body = JsonConvert.SerializeObject(users)};
      }
      
      
      private async Task<string> somethingAsync()
      {
        using (var client = new AmazonDynamoDBClient())
        {
          var response = await client.ScanAsync(new ScanRequest("hey-bevan-table-dev"));

          var heyBevanJson = JsonConvert.SerializeObject(response.Items);

          return heyBevanJson;
        }
      }    
    
=======
      } 
>>>>>>> ebd72f185ff382f4cec953d632ab5b233ff1787d
    }
}
