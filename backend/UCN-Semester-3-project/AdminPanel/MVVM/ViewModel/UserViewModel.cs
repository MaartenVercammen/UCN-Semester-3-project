using System;
using AdminPanel.MVVM.Model;
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
            User = new User(Guid.NewGuid(), "Mark", "Mark", "email", "password", "Home", "USER");
        }
    }
}