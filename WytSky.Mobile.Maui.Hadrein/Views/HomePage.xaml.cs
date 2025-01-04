using System.Collections.ObjectModel;
using WytSky.Mobile.Maui.Hadrein.Dtos;
using WytSky.Mobile.Maui.Hadrein.Helpers;

namespace WytSky.Mobile.Maui.Hadrein.Views;

public partial class HomePage : BaseContentPage
{
    List<CategoryModel> ResturantOffers = new List<CategoryModel>();

    public HomePage()
    {
        try
        {
            InitializeComponent();
            BindingContext = App.HomeVM;

            ResturantOffers = new List<CategoryModel>()
            {
                    new CategoryModel(){Picurl1="resturant_offer"},
                    new CategoryModel(){Picurl1="resturant_offer"},
                    new CategoryModel(){Picurl1="resturant_offer"},
                    new CategoryModel(){Picurl1="resturant_offer"},
                    new CategoryModel(){Picurl1="resturant_offer"},
                    new CategoryModel(){Picurl1="resturant_offer"},
                    new CategoryModel(){Picurl1="resturant_offer"},
                    new CategoryModel(){Picurl1="resturant_offer"},
                    new CategoryModel(){Picurl1="resturant_offer"},
            };
            resturantOffers.ItemsSource = ResturantOffers;

            Task.Run(async () =>
            {
                await App.HomeVM.GetCategories();
                await App.HomeVM.GetNearestForAllMainCategoryParentID();
                await App.ShowConfirmationDeleteOrSendCachedCartItemsPopup();
            });

        }
        catch (Exception ex)
        {
            string er = ex.Message;
        }
    }

    private async void ItemSelected(object sender, TappedEventArgs e)
    {
        try
        {
            var _CollectionView = (sender as View).Parent as CollectionView;
            var item = (sender as View).BindingContext as StCatgeory ?? null;
            var selectedItem = _CollectionView.ItemsSource as IEnumerable<StCatgeory>;
            await App.HomeVM.NavigateToPage(item, new ObservableCollection<StCatgeory>(selectedItem));
        }
        catch (Exception ex)
        {
            ExtensionLogMethods.LogExtension(ex, "", "HomePage", "ItemSelected");
        }
    }
}