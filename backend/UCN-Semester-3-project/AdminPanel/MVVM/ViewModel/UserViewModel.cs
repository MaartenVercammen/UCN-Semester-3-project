using System;
using System.Threading.Tasks;
using AdminPanel.MVVM.Model;
using AdminPanel.Service;
using AdminPanel.Tools;

namespace AdminPanel.MVVM.ViewModel
{
    public class UserViewModel : ObservableObject
    {
        private User _user;

        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }

        public UserViewModel()
        {
           Repository repository= new Repository();
           User = repository.GetUser(Guid.Parse("6cc2fb6f-762d-4f17-a930-40dbd7aff7e3"));
        }
    }
}