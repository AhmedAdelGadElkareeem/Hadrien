
using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;
using WytSky.Mobile.Maui.Hadrein.Helpers;
using WytSky.Mobile.Maui.Hadrein.Views;

namespace WytSky.Mobile.Maui.Hadrein.CustomControl;

public partial class CustomHeader : ContentView
{

    public CustomHeader()
    {
        InitializeComponent();
        FlowDirection = Settings.Language == "ar" ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
    }

    #region IsSearchVisible
    public static readonly BindableProperty IsSearchVisibleProperty =
        BindableProperty.Create(nameof(IsSearchVisible), typeof(bool), typeof(CustomHeader), false, BindingMode.TwoWay);
    public bool IsSearchVisible
    {
        get => (bool)GetValue(IsSearchVisibleProperty);
        set => SetValue(IsSearchVisibleProperty, value);
    }
    #endregion

    #region IsMenuVisible
    public static readonly BindableProperty IsMenuVisibleProperty =
        BindableProperty.Create(nameof(IsMenuVisible), typeof(bool), typeof(CustomHeader), false, BindingMode.TwoWay);
    public bool IsMenuVisible
    {
        get => (bool)GetValue(IsMenuVisibleProperty);
        set => SetValue(IsMenuVisibleProperty, value);
    }
    #endregion

    #region IsFilterVisible
    public static readonly BindableProperty IsFilterVisibleProperty =
        BindableProperty.Create(nameof(IsFilterVisible), typeof(bool), typeof(CustomHeader), true, BindingMode.TwoWay);
    public bool IsFilterVisible
    {
        get => (bool)GetValue(IsFilterVisibleProperty);
        set => SetValue(IsFilterVisibleProperty, value);
    }
    #endregion

    #region IsGridListVisible
    public static readonly BindableProperty IsGridListVisibleProperty =
        BindableProperty.Create(nameof(IsGridListVisible), typeof(bool), typeof(CustomHeader), false, BindingMode.TwoWay);
    public bool IsGridListVisible
    {
        get => (bool)GetValue(IsGridListVisibleProperty);
        set => SetValue(IsGridListVisibleProperty, value);
    }

    #endregion

    #region IsGrid
    public static readonly BindableProperty IsGridProperty =
        BindableProperty.Create(nameof(IsGrid), typeof(bool), typeof(CustomHeader), false,
            BindingMode.TwoWay, propertyChanging: OnChanged);
    public bool IsGrid
    {
        get => (bool)GetValue(IsGridProperty);
        set => SetValue(IsGridProperty, value);
    }
    static void OnChanged(BindableObject bindable, object oldValue, object newValue)
    {
        CustomHeader obj = bindable as CustomHeader;
        // Property changed implementation goes here
        if (obj != null && obj.IsGrid)
        {
            obj.gridImage.Source = "red_grid";
            obj.listImage.Source = "list";
        }
        else
        {
            obj.gridImage.Source = "grid";
            obj.listImage.Source = "red_list";
        }
    }
    #endregion

    #region ListCommand
    public static readonly BindableProperty ListCommandProperty =
    BindableProperty.Create(nameof(ListCommand),
        typeof(ICommand), typeof(CustomHeader), defaultValue: null, BindingMode.TwoWay);
    public ICommand ListCommand
    {
        get => (ICommand)GetValue(ListCommandProperty);
        set => SetValue(ListCommandProperty, value);
    }
    #endregion

    #region GridCommand
    public static readonly BindableProperty GridCommandProperty =
    BindableProperty.Create(nameof(GridCommand),
        typeof(ICommand), typeof(CustomHeader), defaultValue: null, BindingMode.TwoWay);
    public ICommand GridCommand
    {
        get => (ICommand)GetValue(GridCommandProperty);
        set => SetValue(GridCommandProperty, value);
    }
    #endregion

    #region SearchText
    public static readonly BindableProperty SearchTextProperty =
        BindableProperty.Create(nameof(SearchText), typeof(string), typeof(CustomHeader), null, BindingMode.TwoWay);
    public string SearchText
    {
        get => (string)GetValue(SearchTextProperty);
        set => SetValue(SearchTextProperty, value);
    }
    #endregion

    private void OpenCartPage(object sender, EventArgs e)
    {
        App.Current.MainPage.Navigation.PushModalAsync(new CartPage());
    }

    private void OpenMenu(object sender, TappedEventArgs e)
    {
        WeakReferenceMessenger.Default.Send("OpenMenu");
    }

    private void GoBack(object sender, TappedEventArgs e)
    {
        App.Current.MainPage.Navigation.PopModalAsync();
    }

    //private void GridClicked(object sender, TappedEventArgs e)
    //{
    //    gridImage.Source = "red_grid";
    //    listImage.Source = "list";
    //}
    //private void ListClicked(object sender, TappedEventArgs e)
    //{
    //    gridImage.Source = "grid";
    //    listImage.Source = "red_list";
    //}
}