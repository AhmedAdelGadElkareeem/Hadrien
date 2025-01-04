using System.Windows.Input;
using WytSky.Mobile.Maui.Hadrein.Dtos;

namespace WytSky.Mobile.Maui.Hadrein.DataTemplates;

public partial class CatgeoryListDT : ContentView
{
    public CatgeoryListDT()
    {
        InitializeComponent();
    }

    #region ModelData

    public static readonly BindableProperty ModelDataProperty =
    BindableProperty.Create(nameof(ModelData),
        typeof(StCatgeory), typeof(CatgeoryListDT), default, BindingMode.TwoWay);
    // Gets or sets Command value  
    public StCatgeory ModelData
    {
        get => (StCatgeory)GetValue(ModelDataProperty);
        set => SetValue(ModelDataProperty, value);
    }

    #endregion

    #region TapCommand

    public static readonly BindableProperty TapCommandProperty =
    BindableProperty.Create(nameof(TapCommand),
        typeof(ICommand), typeof(CatgeoryListDT),
        defaultValue: null, BindingMode.TwoWay);
    // Gets or sets Command value  
    public ICommand TapCommand
    {
        get => (ICommand)GetValue(TapCommandProperty);
        set => SetValue(TapCommandProperty, value);
    }

    #endregion

    #region ImageAspect

    public static readonly BindableProperty ImageAspectProperty =
    BindableProperty.Create(nameof(ImageAspect),
        typeof(Aspect), typeof(CatgeoryListDT),
        defaultValue: Aspect.AspectFill, BindingMode.TwoWay);
    // Gets or sets Command value  
    public Aspect ImageAspect
    {
        get => (Aspect)GetValue(ImageAspectProperty);
        set => SetValue(ImageAspectProperty, value);
    }

    #endregion
    
    #region IsVisibleTime

    public static readonly BindableProperty IsVisibleTimeProperty =
    BindableProperty.Create(nameof(IsVisibleTime),
        typeof(bool), typeof(CatgeoryListDT),
        defaultValue: false, BindingMode.TwoWay);
    // Gets or sets Command value  
    public bool IsVisibleTime
    {
        get => (bool)GetValue(IsVisibleTimeProperty);
        set => SetValue(IsVisibleTimeProperty, value);
    }

    #endregion

    #region IsVisibleDeliveryPrice

    public static readonly BindableProperty IsVisibleDeliveryPriceProperty =
    BindableProperty.Create(nameof(IsVisibleDeliveryPrice),
        typeof(bool), typeof(CatgeoryListDT),
        defaultValue: false, BindingMode.TwoWay);
    // Gets or sets Command value  
    public bool IsVisibleDeliveryPrice
    {
        get => (bool)GetValue(IsVisibleDeliveryPriceProperty);
        set => SetValue(IsVisibleDeliveryPriceProperty, value);
    }

    #endregion
    
    #region IsVisibleRate

    public static readonly BindableProperty IsVisibleRateProperty =
    BindableProperty.Create(nameof(IsVisibleRate),
        typeof(bool), typeof(CatgeoryListDT),
        defaultValue: false, BindingMode.TwoWay);
    // Gets or sets Command value  
    public bool IsVisibleRate
    {
        get => (bool)GetValue(IsVisibleRateProperty);
        set => SetValue(IsVisibleRateProperty, value);
    }

    #endregion

    #region ViewTapped

    public event EventHandler<TappedEventArgs>? ViewTapped;
    protected virtual void OnViewTapped(object sender, TappedEventArgs e)
    {
        ViewTapped?.Invoke(this, e);
    }

    #endregion

}