using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Amazon.Util;
using Newtonsoft.Json;

namespace AwsDotnetCsharp.Service
{
    public interface ISlackService
    {
        Task<AuthResponse> DoAuth(string code);
    }

    public class AuthResponse
    {
        public string access_token { get; set; }
        public string scope { get; set; }
    }   
    
    public class SlackService: ISlackService
    {
        private HttpClient _httpClient;
        
        public async Task<AuthResponse> DoAuth(string code)
        {
            _httpClient = new HttpClient
            {
                BaseAddress =  new Uri("https://slack.com/")
            };

            var auth = await _httpClient.PostAsync("api/oauth.access", new FormUrlEncodedContent(
                new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("client_id", "2315277109.736385718354"),
                    new KeyValuePair<string, string>("client_secret", "ccc74e83cc7f10ee7bac633e2a626153"),
                    new KeyValuePair<string, string>("code", code),
                }));

            var ss = await auth.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<AuthResponse>(ss);
        }
    }
}