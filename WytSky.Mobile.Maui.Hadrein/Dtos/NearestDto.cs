using System.Collections.ObjectModel;

namespace WytSky.Mobile.Maui.Hadrein.Dtos
{
    public class NearestDto
    {
        public string CategoryName { get; set; }
        public ObservableCollection<StCatgeory> Nearests { get; set; }
    }
}
