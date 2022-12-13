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
    [QueryProperty(nameof(User), "User")]
    public partial class UserViewModel : ObservableObject
    {
        [ObservableProperty]
        User user;

        [ObservableProperty]
        bool isDone = false;

        UserService _userService;

        public UserViewModel(UserService userService)
        {
            _userService = userService;
        }

        [RelayCommand]
        public void Save()
        {
            var task = _userService.UpdateUser(User);
            task.Wait();
            IsDone = task.Result;
        }

        [RelayCommand]
        public void Delete()
        {
            var task = _userService.DeleteUser(User.UserId.ToString());
            task.Wait();
            IsDone = task.Result;
        }

    }
}
