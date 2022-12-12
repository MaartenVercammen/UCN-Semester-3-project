﻿using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using admin_client.MVVM.Model;

namespace admin_client.Services
{
    public class UserService : BaseService
    {
        public UserService()
        {
        }

        public async Task<List<User>> GetUsers()
        {
            List<User> users = null;
            var response = await _client.GetAsync($"https://localhost:7088/User");
            if (response.IsSuccessStatusCode)
            {
                users = await response.Content.ReadFromJsonAsync<List<User>>();
            }
            return users;
        }

        public async Task<User> GetUser(string id)
        {
            User user = null;
            var response = await _client.GetAsync($"https://localhost:7088/User/{id}");
            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadFromJsonAsync<User>();
            }
            return user;
        }
    }
}
