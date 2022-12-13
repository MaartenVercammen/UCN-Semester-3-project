﻿using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using adminClient.MVVM.Model;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace adminClient.Services
{
    public class UserService : BaseService
    {
        public UserService()
        {
        }

        public async Task<List<User>> GetUsers()
        {
            List<User> users = null;
            var task = _client.GetAsync("https://localhost:7088/User");
            task.Wait();
            var response = task.Result;
            if (response.IsSuccessStatusCode)
            {
                users = await response.Content.ReadFromJsonAsync<List<User>>();
            }
            return users;
        }

        public async Task<User> GetUser(string id)
        {
            User user = null;
            var task = _client.GetAsync($"https://localhost:7088/User/{id}");
            task.Wait();
            var response = task.Result;
            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadFromJsonAsync<User>();
            }
            return user;
        }

        public async Task<bool> DeleteUser(string id)
        {
            bool IsDone = false;
            var task = _client.DeleteAsync($"https://localhost:7088/User/{id}");
            task.Wait();
            var response = task.Result;
            if (response.IsSuccessStatusCode)
            {
                IsDone = bool.Parse(await response.Content.ReadAsStringAsync());
            }
            return IsDone;
        }

        public async Task<bool> CreateUser(User user)
        {
            bool IsDone = false;
            var response = await _client.PostAsJsonAsync($"https://localhost:7088/User", JsonSerializer.Serialize(user));
            if (response.IsSuccessStatusCode)
            {
                IsDone = bool.Parse(await response.Content.ReadAsStringAsync());
            }
            return IsDone;
        }

        public async Task<bool> UpdateUser(User user)
        {
            bool IsDone = false;
            var task = _client.PutAsJsonAsync<User>("https://localhost:7088/User",user);
            task.Wait();
            var response = task.Result;
            if (response.IsSuccessStatusCode)
            {
                IsDone = bool.Parse(await response.Content.ReadAsStringAsync());
            }
            return IsDone;
        }
    }
}
