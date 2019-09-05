using Amazon.Lambda.Core;
using System;
using System.Threading.Tasks;
using System.Transactions;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.APIGatewayEvents;
using AwsDotnetCsharp.Infrastructure;
using AwsDotnetCsharp.Infrastructure.Configs;
using AwsDotnetCsharp.Models;
using AwsDotnetCsharp.Repository;
using Newtonsoft.Json;

[assembly:LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AwsDotnetCsharp
{
    public class Handler
    {

      const string emoji = ":bevan:";

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
              Message = request.Event.Text
            };

            // await _dynamoRepository.SaveBevan(bevan);

            var ss = await somethingAsync();
            
            ProcessMessage(request.Event);

            return new APIGatewayProxyResponse
            {
              StatusCode = 200, Body = JsonConvert.SerializeObject(new
              {
                Message = request.Event.Text,
                Something = ss
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

        private void ProcessMessage(Event @event)
        {

            Console.WriteLine("User: {0} Message: \"{1}\"", @event.User, @event.Text);

            if (@event.Text.Contains(emoji)){
              //do someting
              var noOfEmojis = @event.Text.Split(emoji).Length - 1;
              Console.WriteLine("{0} gave \"{1}\" emojis to someone...", @event.User, noOfEmojis);

            }

            //do nothing
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
      public APIGatewayProxyResponse ShowSent(APIGatewayProxyRequest request)
      {
        return new APIGatewayProxyResponse {StatusCode = 200};
      }
      
      [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
      public APIGatewayProxyResponse ShowGiven(APIGatewayProxyRequest request)
      {
        return new APIGatewayProxyResponse {StatusCode = 200};
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
    
    }
    
}
