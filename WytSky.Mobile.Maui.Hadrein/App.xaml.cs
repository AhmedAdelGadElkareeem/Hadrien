
using CommunityToolkit.Maui.Core;
using System.Collections.ObjectModel;
using System.Globalization;
using WytSky.Mobile.Maui.Hadrein.Dtos;
using WytSky.Mobile.Maui.Hadrein.Helpers;
using WytSky.Mobile.Maui.Hadrein.Services.Implementation;
using WytSky.Mobile.Maui.Hadrein.ViewModels;
using WytSky.Mobile.Maui.Hadrein.Views;
using WytSky.Mobile.Maui.Hadrein.Views.SplashViews;
using WytSky.Mobile.Maui.Hadrein.Views.User;
using MyiOSSDK;

namespace WytSky.Mobile.Maui.Hadrein
{
    public partial class App : Application
    {
        public static ObservableCollection<StItem> CartItems = new ObservableCollection<StItem>();
        public static ResturantProfileVM ResturantProfileVM = new ResturantProfileVM();
        public static HomeVM HomeVM = new HomeVM();
        public static ShowPopupService ShowPopupService = ShowPopupService.Instance;

        public App()
        {
            try
            {
                InitializeComponent();
                SetAppLanguage(Settings.Language);
                if (Settings.IsLogedin)
                    Application.Current.MainPage = new NavigationPage(new SplashPage());
                else
                    Application.Current.MainPage = new NavigationPage(new OnBoardingPage());
                MyiOSSDK.MyiOSSDK x = new MyiOSSDK.MyiOSSDK();
                x.GetGreeting("test");
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                ExtensionLogMethods.LogExtension(ex, "", "App", "Constructor");
            }
        }

        public async static Task ShowConfirmationDeleteOrSendCachedCartItemsPopup()
        {
            if (Settings.CartItems.Count > 0)
            {
                var result = await App.Current.MainPage
                                    .DisplayAlert(AppResources.SharedResources.Text_Alert,
                                    AppResources.SharedResources.Text_YouHaveCartItems,
                                    AppResources.SharedResources.Text_Yes,
                                    AppResources.SharedResources.Text_No);
                if (result)
                {
                    App.CartItems = Settings.CartItems;
                    // used to show stack prices in cart page
                    App.ResturantProfileVM.CartDetailsVisibility = true;
                    await App.Current.MainPage.Navigation.PushModalAsync(new CartPage());
                }
                else
                {
                    Settings.CartItems = new ObservableCollection<StItem>();
                    App.CartItems = new ObservableCollection<StItem>();
                    Toast.ShowCustomToast(AppResources.SharedResources.Text_CartItemsDeleted);
                }
            }
        }

        public void SetAppLanguage(string lang)
        {
            try
            {
                CultureInfo culture;
                if (lang == "ar")
                    culture = new CultureInfo("ar-AE");
                else
                    culture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
                Thread.CurrentThread.CurrentUICulture.NumberFormat = new System.Globalization.CultureInfo("en").NumberFormat;
                Thread.CurrentThread.CurrentUICulture.DateTimeFormat = new System.Globalization.CultureInfo("en").DateTimeFormat;
                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;
                AppResources.SharedResources.Culture = culture;
                Settings.Language = lang;
            }
            catch (Exception ex)
            {
                ExtensionLogMethods.LogExtension(ex, null, "", "");
            }
        }
    }
}
