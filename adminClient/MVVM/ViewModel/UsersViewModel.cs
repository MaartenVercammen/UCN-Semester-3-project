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
    public partial class UsersViewModel: ObservableObject
    {
        private readonly UserService _userService;
        public ObservableCollection<User> Users { get; } = new();

        [ObservableProperty]
        bool isRefreshing;
        public UsersViewModel(UserService userService)
        {
            _userService= userService;
            GetUsers();
        }

        private void GetUsers() {
            var task = _userService.GetUsers();
            task.Wait();
            var userList = task.Result;
            foreach (var user in userList)
            {
                Users.Add(user);
            }
        }

        [RelayCommand]
        async Task GoToDetails(User user)
        {
            if (user == null)
                return;

            await Shell.Current.GoToAsync(nameof(UserView), true, new Dictionary<string, object>
            {
                {"User", user }
            });
        }

        [RelayCommand]
        void Refresh()
        {
            IsRefreshing = true;
            GetUsers();
            IsRefreshing = false;
        }
    }
}
