using WytSky.Mobile.Maui.Hadrein.ViewModels;

namespace WytSky.Mobile.Maui.Hadrein.Views.SplashViews;

public partial class OnBoardingPage : BaseContentPage
{
	public OnBoardingPage()
	{
		InitializeComponent();
		BindingContext = new CarouselVM();
    }
}