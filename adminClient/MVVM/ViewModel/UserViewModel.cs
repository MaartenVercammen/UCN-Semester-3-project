using admin_client.MVVM.Model;
using admin_client.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
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

        private readonly UserService _userService;
        public UserViewModel(UserService userService)
        { 
           //_userService= userService;
           //var task = Task.Run(() =>  GetUser());
           //task.Wait();
           //user = task.Result;
        }

        public async Task<User> GetUser()
        {
            User getUser = null;
            try
            {
                getUser = await _userService.GetUser("6cc2fb6f-762d-4f17-a930-40dbd7aff7e3");
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return getUser;
        }
    }
}
