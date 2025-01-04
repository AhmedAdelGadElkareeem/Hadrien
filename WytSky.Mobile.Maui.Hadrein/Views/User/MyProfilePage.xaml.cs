using WytSky.Mobile.Maui.Hadrein.ViewModels.User;

namespace WytSky.Mobile.Maui.Hadrein.Views.User;

public partial class MyProfilePage : BaseContentPage
{
	MyProfileVM MyProfileVM = new MyProfileVM();
	public MyProfilePage()
	{
		InitializeComponent();
		BindingContext = MyProfileVM;
	}
}