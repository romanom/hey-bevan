using Amazon.Lambda.Core;
using System;
using System.Threading.Tasks;
using System.Transactions;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.APIGatewayEvents;
using AwsDotnetCsharp.Models;
using Newtonsoft.Json;

[assembly:LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AwsDotnetCsharp
{
    public class Handler
    {

      const string emoji = ":bevan:";

      [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
      public APIGatewayProxyResponse AddBevan(APIGatewayProxyRequest request)
      {
        return new APIGatewayProxyResponse {StatusCode = 200};
      }

      [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
      public APIGatewayProxyResponse Challenge(APIGatewayProxyRequest request)
      {
        var requestModel = JsonConvert.DeserializeObject<SlackRequest>(request.Body);
        
        return HandleRequest(requestModel);
      }

      private APIGatewayProxyResponse HandleRequest(SlackRequest request)
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

            ProcessMessage(request.Event);

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

        private void ProcessMessage(Event @event)
        {

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
          var response = await client.ScanAsync(new ScanRequest("HeyBevanTable"));

          var heyBevanJson = JsonConvert.SerializeObject(response.Items);

          return heyBevanJson;
        }
      }    
    
    }
    
}
