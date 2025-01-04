using WytSky.Mobile.Maui.Hadrein.Helpers;
using WytSky.Mobile.Maui.Hadrein.Views.Public;
using WytSky.Mobile.Maui.Hadrein.Views.User;

namespace WytSky.Mobile.Maui.Hadrein.Views.SplashViews;

public partial class SplashPage : BaseContentPage
{
    public SplashPage()
    {
        try
        {
            InitializeComponent();
        }
        catch (Exception ex)
        {
            ExtensionLogMethods.LogExtension(ex, "", "SplashPage", "Constructor");
        }
    }

    protected async override void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            await Task.Delay(6000);
            NavigateToMainPage();
        }
        catch (Exception ex)
        {
            ExtensionLogMethods.LogExtension(ex, "", "SplashPage", "OnAppearing");
        }
    }

    void NavigateToMainPage()
    {
        if (Settings.IsLogedin)
            Application.Current.MainPage = new NavigationPage(new MenuPage());
        else
            Application.Current.MainPage = new NavigationPage(new SignInSignUpPage());
    }
}