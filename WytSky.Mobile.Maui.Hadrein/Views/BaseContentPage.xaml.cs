using WytSky.Mobile.Maui.Hadrein.Helpers;

namespace WytSky.Mobile.Maui.Hadrein.Views;

public partial class BaseContentPage : ContentPage
{
    public BaseContentPage()
    {
        try
        {
            InitializeComponent();
            FlowDirection = Settings.Language == "ar" ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
        }
        catch (Exception ex)
        {
            ExtensionLogMethods.LogExtension(ex, "", "BaseContentPage", "Constructor");
        }
    }

    protected override bool OnBackButtonPressed()
    {
        base.OnBackButtonPressed();
        App.Current.MainPage.Navigation.PopModalAsync();
        return true;
    }
}