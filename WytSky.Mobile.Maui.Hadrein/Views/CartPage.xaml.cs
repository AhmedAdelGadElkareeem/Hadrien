namespace WytSky.Mobile.Maui.Hadrein.Views;

public partial class CartPage : BaseContentPage
{
    public CartPage()
    {
        InitializeComponent();
        BindingContext = App.ResturantProfileVM;
        cartItems.ItemsSource = App.CartItems;
    }

    private async void Back(object sender, EventArgs e)
    {
        App.Current.MainPage.Navigation.PopModalAsync();
    }
}