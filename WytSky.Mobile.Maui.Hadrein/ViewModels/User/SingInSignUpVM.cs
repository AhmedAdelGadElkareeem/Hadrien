using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WytSky.Mobile.Maui.Hadrein.AppResources;
using WytSky.Mobile.Maui.Hadrein.Dtos;
using WytSky.Mobile.Maui.Hadrein.Helpers;
using WytSky.Mobile.Maui.Hadrein.Views;
using WytSky.Mobile.Maui.Hadrein.Views.Public;
using WytSky.Mobile.Maui.Hadrein.Views.User;

namespace WytSky.Mobile.Maui.Hadrein.ViewModels.User
{
    public partial class SignInSignUpVM : BaseViewModel
    {
        #region Properties
        [ObservableProperty]
        public StClientSocial client = new StClientSocial();

        [ObservableProperty]
        public string userName;

        [ObservableProperty]
        public string password;

        [ObservableProperty]
        public string newPassword;

        [ObservableProperty]
        public string verficationCode;

        [ObservableProperty]
        public string regVerficationCode;

        [ObservableProperty]
        public string verfication;

        [ObservableProperty]
        public bool isVisibleLogin;

        [ObservableProperty]
        public bool isVisibleRegister;

        [ObservableProperty]
        public bool isVisibleResetPassword;

        [ObservableProperty]
        public bool isVisibleReset;

        [ObservableProperty]

        public bool isVisibleResetSendCode;

        [ObservableProperty]
        public bool isVisibleNavigationBar;

        [ObservableProperty]
        public bool isVisibleAppleSignIn;

        [ObservableProperty]
        public string pageTitle;
        #endregion


        #region Constructor
        public SignInSignUpVM()
        {
            try
            {
                BackToLogin();

                if (string.IsNullOrEmpty(Settings.SocialID))
                {
                    UserName = Settings.UserName;
                    Password = Settings.Password;
                }
                if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
                {
                    //appleSignInService = DependencyService.Get<Services.IAppleSignInService>();
                    //IsVisibleAppleSignIn = appleSignInService.IsIos13();
                    //SignInWithAppleCommand = new Command(SignInWithApple);
                }
            }
            catch (Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                System.Diagnostics.Debug.WriteLine(ExceptionMseeage);
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "SignInSignUpVM", "Constructor");
            }
        }
        #endregion

