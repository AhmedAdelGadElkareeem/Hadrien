using WytSky.Mobile.Maui.Hadrein.ViewModels.User;

namespace WytSky.Mobile.Maui.Hadrein.Views.User;

public partial class MyOrdersPage : BaseContentPage
{
    MyOrderVM myOrderVM = new MyOrderVM();
	public MyOrdersPage()
	{
		InitializeComponent();
        BindingContext = myOrderVM;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await myOrderVM.GetAllOrders();
    }

    private void HistoryClicked(object sender, TappedEventArgs e)
    {
		upcomingList.IsVisible = false;
		historyList.IsVisible = true;

        if (App.Current.Resources.TryGetValue("PrimaryColor", out var primaryColor))
            historyFrame.BackgroundColor = (Color)primaryColor;
        if (App.Current.Resources.TryGetValue("White", out var whiteColor))
            historyLabel.TextColor = (Color)whiteColor;

        upcomingFrame.BackgroundColor = Colors.Transparent;
        upcomingLabel.TextColor = (Color)primaryColor;
    }

    private void UpcomingClicked(object sender, TappedEventArgs e)
    {
        upcomingList.IsVisible = true;
        historyList.IsVisible = false;

        if (App.Current.Resources.TryGetValue("PrimaryColor", out var primaryColor))
            upcomingFrame.BackgroundColor = (Color)primaryColor;
        if (App.Current.Resources.TryGetValue("White", out var whiteColor))
            upcomingLabel.TextColor = (Color)whiteColor;

        historyFrame.BackgroundColor = Colors.Transparent;
        historyLabel.TextColor = (Color)primaryColor;
    }
}