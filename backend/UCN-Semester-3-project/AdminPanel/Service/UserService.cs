using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using AdminPanel.MVVM.Model;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace AdminPanel.Service
{
    public class UserService
    {
        private HttpClient _client;
        public UserService() {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJtYWFydGVuQGZvb2RwYW5kYS5kZXYiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBRE1JTiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiNmNjMmZiNmYtNzYyZC00ZjE3LWE5MzAtNDBkYmQ3YWZmN2UzIiwiZXhwIjoxNjcwNTkzMTY5LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MDg4IiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzA4OCJ9.PbbJ67J9_h8wQsaZEqNDAQ060WUVJdm84E5ru08zzac");
            //_client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("connection"));
        }

        public async Task<User> GetUser(Guid id)
        {
            User user = null;
            var result = await _client.GetStringAsync("https://localhost:7088/User/" + id).ConfigureAwait(false);
            string content = result;
            JObject jUser = JObject.Parse(result);
            user = new User(jUser);
            return user;
        }
    }
}
