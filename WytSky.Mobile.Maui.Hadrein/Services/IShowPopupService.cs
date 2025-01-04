using Mopups.Pages;

namespace WytSky.Mobile.Maui.Hadrein.Services
{
    public interface IShowPopupService
    {
        Task<IDisposable> Show(PopupPage popupPage);
        void Dispose();
    }
}
