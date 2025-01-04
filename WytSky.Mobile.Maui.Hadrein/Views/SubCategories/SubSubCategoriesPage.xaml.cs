using System.Collections.ObjectModel;
using WytSky.Mobile.Maui.Hadrein.Dtos;

namespace WytSky.Mobile.Maui.Hadrein.Views.SubCategories;

public partial class SubSubCategoriesPage : BaseContentPage
{
	public SubSubCategoriesPage()
	{
		InitializeComponent();
        BindingContext = App.HomeVM;
	}
}