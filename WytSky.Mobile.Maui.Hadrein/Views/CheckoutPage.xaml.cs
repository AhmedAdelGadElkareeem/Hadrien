namespace WytSky.Mobile.Maui.Hadrein.Views;

public partial class CheckoutPage : BaseContentPage
{
    public CheckoutPage()
    {
        InitializeComponent();
        BindingContext = App.ResturantProfileVM;
        cartItems.ItemsSource = App.CartItems;
    }
    private void Back(object sender, EventArgs e)
    {
        App.Current.MainPage.Navigation.PopModalAsync();
    }
}