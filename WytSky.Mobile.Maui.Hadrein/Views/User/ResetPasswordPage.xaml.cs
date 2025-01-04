namespace WytSky.Mobile.Maui.Hadrein.Views.User;

public partial class ResetPasswordPage : BaseContentPage
{
	public ResetPasswordPage()
	{
		InitializeComponent();
	}

    private void ShowPassword(object sender, TappedEventArgs e)
    {
		password.IsPassword = !password.IsPassword;
    }

    private void ShowRePassword(object sender, TappedEventArgs e)
    {
        rePassword.IsPassword = !rePassword.IsPassword;
    }
}