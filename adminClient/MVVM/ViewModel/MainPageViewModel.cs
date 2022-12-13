using adminClient.MVVM.Model;
using adminClient.Services;
using adminClient.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adminClient.MVVM.ViewModel
{
    public partial class MainPageView : ObservableObject
    {

        private readonly RecipeService _recipeService;

        private readonly BambooSessionService _bambooSessionService;

        private readonly UserService _userService;

        [ObservableProperty]
        int userCount;

        [ObservableProperty]
        int recipeCount;

        [ObservableProperty]
        int bambooCount;
        

        public MainPageView(UserService userService, RecipeService recipeService, BambooSessionService bambooSessionService)
        {
            _recipeService = recipeService;
            _bambooSessionService = bambooSessionService;
            _userService= userService;

            var recipes = GetRecipes();
            var bambbo = GetBabmboo();
            var users = GetUsers();

            Task.WaitAll(recipes, bambbo, users);

            UserCount = users.Result.Count;
            RecipeCount = recipes.Result.Count;
            BambooCount = bambbo.Result.Count;
        }

        private async Task<List<User>> GetUsers()
        {
            var users = await _userService.GetUsers();
            return users;
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
