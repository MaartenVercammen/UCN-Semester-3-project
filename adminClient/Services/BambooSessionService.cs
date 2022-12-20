using adminClient.MVVM.Model;
using adminClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace adminClient.Services
{
    public class BambooSessionService : BaseService
    {
        public async Task<List<BambooSession>> GetBambooSessions()
        {
            List<BambooSession> BambooSessions = null;
            var task = _client.GetAsync("https://localhost:7088/BambooSession");
            task.Wait();
            var response = task.Result;
            if (response.IsSuccessStatusCode)
            {
                BambooSessions = await response.Content.ReadFromJsonAsync<List<BambooSession>>();
            }
            return BambooSessions;
        }

        public async Task<BambooSession> GetBambooSession(string id)
        {
            BambooSession BambooSession = null;
            var response = await _client.GetAsync($"https://localhost:7088/BambooSession/{id}");
            if (response.IsSuccessStatusCode)
            {
                BambooSession = await response.Content.ReadFromJsonAsync<BambooSession>();
            }
            return BambooSession;
        }

        public async Task<bool> DeleteBambooSession(string id)
        {
            bool IsDone = false;
            var response = await _client.DeleteAsync($"https://localhost:7088/BambooSession/{id}");
            if (response.IsSuccessStatusCode)
            {
                IsDone = bool.Parse(await response.Content.ReadAsStringAsync());
            }
            return IsDone;
        }

        public async Task<bool> CreateBambooSession(BambooSession BambooSession)
        {
            bool IsDone = false;
            var response = await _client.PostAsJsonAsync($"https://localhost:7088/BambooSession", JsonSerializer.Serialize(BambooSession));
            if (response.IsSuccessStatusCode)
            {
                IsDone = bool.Parse(await response.Content.ReadAsStringAsync());
            }
            return IsDone;
        }

        public async Task<bool> UpdateBambooSession(BambooSession BambooSession)
        {
            bool IsDone = false;
            var response = await _client.PutAsJsonAsync($"https://localhost:7088/BambooSession", JsonSerializer.Serialize(BambooSession));
            if (response.IsSuccessStatusCode)
            {
                IsDone = bool.Parse(await response.Content.ReadAsStringAsync());
            }
            return IsDone;
        }
    }
}
