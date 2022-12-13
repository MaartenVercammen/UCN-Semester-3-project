using adminClient.MVVM.Model;
using adminClient.Services;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace adminClient.MVVM.ViewModel
{
    [QueryProperty(nameof(Recipe), "Recipe")]
    public partial class RecipeViewModel : ObservableObject
    {
        [ObservableProperty]
        Recipe recipe;

        [RelayCommand]
        public void Save()
        {
            Console.WriteLine("Save");
        }

    }
}
