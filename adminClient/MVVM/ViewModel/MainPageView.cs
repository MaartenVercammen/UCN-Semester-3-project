using admin_client.MVVM.Model;
using admin_client.Services;
using adminClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace admin_client.MVVM.ViewModel
{
    public class MainPageView
    {

        private readonly RecipeService _recipeService;

        private readonly BambooSessionService _bambooSessionService;

        public MainPageView(UserService userService, RecipeService recipeService, BambooSessionService bambooSessionService)
        {
            _recipeService = recipeService;
            _bambooSessionService = bambooSessionService;

            var recipes = GetRecipes();
            var bambbo = GetBabmboo();

            Console.WriteLine("Hrer");
        }

        public async Task<List<Recipe>> GetRecipes()
        {
            var recipes = await _recipeService.GetRecipes();
            return recipes;
        }

        public async Task<List<BambooSession>> GetBabmboo()
        {
            var recipes = await _bambooSessionService.GetBambooSessions();
            return recipes;
        }
    }
}
