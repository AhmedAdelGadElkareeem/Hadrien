using System.Collections.ObjectModel;
using WytSky.Mobile.Maui.Hadrein.Dtos;

namespace WytSky.Mobile.Maui.Hadrein.Views;

public partial class ItemsPage : BaseContentPage
{
    public ItemsPage(ObservableCollection<StItem> items , ObservableCollection<StCatgeory> subSubcategories)
    {
        InitializeComponent();

        BindingContext = App.ResturantProfileVM;

        App.ResturantProfileVM.Categories = subSubcategories;
        App.ResturantProfileVM.Items = items;
        App.ResturantProfileVM.TempItems = items;
    }

    private void Back(object sender, EventArgs e)
    {
        App.Current.MainPage.Navigation.PopModalAsync();
    }

    private void OpenCartItems(object sender, EventArgs e)
    {
        App.Current.MainPage.Navigation.PushModalAsync(new CartPage());
    }
}