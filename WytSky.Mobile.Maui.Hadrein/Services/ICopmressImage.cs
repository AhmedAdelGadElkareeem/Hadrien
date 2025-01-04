
namespace WytSky.Mobile.Maui.Hadrein.Services
{
    public interface ICopmressImage
    {
        byte[] ResizeImage(byte[] imageData, float width, float height);
    }
}
