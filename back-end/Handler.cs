using Amazon.Lambda.Core;
using System;
using System.Threading.Tasks;
using System.Transactions;
using Amazon.Lambda.APIGatewayEvents;
using AwsDotnetCsharp.Models;
using Newtonsoft.Json;

[assembly:LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AwsDotnetCsharp
{
    public class Handler
    {
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
    }
    
}
