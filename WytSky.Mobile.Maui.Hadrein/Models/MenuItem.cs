using System;
using System.Collections.Generic;

namespace WytSky.Mobile.Maui.Hadrein.Models
{

    public class MenuItem
    {
        public string Title { get; set; }
        public string Icon { get; set; } = "IconMenu.png";
        public Type TargetType { get; set; }
        public Command CommandE { get; set; }
    }
}