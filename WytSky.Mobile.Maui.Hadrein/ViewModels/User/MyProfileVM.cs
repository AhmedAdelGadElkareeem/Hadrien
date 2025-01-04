using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WytSky.Mobile.Maui.Hadrein.Helpers;

namespace WytSky.Mobile.Maui.Hadrein.ViewModels.User
{
    public partial class MyProfileVM : BaseViewModel
    {
        [ObservableProperty]
        public string name;

        [ObservableProperty]
        public string email;

        [ObservableProperty]
        public string phone;

        public MyProfileVM()
        {
            Name = Settings.ClientName;
            Email = Settings.ClientEmail;
            Phone = Settings.ClientPhone;
        }
    }
}
