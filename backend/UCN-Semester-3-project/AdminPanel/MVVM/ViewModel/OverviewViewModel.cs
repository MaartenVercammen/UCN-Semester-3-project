using System;
using AdminPanel.Tools;

namespace AdminPanel.MVVM.ViewModel
{
    public class OverviewViewModel : ObservableObject
    {
        private string _name;

        public string Name
        {
            get => _name;

            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        
        public OverviewViewModel()
        {
            Name = "Mark";
        }
    }
}