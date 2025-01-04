using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WytSky.Mobile.Maui.Hadrein.Dtos;
using WytSky.Mobile.Maui.Hadrein.Views.SplashViews;
using WytSky.Mobile.Maui.Hadrein.Views.User;

namespace WytSky.Mobile.Maui.Hadrein.ViewModels
{
    public partial class CarouselVM : BaseViewModel
    {
        [ObservableProperty]
        public List<StCarouselItem> carouselItems;

        [ObservableProperty]
        public int positionSelected = 0;

        public CarouselVM()
        {
            CarouselItems = new List<StCarouselItem>()
            {
                new StCarouselItem() {Title="Find and Get\r\nYour Best Food",
                    Description="Find the most delicious food\r\nwith the best quality and free delivery here",
                    ImageNumber="skip_1",Image="splash_1"},

                new StCarouselItem() {Title="Find and Get\r\nYour Best Food",
                    Description="Find the most delicious food\r\nwith the best quality and free delivery here",
                    ImageNumber="skip_2",Image="splash_2"},

                new StCarouselItem() {Title="Find and Get\r\nYour Best Food",
                    Description="Find the most delicious food\r\nwith the best quality and free delivery here",
                    ImageNumber="skip_3",Image="splash_3"},

                new StCarouselItem() {Title="Find and Get\r\nYour Best Food",
                    Description="Find the most delicious food\r\nwith the best quality and free delivery here",
                    ImageNumber="skip_4",Image="splash_4" },
            };
        }

        [RelayCommand(CanExecute = nameof(CanExecute))]
        public void ChangePosition()
        {
            if (PositionSelected < 3)
                PositionSelected++;
            else if (PositionSelected == 3)
            {
                App.Current.MainPage = new NavigationPage(new SplashPage());
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecute))]
        public void Skip()
        {
            App.Current.MainPage = new NavigationPage(new SplashPage());
        }


    }
}