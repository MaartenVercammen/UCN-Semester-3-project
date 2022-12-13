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
    public partial class BambooSessionOverviewViewModel : ObservableObject
    {

        public ObservableCollection<BambooSession> BambooSessions { get; } = new();
        public BambooSessionOverviewViewModel(BambooSessionService bambooSessionService)
        {
            var task = bambooSessionService.GetBambooSessions();
            task.Wait();
            var bambooSessionsResult = task.Result;
            foreach (var bambooSession in bambooSessionsResult)
            {
                BambooSessions.Add(bambooSession);
            }
        }

        [RelayCommand]
        async Task GoToDetails(BambooSession bambooSession)
        {
            if (bambooSession == null)
                return;

            await Shell.Current.GoToAsync(nameof(BambooSessionView), true, new Dictionary<string, object>
            {
                {"BambooSession", bambooSession }
            });
        }

    }
}
