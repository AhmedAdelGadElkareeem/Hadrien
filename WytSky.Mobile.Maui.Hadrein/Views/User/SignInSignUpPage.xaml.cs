using WytSky.Mobile.Maui.Hadrein.Helpers;
using WytSky.Mobile.Maui.Hadrein.ViewModels.User;

namespace WytSky.Mobile.Maui.Hadrein.Views.User;

public partial class SignInSignUpPage : BaseContentPage
{
    private SignInSignUpVM signInSignUpVM = new SignInSignUpVM();
    public SignInSignUpPage()
    {
        try
        {
            InitializeComponent();
            BindingContext = signInSignUpVM;
        }
        catch (Exception ex)
        {
            ExtensionLogMethods.LogExtension(ex, "", "SignInSignUpPage", "Constructor");
        }
    }

    private void Back(object sender, TappedEventArgs e)
    {
        signInSignUpVM.IsVisibleLogin = true;
        signInSignUpVM.IsVisibleRegister = false;
    }

    private void ShowLoginPassword(object sender, TappedEventArgs e)
    {
        loginPassword.IsPassword = !loginPassword.IsPassword;
    }

    private void ShowRegisterPassword(object sender, TappedEventArgs e)
    {
        Password.IsPassword = !Password.IsPassword;
    }

    private void ShowRePassword(object sender, TappedEventArgs e)
    {
        rePassword.IsPassword = !rePassword.IsPassword;
    }
}