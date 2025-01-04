using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using WytSky.Mobile.Maui.Hadrein.Dtos;

namespace WytSky.Mobile.Maui.Hadrein.ViewModels.User
{
    public partial class MyOrderVM : BaseViewModel
    {
        [ObservableProperty]
        public ObservableCollection<StOrder> upcomingOrders;

        [ObservableProperty]
        public ObservableCollection<StOrder> historyOrders;

        public async Task GetAllOrders()
        {
            var response = await APIs.ServiceApp.GetAll<StOrder>("StOrder", isLoading: true);
            if (response != null)
            {
                UpcomingOrders = new ObservableCollection<StOrder>(response.Where(x => (x.IsApproval.HasValue && x.IsApproval.Value) || x.IsActive.HasValue && x.IsActive.Value));
                HistoryOrders = new ObservableCollection<StOrder>(response.Where(x => x.IsCompleted.HasValue && x.IsCompleted.Value));
            }
        }
    }
}
