using admin_client.MVVM.Model;
using admin_client.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace admin_client.MVVM.ViewModel
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
    }
}
