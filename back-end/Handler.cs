using Amazon.Lambda.Core;
using System;
using System.Transactions;
using AwsDotnetCsharp.Models;

[assembly:LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AwsDotnetCsharp
{
    public class Handler
    {
      public Response AddBevan(Request request)
      {
        return new Response();
      }

      public Response Challenge(SlackAuthentication request)
      {
        return new Response
        {
          StatusCode = 200
        };
      }
       
      public Response GetAll()
      {
        return new Response();
      }

      public Response GetById(string userId)
      {
        return new Response();
      }
    }

    public class Response
    {
      public object StatusCode {get; set;}
      public Header Headers { get; set; }
    }

    public class Header
    {  
    }

    public class Request
    {
      public string Key1 {get; set;}
      public string Key2 {get; set;}
      public string Key3 {get; set;}

      public Request(string key1, string key2, string key3){
        Key1 = key1;
        Key2 = key2;
        Key3 = key3;
      }
    }
}
