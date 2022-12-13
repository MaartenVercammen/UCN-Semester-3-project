using adminClient.MVVM.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adminClient.MVVM.ViewModel
{
    [QueryProperty(nameof(BambooSession), "BambooSession")]
    public partial class BambooSessionViewModel : ObservableObject
    {
        [ObservableProperty]
        BambooSession bambooSession;

        public BambooSessionViewModel()
        {
            
        } 
    }
}
