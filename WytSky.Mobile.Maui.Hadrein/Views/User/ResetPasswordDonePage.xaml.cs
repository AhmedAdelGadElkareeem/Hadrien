namespace WytSky.Mobile.Maui.Hadrein.Views.User;

public partial class ResetPasswordDonePage : BaseContentPage
{
	public ResetPasswordDonePage(bool afterVerificationCode)
	{
		InitializeComponent();

		// this boolean becuase we have 2 pages with the same design 
		// one after verification code and one after reset password

		if (!afterVerificationCode)
		{
			backView.back.IsVisible = false;
			profileReadyToUse.Text = AppResources.SharedResources.Text_PasswordResetSuccessfully;
			loginLabel.Text = AppResources.SharedResources.Text_Login;
        }
	}
}