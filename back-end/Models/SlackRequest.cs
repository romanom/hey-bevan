using System;
using System.Data.Common;

namespace AwsDotnetCsharp.Models
{
    public class SlackRequest
    {
        public string Token { get; set; }
        public string Team_Id { get; set; }
        public string Api_App_Id { get; set; }
        public Event Event { get; set; }

        public string Type { get; set; }

        public string[] Authed_Teams { get; set; }

        public string Event_Id { get; set; }

        public string Event_Time { get; set; }
        public string Challenge { get; set; }
    }

    public class Event
    {
        public string Type { get; set; }
        public string Channel { get; set; }
        public string User { get; set; }
        public string Text { get; set; }
        public string Ts { get; set; }
        public string Event_Ts { get; set; }
        public string Channel_Type { get; set; }
    }

    public class BevanRequest
    {
        public string ChannelId { get; set; }
    }

    public class RedeemableRequest
    {
        public string ReceiverId { get; set; }
    }

    public class Leaderboard
    {
        public string RecognitionRole { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Channel { get; set; }
    }
}