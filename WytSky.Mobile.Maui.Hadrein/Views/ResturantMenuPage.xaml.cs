using System.Collections.ObjectModel;
using WytSkyECommerceTalabat.Dtos;

namespace WytSkyECommerceTalabat.Views;

public partial class ResturantMenuPage : BaseContentPage
{
    public List<CategoryModel> ItemsList { get; set; }

    public ResturantMenuPage(StCatgeory selectedCategory, ObservableCollection<StItem> popularItems)
    {
        InitializeComponent();
        BindingContext = App.ResturantProfileVM;

        image.Source = selectedCategory.CatgeoryImageUrl;
        name.Text = selectedCategory.Name;

        App.ResturantProfileVM.PopularItems = popularItems;

        ItemsList = new List<CategoryModel>()
        {
                new CategoryModel(){NameEn = "All menu"},
                new CategoryModel(){NameEn = "Sea food"},
                new CategoryModel(){NameEn = "Burger"},
                new CategoryModel(){NameEn = "Chinese"},
                new CategoryModel(){NameEn = "Drinks"},
        };
        items.ItemsSource = ItemsList;
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