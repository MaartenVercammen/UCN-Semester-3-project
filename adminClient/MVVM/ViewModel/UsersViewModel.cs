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
        public ObservableCollection<User> Users { get; } = new();
        public UsersViewModel(UserService userService)
        {
            var task = userService.GetUsers();
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
    }
}
