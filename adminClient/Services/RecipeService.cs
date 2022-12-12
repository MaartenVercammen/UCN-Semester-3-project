using admin_client.MVVM.Model;
using admin_client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace adminClient.Services
{
    public class RecipeService : BaseService
    {
        public async Task<List<Recipe>> GetRecipe()
        {
            List<Recipe> recipes = null;
            var task = _client.GetAsync("https://localhost:7088/Recipes");
            task.Wait();
            var response = task.Result;
            if (response.IsSuccessStatusCode)
            {
                recipes = await response.Content.ReadFromJsonAsync<List<Recipe>>();
            }
            return recipes;
        }

        public async Task<Recipe> GetRecipe(string id)
        {
            Recipe recipe = null;
            var response = await _client.GetAsync($"https://localhost:7088/recipes/{id}");
            if (response.IsSuccessStatusCode)
            {
                recipe = await response.Content.ReadFromJsonAsync<Recipe>();
            }
            return recipe;
        }

        public async Task<bool> DeleteRecipe(string id)
        {
            bool IsDone = false;
            var response = await _client.DeleteAsync($"https://localhost:7088/Recipe/{id}");
            if (response.IsSuccessStatusCode)
            {
                IsDone = bool.Parse(await response.Content.ReadAsStringAsync());
            }
            return IsDone;
        }

        public async Task<bool> CreateRecipe(Recipe recipe)
        {
            bool IsDone = false;
            var response = await _client.PostAsJsonAsync($"https://localhost:7088/Recipe", JsonSerializer.Serialize(recipe));
            if (response.IsSuccessStatusCode)
            {
                IsDone = bool.Parse(await response.Content.ReadAsStringAsync());
            }
            return IsDone;
        }

        public async Task<bool> UpdateRecipe(Recipe recipe)
        {
            bool IsDone = false;
            var response = await _client.PutAsJsonAsync($"https://localhost:7088/User", JsonSerializer.Serialize(recipe));
            if (response.IsSuccessStatusCode)
            {
                IsDone = bool.Parse(await response.Content.ReadAsStringAsync());
            }
            return IsDone;
        }
    }
}
