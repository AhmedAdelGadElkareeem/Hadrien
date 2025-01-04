
namespace WytSky.Mobile.Maui.Hadrein.Services;

public class RequestProvider
{
    private static readonly ApiServices _requestProvider;
    public static ApiServices Current => _requestProvider;
    static RequestProvider()
    {
        _requestProvider = new ApiServices();
    }
}
