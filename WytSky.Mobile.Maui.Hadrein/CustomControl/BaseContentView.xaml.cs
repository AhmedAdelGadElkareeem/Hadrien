using WytSky.Mobile.Maui.Hadrein.Helpers;
using WytSky.Mobile.Maui.Hadrein.ViewModels;

namespace WytSky.Mobile.Maui.Hadrein.CustomControl;

public partial class BaseContentView : ContentView
{
	public BaseContentView()
	{
		InitializeComponent();
        BindingContext = new BaseViewModel();
        this.FlowDirection = Settings.Language == "ar" ?
                                 FlowDirection.RightToLeft : FlowDirection.LeftToRight;
    }
}