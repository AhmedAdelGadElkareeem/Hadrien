using WytSky.Mobile.Maui.Hadrein.Helpers;
using WytSky.Mobile.Maui.Hadrein.ViewModels;

namespace WytSky.Mobile.Maui.Hadrein.CustomControl;

public partial class BackView : BaseContentView
{
	public BackView()
	{
		InitializeComponent();
	}

    private void Back(object sender, TappedEventArgs e)
    {
		App.Current.MainPage.Navigation.PopModalAsync();
    }
}