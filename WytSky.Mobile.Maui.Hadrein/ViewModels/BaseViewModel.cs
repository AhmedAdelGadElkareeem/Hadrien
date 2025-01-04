using CommunityToolkit.Mvvm.ComponentModel;
using WytSky.Mobile.Maui.Hadrein.Helpers;

namespace WytSky.Mobile.Maui.Hadrein.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        public string lang;

        [ObservableProperty]
        private bool cartDetailsVisibility = false;
        
        [ObservableProperty]
        private bool canExecute = true;

        public BaseViewModel()
        {
            Lang = Settings.Language;
        }

        public bool HasInternetConnection()
        {
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;
            return accessType == NetworkAccess.Internet;
        }

        public void SetUserNameAndPassword()
        {
            if (!Settings.IsLogedin)
            {
                Settings.Password = "sky365@365";
                Settings.UserName = "sky365";
            }
        }

        public void ResetUserNameAndPassword()
        {
            if (!Settings.IsLogedin)
            {
                Settings.Password = "";
                Settings.UserName = "";
            }
        }
    }
}
