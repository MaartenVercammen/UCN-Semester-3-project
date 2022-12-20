using adminClient.MVVM.Model;
using adminClient.Services;
using adminClient.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

        private readonly CountService _countService;

        [ObservableProperty]
        int userCount;

        [ObservableProperty]
        int recipeCount;

        [ObservableProperty]
        int bambooCount;

        [ObservableProperty]
        int visitors;

        [ObservableProperty]
        bool isRefreshing;


        public MainPageView(UserService userService, RecipeService recipeService, BambooSessionService bambooSessionService, CountService countService)
        {
            _recipeService = recipeService;
            _bambooSessionService = bambooSessionService;
            _userService= userService;
            _countService = countService;

            GetData();
        }

        private void GetData()
        {
            var recipes = GetRecipes();
            var bambbo = GetBabmboo();
            var users = GetUsers();
            var count = GetCount();

            Task.WaitAll(recipes, bambbo, users, count);

            UserCount = users.Result.Count;
            RecipeCount = recipes.Result.Count;
            BambooCount = bambbo.Result.Count;
            Visitors = count.Result;
        }

        private async Task<int> GetCount()
        {
            var count = await _countService.GetCount();
            return count;
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

        [RelayCommand]
        void Refresh()
        {
            IsRefreshing = true;
            GetData();
            IsRefreshing= false;
        }
    }
}
