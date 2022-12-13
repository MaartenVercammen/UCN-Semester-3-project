using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adminClient.Services
{
    public class CountService : BaseService
    {
        public async Task<int> GetCount()
        {
            int count = 0;
            var task = _client.GetAsync("https://localhost:7088/Statistics");
            task.Wait();
            var response = task.Result;
            if (response.IsSuccessStatusCode)
            {
                count = int.Parse(await response.Content.ReadAsStringAsync());
            }
            return count;
        }
    }
}
