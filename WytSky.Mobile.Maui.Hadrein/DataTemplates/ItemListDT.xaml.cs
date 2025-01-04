using System.Windows.Input;
using WytSky.Mobile.Maui.Hadrein.Dtos;

namespace WytSky.Mobile.Maui.Hadrein.DataTemplates;

public partial class ItemListDT : ContentView
{
    public ItemListDT()
    {
        InitializeComponent();
    }

    #region ModelData

    public static readonly BindableProperty ModelDataProperty =
    BindableProperty.Create(nameof(ModelData),
        typeof(StItem), typeof(ItemListDT), default, BindingMode.TwoWay);
    // Gets or sets Command value  
    public StItem ModelData
    {
        get => (StItem)GetValue(ModelDataProperty);
        set => SetValue(ModelDataProperty, value);
    }

    #endregion

    #region IncreaseQuantityCommand

    public static readonly BindableProperty IncreaseQuantityCommandProperty =
    BindableProperty.Create(nameof(IncreaseQuantityCommand),
        typeof(ICommand), typeof(ItemListDT),
        defaultValue: null, BindingMode.TwoWay);
    // Gets or sets Command value  
    public ICommand IncreaseQuantityCommand
    {
        get => (ICommand)GetValue(IncreaseQuantityCommandProperty);
        set => SetValue(IncreaseQuantityCommandProperty, value);
    }

    #endregion
    
    #region DecreaseQuantityCommand

    public static readonly BindableProperty DecreaseQuantityCommandProperty =
    BindableProperty.Create(nameof(DecreaseQuantityCommand),
        typeof(ICommand), typeof(ItemListDT),
        defaultValue: null, BindingMode.TwoWay);
    // Gets or sets Command value  
    public ICommand DecreaseQuantityCommand
    {
        get => (ICommand)GetValue(DecreaseQuantityCommandProperty);
        set => SetValue(DecreaseQuantityCommandProperty, value);
    }

    #endregion
    
    #region TapCommand

    public static readonly BindableProperty TapCommandProperty =
    BindableProperty.Create(nameof(TapCommand),
        typeof(ICommand), typeof(ItemListDT),
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
        typeof(Aspect), typeof(ItemListDT),
        defaultValue: Aspect.AspectFill, BindingMode.TwoWay);
    // Gets or sets Command value  
    public Aspect ImageAspect
    {
        get => (Aspect)GetValue(ImageAspectProperty);
        set => SetValue(ImageAspectProperty, value);
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