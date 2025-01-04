namespace WytSky.Mobile.Maui.Hadrein.Views.SubCategories;

public partial class MainSubcategoriesPage : BaseContentPage
{
	public MainSubcategoriesPage()
	{
		InitializeComponent(); 
        BindingContext = App.HomeVM;
    }

    private void Back(object sender, TappedEventArgs e)
    {
        App.Current.MainPage.Navigation.PopModalAsync();
    }
}