        #region Methods
        [RelayCommand(CanExecute = nameof(CanExecute))]
        private async Task Login()
        {
            try
            {
                if (chekLogInData())
                {
                    var res = await APIs.ServiceApp.auth(UserName, Password);
                    if (res != null)
                    {
                        Settings.IsLogedin = true;
                        Settings.UserName = UserName;
                        Settings.Password = Password;
                        Settings.AuthoToken = res.access_token;
                        Settings.SocialID = "";
                        await GetClient();
                    }
                    else
                    {
                        Toast.ShowToastError(SharedResources.Msg_Error);
                    }
                }
            }
            catch (Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                System.Diagnostics.Debug.WriteLine(ExceptionMseeage);
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "SignInSignUpVM", "loginCommand");
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecute))]
        private async Task ContinueAsGuest()
        {
            try
            {
                UserName = "sky365";
                Password = "sky365@365";
                var res = await APIs.ServiceApp.auth(UserName, Password);
                if (res != null)
                {
                    Settings.IsLogedin = false;
                    Settings.UserName = UserName;
                    Settings.Password = Password;
                    Settings.AuthoToken = res.access_token;
                    Settings.SocialID = "";
                    await GetClient();
                }
                else
                {
                    Toast.ShowToastError(SharedResources.Msg_Error);
                }
            }
            catch (Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                System.Diagnostics.Debug.WriteLine(ExceptionMseeage);
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "SignInSignUpVM", "loginCommand");
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecute))]
        private void BackToLogin()
        {
            try
            {
                IsVisibleLogin = true;
                IsVisibleRegister = false;
            }
            catch (Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                System.Diagnostics.Debug.WriteLine(ExceptionMseeage);
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "SignInSignUpVM", "backToLogin");
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecute))]
        private async Task CreateAccount()
        {
            try
            {
                if (chekRegisterData())
                {
                    Settings.SocialID = "";
                    Settings.Password = "sky365@365";
                    Settings.UserName = "sky365";
                    Dictionary<string, string> keys = new Dictionary<string, string>()
                    {
                      { "Email", Client.Email}
                    };
                    var clinte = await APIs.ServiceClient.GetAll(filter: keys);
                    if (clinte != null && clinte.Count > 0)
                    {
                        Toast.ShowToastError(SharedResources.Msg_EmailAlreadyUse);
                        Settings.Password = "";
                        Settings.UserName = "";
                        return;
                    }
                    Dictionary<string, string> keys2 = new Dictionary<string, string>()
                    {
                            { "UserName", Client.Email}
                    };
                    var user = await APIs.ServiceAspNetUser.GetAll(filter: keys2);
                    if (user != null && user.Count > 0)
                    {
                        Toast.ShowToastError(SharedResources.Msg_EmailAlreadyUse);
                        Settings.Password = "";
                        Settings.UserName = "";
                        return;
                    }
                    Random _random = new Random();
                    RegVerficationCode = _random.Next().ToString();
                    var NewClient = await APIs.ServiceClient.SaveNew(new Dictionary<string, object>()
                        {
                            {"ClientTypeID","2" },
                            {"FullName",Client.FullName },
                            {"Phone",Client.Phone },
                            {"Email",Client.Email },
                            {"PasswordHash",SecurityHelper.EncodePasswordmosso(Client.HasPassword) },
                            //{"FbToken",Plugin.FirebasePushNotification.CrossFirebasePushNotification.Current.Token },
                        });
                    if (NewClient != null && NewClient.rowsAffected > 0)
                    {
                        var NewUser = await APIs.ServiceAspNetUser.SaveNew(new Dictionary<string, object>()
                            {
                                {"Email",Client.Email },
                                {"UserName",Client.Email },
                                {"PhoneNumber",Client.Phone },
                                {"VerficationCode",RegVerficationCode },
                                {"PasswordHash",SecurityHelper.EncodePasswordmosso(Client.HasPassword) },
                                {"UserTypeID","4" },
                                {"Confirmed","false" },
                                {"EmailConfirmed","false" },
                                {"IsApproved","true" },
                                {"PhoneNumberConfirmed","false" },
                                {"LastLoginDate",DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") },
                                {"LastActivityDate",DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") },
                                {"LastPasswordChangedDate",DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") },
                                {"LastLockedOutDate",DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") },
                                {"LastLockoutDate",DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") },
                            });
                        if (NewUser != null && NewUser.rowsAffected > 0)
                        {
                            await Login(Client.Email, Client.HasPassword);
                        }
                        else
                        {
                            Toast.ShowToastError(SharedResources.Msg_Error);
                            Settings.Password = "";
                            Settings.UserName = "";
                        }
                    }
                    else
                    {
                        Toast.ShowToastError(SharedResources.Msg_Error);
                        Settings.Password = "";
                        Settings.UserName = "";
                    }
                }
            }
            catch (Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                System.Diagnostics.Debug.WriteLine(ExceptionMseeage);
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "SignInSignUpVM", "createAccountCommand");
            }
        }
        [RelayCommand(CanExecute = nameof(CanExecute))]
        private void Resgister()
        {
            try
            {
                IsVisibleLogin = false;
                IsVisibleRegister = true;
                IsVisibleResetSendCode = false;
                IsVisibleReset = false;
                IsVisibleResetPassword = false;
                IsVisibleNavigationBar = true;
                PageTitle = SharedResources.Text_SignUp;
            }
            catch (Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                System.Diagnostics.Debug.WriteLine(ExceptionMseeage);
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "SignInSignUpVM", "registerCommand");
            }
        }
        public static async Task Login(string userName, string password)
        {
            try
            {
                var res = await APIs.ServiceApp.auth(userName, password);
                if (res != null)
                {
                    Settings.IsLogedin = true;
                    Settings.UserName = userName;
                    Settings.Password = password;
                    Settings.AuthoToken = res.access_token;
                    // Plugin.FirebasePushNotification.CrossFirebasePushNotification.Current.OnTokenRefresh += App.OnTokenRefresh;
                    await GetClient();
                }
                else
                {
                    Toast.ShowToastError(SharedResources.Msg_Error);
                }
            }
            catch (System.Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                System.Diagnostics.Debug.WriteLine(ExceptionMseeage);
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "SignInSignUpVM", "Login()");
            }
        }
        private async void ResetSendCode()
        {
            try
            {
                if (chekSendCodeData())
                {
                    var res = await APIs.ServiceClient.GetCLientByEmail(UserName);
                    if (res != null && res.success)
                    {
                        if (!string.IsNullOrEmpty(res.data.SocialID))
                        {
                            Toast.ShowToastError(SharedResources.Msg_SocialEmail);
                            BackToLogin();
                            return;
                        }
                    }
                    else
                    {
                        Toast.ShowToastError(SharedResources.Msg_EmailNotValid);
                        return;
                    }
                    var res1 = await APIs.ServiceClient.ResendVerficationCode(new
                    {
                        email = UserName
                    });
                    if (res1 != null && res1.success && res1.data != null)
                    {
                        IsVisibleLogin = false;
                        IsVisibleRegister = false;
                        IsVisibleReset = false;
                        IsVisibleResetPassword = false;
                        IsVisibleResetSendCode = true;
                        IsVisibleNavigationBar = true;
                        Verfication = res1.data.VerficationCode;
                    }
                    else
                    {
                        Toast.ShowToastError(SharedResources.Msg_NotValidEmail);
                    }
                }
            }
            catch (Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                System.Diagnostics.Debug.WriteLine(ExceptionMseeage);
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "SignInSignUpVM", "createAccountCommand");
            }
        }
        private void CommandVerficationCode()
        {
            try
            {
                if (Verfication == VerficationCode)
                {
                    IsVisibleLogin = false;
                    IsVisibleRegister = false;
                    IsVisibleResetPassword = false;
                    IsVisibleResetSendCode = false;
                    IsVisibleReset = true;
                    IsVisibleNavigationBar = true;
                }
                else
                {
                    Toast.ShowToastError(SharedResources.Msg_Error);
                }
            }
            catch (Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                System.Diagnostics.Debug.WriteLine(ExceptionMseeage);
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "SignInSignUpVM", "createAccountCommand");
            }
        }
        [RelayCommand(CanExecute = nameof(CanExecute))]
        private void ResetPassword()
        {
            try
            {
                IsVisibleLogin = false;
                IsVisibleRegister = true;
            }
            catch (Exception ex)
            {
                ExtensionLogMethods.LogExtension(ex, "", "SignInSignUpVM", "ResetPassword");
            }
        }
        private void FacebookLogin()
        {
            try
            {
                //_facebookManager.Login(OnLoginComplete);
            }
            catch (Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                System.Diagnostics.Debug.WriteLine(ExceptionMseeage);
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "SignInSignUpVM", "FacebookLogin");
            }
        }
        /*private async void OnLoginComplete(FacebookProfile facebookProfile, string message)
        {
            try
            {
                if (facebookProfile != null)
                {
                    string Social = facebookProfile.Id + "@Wytsky";
                    string Password = facebookProfile.Id + "@" + facebookProfile.Id + ".Wytsky";
                    Settings.SocialID = "";
                    Settings.Password = "sky365@365";
                    Settings.UserName = "sky365";
                    Dictionary<string, string> keys = new Dictionary<string, string>()
                        {
                            { "SocialID", Social}
                        };
                    var clinte = await APIs.ServiceClient.GetAll(filter: keys);
                    if (clinte != null && clinte.Count > 0)
                    {
                        Settings.SocialID = Social;
                        await Login(Social, Password);
                        return;
                    }
                    else
                    {
                        Dictionary<string, string> keys2 = new Dictionary<string, string>()
                            {
                                { "UserName", Social}
                            };
                        var user = await APIs.ServiceAspNetUser.GetAll(filter: keys2);
                        if (user != null && user.Count > 0)
                        {
                            Toast.ShowToastError(SharedResources.Msg_EmailAlreadyUse);
                            Settings.Password = "";
                            Settings.UserName = "";
                            return;
                        }
                        Random _random = new Random();
                        RegVerficationCode = _random.Next().ToString();
                        var NewClient = await APIs.ServiceClient.SaveNew(new Dictionary<string, object>()
                            {
                                {"ClientTypeID","2" },
                                { "SocialID", Social},
                                {"FullName",facebookProfile.FirstName + " " + facebookProfile.LastName },
                                {"Phone",facebookProfile.phoneNumber },
                                {"Email",facebookProfile.Email },
                                {"PasswordHash",SecurityHelper.EncodePasswordmosso(Password) },
                                {"FbToken",Plugin.FirebasePushNotification.CrossFirebasePushNotification.Current.Token },
                            });
                        if (NewClient != null && NewClient.rowsAffected > 0)
                        {
                            var NewUser = await APIs.ServiceAspNetUser.SaveNew(new Dictionary<string, object>()
                                {
                                    {"Email",facebookProfile.Email },
                                    {"UserName",Social },
                                    {"PhoneNumber",facebookProfile.phoneNumber },
                                    {"VerficationCode",RegVerficationCode },
                                    {"PasswordHash",SecurityHelper.EncodePasswordmosso(Password) },
                                    {"UserTypeID","4" },
                                    {"Confirmed","false" },
                                    {"EmailConfirmed","false" },
                                    {"IsApproved","true" },
                                    {"PhoneNumberConfirmed","false" },
                                    {"LastLoginDate",DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") },
                                    {"LastActivityDate",DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") },
                                    {"LastPasswordChangedDate",DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") },
                                    {"LastLockedOutDate",DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") },
                                    {"LastLockoutDate",DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") },
                                });
                            if (NewUser != null && NewUser.rowsAffected > 0)
                            {
                                Settings.SocialID = Social;
                                await Login(Social, Password);
                            }
                            else
                            {
                                Toast.ShowToastError(SharedResources.Msg_Error);
                                Settings.Password = "";
                                Settings.UserName = "";
                            }
                        }
                        else
                        {
                            Toast.ShowToastError(SharedResources.Msg_Error);
                            Settings.Password = "";
                            Settings.UserName = "";
                        }
                    }
                }
                else
                {
                    Toast.ShowToastError(SharedResources.Msg_Error);
                }
            }
            catch (System.Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                System.Diagnostics.Debug.WriteLine(ExceptionMseeage);
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "SignInSignUpVM", "OnLoginComplete()");
            }
        }*/
        private async void SignInWithApple()
        {
            /*try
            {
                var account = await appleSignInService.SignInAsync();
                if (account != null)
                {
                    string Social = account.UserId + "@Wytsky";
                    string Password = account.UserId + "@" + account.UserId + ".Wytsky";
                    var res1 = await APIs.ServiceClient.SignUpSocial(new Dtos.StClientSocial()
                    {
                        Email = account.Email,
                        FullName = account.Name,
                        SocialID = Social,
                        //SocialID = facebookProfile.Id,
                        HasPassword = Password,
                    });
                    Settings.SocialID = Social;
                    await Login(Social, Password);
                }
                else
                {
                    Toast.ShowToastError(SharedResources.Msg_Error);
                }
            }
            catch (System.Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                System.Diagnostics.Debug.WriteLine(ExceptionMseeage);
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "SignInSignUpVM", "SignInWithApple()");
            }*/
        }
        private async void ChangePassword()
        {
            try
            {
                if (chekData())
                {
                    var res = await APIs.ServiceClient.RestPassword(new
                    {
                        userName = UserName,
                        newPassword = NewPassword,
                    });
                    if (res != null && res.success)
                    {
                        BackToLogin();
                    }
                    else
                    {
                        Toast.ShowToastError(SharedResources.Msg_Error);
                    }
                }
            }
            catch (Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                System.Diagnostics.Debug.WriteLine(ExceptionMseeage);
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "ChangePasswordVM", "UpdateData");
            }
        }
        private bool chekData()
        {
            try
            {
                if (string.IsNullOrEmpty(NewPassword) || !Behaviors.PasswordValidBehavior.RegexIsMatch(NewPassword))
                {
                    Toast.ShowToastError(SharedResources.Msg_NotValidNewPassword);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                System.Diagnostics.Debug.WriteLine(ExceptionMseeage);
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "ChangePasswordVM", "chekData");
                return false;
            }
        }
        private bool chekLogInData()
        {
            try
            {
                if (string.IsNullOrEmpty(UserName))
                {
                    Toast.ShowToastError(SharedResources.Msg_NotValidUserName);
                    return false;
                }
                else if (string.IsNullOrEmpty(Password))
                {
                    Toast.ShowToastError(SharedResources.Msg_NotValidPassword);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                System.Diagnostics.Debug.WriteLine(ExceptionMseeage);
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "SignInSignUpVM", "update");
                return false;
            }
        }
        private bool chekRegisterData()
        {
            try
            {
                if (string.IsNullOrEmpty(Client.FullName) || Client.FullName.Length < 5)
                {
                    Toast.ShowToastError(SharedResources.Msg_NotValidName);
                    return false;
                }
                else if (string.IsNullOrEmpty(Client.Email) || !Behaviors.EmailValidBehavior.RegexIsMatch(Client.Email))
                {
                    Toast.ShowToastError(SharedResources.Msg_NotValidEmail);
                    return false;
                }
                else if (string.IsNullOrEmpty(Client.HasPassword) || !Behaviors.PasswordValidBehavior.RegexIsMatch(Client.HasPassword))
                {
                    Toast.ShowToastError(SharedResources.Msg_PasswordValdation);
                    return false;
                }
                else if (string.IsNullOrEmpty(Client.ConfirmPassword) || Client.ConfirmPassword != Client.HasPassword)
                {
                    Toast.ShowToastError(SharedResources.Msg_PasswordNotMatch);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                System.Diagnostics.Debug.WriteLine(ExceptionMseeage);
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "SignInSignUpVM", "update");
                return false;
            }
        }
        private bool chekSendCodeData()
        {
            try
            {
                if (string.IsNullOrEmpty(UserName) || !Behaviors.EmailValidBehavior.RegexIsMatch(UserName))
                {
                    Toast.ShowToastError(SharedResources.Msg_NotValidEmail);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                System.Diagnostics.Debug.WriteLine(ExceptionMseeage);
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "SignInSignUpVM", "update");
                return false;
            }
        }
        private static async Task GetClient()
        {
            try
            {
                Dictionary<string, string> keys = new Dictionary<string, string>();
                if (string.IsNullOrEmpty(Settings.SocialID))
                {
                    keys.Add("Email", Settings.UserName);
                }
                else
                {
                    keys.Add("SocialID", Settings.SocialID);
                }
                var res = await APIs.ServiceClient.GetAll(filter: keys);
                if (res != null && res.Count > 0)
                {
                    Settings.ClientId = (long)res[0].ClientID;
                    Settings.ClientName = res[0].FullName;
                    Settings.ClientEmail = res[0].Email;
                    Settings.ClientPhone = res[0].Phone;
                    Settings.Client = res[0];
                    await APIs.ServiceClient.UpdateCLientFbToken();
                    App.Current.MainPage = new NavigationPage(new MenuPage());
                }
                else
                {
                    Settings.IsLogedin = false;
                    Toast.ShowToastError(SharedResources.Msg_UserNoValid);
                }
            }
            catch (Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                System.Diagnostics.Debug.WriteLine(ExceptionMseeage);
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "SignInSignUpVM", "getClient");
            }
        }
        #endregion
    }
}