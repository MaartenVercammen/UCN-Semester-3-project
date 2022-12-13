using admin_client.MVVM.Model;
using admin_client.Services;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace admin_client.MVVM.ViewModel
{
    [QueryProperty(nameof(User), "User")]
    public partial class UserViewModel : ObservableObject
    {
        [ObservableProperty]
        User user;

        [RelayCommand]
        public void Save()
        {
            Console.WriteLine("Save");
        }

    }
}
