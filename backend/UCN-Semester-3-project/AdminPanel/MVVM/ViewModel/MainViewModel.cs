using System;
using AdminPanel.Service;
using AdminPanel.Tools;

namespace AdminPanel.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        //Action command
        public RelayCommand OverviewCommand { get; set; }

        //Action command
        public RelayCommand UserViewCommand { get; set; }
        
        // New Screen / object 
        public OverviewViewModel OverviewViewModel { get; set; }

        //New Screen / object 
        public UserViewModel UserViewModel { get; set; }

        private object _currentView;
        public object  CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            OverviewViewModel = new OverviewViewModel();
            UserViewModel = new UserViewModel();
            
            CurrentView = OverviewViewModel;
            
            UserViewCommand = new RelayCommand(o => CurrentView = UserViewModel);
            OverviewCommand = new RelayCommand(o => CurrentView = OverviewViewModel);

        }
        
    }
}