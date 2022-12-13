using adminClient.MVVM.Model;
using adminClient.MVVM.View;
using adminClient.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adminClient.MVVM.ViewModel
{
    public partial class RecipesViewModel: ObservableObject
    {
        public ObservableCollection<Recipe> Recipes { get; } = new();
        public RecipesViewModel(RecipeService recipeService)
        {
            var task = recipeService.GetRecipes();
            task.Wait();
            var recipeList = task.Result;
            foreach (var recipe in recipeList)
            {
                Recipes.Add(recipe);
            }
        }

        [RelayCommand]
        async Task GoToDetails(Recipe recipe)
        {
            if (recipe == null)
                return;

            await Shell.Current.GoToAsync(nameof(RecipeView), true, new Dictionary<string, object>
            {
                {"Recipe", recipe }
            });
        }
    }
}
