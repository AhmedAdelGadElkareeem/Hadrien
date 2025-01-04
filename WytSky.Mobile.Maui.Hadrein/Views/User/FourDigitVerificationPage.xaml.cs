using WytSky.Mobile.Maui.Hadrein.Helpers;

namespace WytSky.Mobile.Maui.Hadrein.Views.User;

public partial class FourDigitVerificationPage : BaseContentPage
{
	public FourDigitVerificationPage()
	{
		InitializeComponent();
		phoneNumber.Text = Settings.ClientPhone;
    }
}