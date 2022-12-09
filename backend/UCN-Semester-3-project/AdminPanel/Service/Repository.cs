using AdminPanel.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Service
{

    public class Repository
    {
        private readonly UserService _userService;

        public Repository()
        {
            _userService = new UserService();
        }

        public User GetUser(Guid id)
        {
            User user = null;
            Task<User> userTask = _userService.GetUser(id);
            Task.Run(() => userTask);
            Task.WaitAll(userTask);
            user = userTask.Result;
            return user;
        }
    }
}
