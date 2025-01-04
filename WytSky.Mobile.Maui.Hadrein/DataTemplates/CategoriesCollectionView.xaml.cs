using System.Collections.ObjectModel;
using System.Windows.Input;
using WytSky.Mobile.Maui.Hadrein.Dtos;

namespace WytSky.Mobile.Maui.Hadrein.DataTemplates;

public partial class CategoriesCollectionView : ContentView
{
	public CategoriesCollectionView()
	{
		InitializeComponent();
	}

    // Categories
    #region Categories

    public static readonly BindableProperty CategoriesProperty =
    BindableProperty.Create(nameof(Categories),
        typeof(ObservableCollection<StCatgeory>), typeof(CategoriesCollectionView), default, BindingMode.TwoWay);
    // Gets or sets Command value  
    public ObservableCollection<StCatgeory> Categories
    {
        get => (ObservableCollection<StCatgeory>)GetValue(CategoriesProperty);
        set => SetValue(CategoriesProperty, value);
    }

    #endregion

    #region TapCommand

    public static readonly BindableProperty TapCommandProperty =
    BindableProperty.Create(nameof(TapCommand),
        typeof(ICommand), typeof(CategoriesCollectionView),
        defaultValue: null, BindingMode.TwoWay);
    // Gets or sets Command value  
    public ICommand TapCommand
    {
        get => (ICommand)GetValue(TapCommandProperty);
        set => SetValue(TapCommandProperty, value);
    }

    #endregion
}