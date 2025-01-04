

namespace WytSky.Mobile.Maui.Hadrein.Dtos
{
    public class MenuModelItem
    {
        public string Title { get; set; }
        public string Icon { get; set; } = "IconMenu.png";
        public Type TargetType { get; set; }
        public Command CommandE { get; set; }
    }
}