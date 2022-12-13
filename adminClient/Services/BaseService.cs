using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace adminClient.Services
{
    public partial class BaseService
    {
        protected HttpClient _client;
        public BaseService() 
        {
            _client = new HttpClient();
            var task = Task.Run( () => GetToken());
            task.Wait();
            var token = task.Result;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
        }

        private async Task<string> GetToken()
        {
            string token = "";
            IEnumerable<string> tokens;
            using (var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7088/Authorization"))
            {
                request.Headers.Add("Email", "maarten@foodpanda.dev");
                request.Headers.Add("Password", "admin");
                var response = await _client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    response.Headers.TryGetValues("token", out tokens);
                    token = tokens.FirstOrDefault();
                }
            }
            

            return token;
        }

    }
}
