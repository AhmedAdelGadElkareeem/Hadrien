using WytSky.Mobile.Maui.Hadrein.Helpers;

namespace WytSky.Mobile.Maui.Hadrein.CustomControl;

public partial class EmptyView : BaseContentView
{
	public EmptyView()
	{
        try
        {
            InitializeComponent();
            this.FlowDirection = Settings.Language == "ar" ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;

            if (DeviceInfo.Current.Platform == DevicePlatform.iOS && Settings.Language == "ar")
                stack.RotationY = 180;
        }
        catch (Exception ex)
        {
            ExtensionLogMethods.LogExtension(ex, "", "EmptyView", "Constractor");
        }
    }
}