using CommunityToolkit.Maui;
using FFImageLoading.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Platform;
using Mopups.Hosting;
using SkiaSharp.Views.Maui.Controls.Hosting;
using WytSky.Mobile.Maui.Hadrein.CustomControl.Borderless;
using WytSky.Mobile.Maui.Hadrein.Services;
using WytSky.Mobile.Maui.Hadrein.Services.Implementation;

namespace WytSky.Mobile.Maui.Hadrein
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseSkiaSharp() // to use animation
                .ConfigureMopups() // to use popups
                .UseFFImageLoading() // to use FFImageLoading
                .ConfigureFonts(fonts =>
                {
                    //fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    //fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Product_Sans_Bold.ttf", "ProductSansBold");
                    fonts.AddFont("Product_Sans_Regular.ttf", "ProductSansRegular");
                })
                .ConfigureMauiHandlers((handlers) =>
                {
                }).Services.AddTransient<IShowPopupService, ShowPopupService>();

#if DEBUG
             builder.Logging.AddDebug();
#endif
            addHandlers();
            return builder.Build();
        }

        public static void addHandlers()
        {
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(BorderlessEntry), (handler, view) =>
            {
                if (view is BorderlessEntry)
                {
#if ANDROID
                    handler.PlatformView.SetBackgroundColor(Colors.Transparent.ToPlatform());
#elif IOS
                    handler.PlatformView.BackgroundColor = Colors.Transparent.ToPlatform();
                    //handler.PlatformView.Layer.BackgroundColor = Colors.Transparent.ToPlatform();
                    //handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#elif WINDOWS
                handler.PlatformView.Background = Colors.Transparent.ToPlatform();
#endif
                }
            });

            Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping(nameof(BorderlessEditor), (handler, view) =>
            {
                if (view is BorderlessEditor)
                {
#if ANDROID
                    handler.PlatformView.SetBackgroundColor(Colors.Transparent.ToPlatform());
#elif IOS
                    handler.PlatformView.BackgroundColor = Colors.Transparent.ToPlatform();
                    //handler.PlatformView.Layer.BackgroundColor = Colors.Transparent.ToPlatform();
                    //handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#elif WINDOWS
                handler.PlatformView.Background = Colors.Transparent.ToPlatform();
#endif
                }
            });

            Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping(nameof(BorderlessDatePicker), (handler, view) =>
            {
                if (view is BorderlessDatePicker)
                {
#if ANDROID
                    handler.PlatformView.SetBackgroundColor(Colors.Transparent.ToPlatform());
#elif IOS
                    handler.PlatformView.BackgroundColor = Colors.Transparent.ToPlatform();
                    //handler.PlatformView.Layer.BackgroundColor = Colors.Transparent.ToPlatform();
                    //handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#elif WINDOWS
                handler.PlatformView.Background = Colors.Transparent.ToPlatform();
#endif
                }
            });
        }
    }
}
