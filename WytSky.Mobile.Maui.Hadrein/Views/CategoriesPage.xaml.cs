namespace WytSkyECommerceTalabat.Views;

public partial class CategoriesPage : BaseContentPage
{
	public CategoriesPage()
	{
		InitializeComponent();
        BindingContext = App.HomeVM;
    }

    private void GoBack(object sender, TappedEventArgs e)
    {
		try
		{
			App.Current.MainPage.Navigation.PopModalAsync();
		}
		catch (Exception ex)
		{
			ExtensionLogMethods.LogExtension(ex,"", "SubCategoriesPage", "GoBack");
		}
    }
}