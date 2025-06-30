

namespace WaterSortPuzzle.Views.Controls;

public partial class TubeControl : ContentView
{
    //public TubeControl()
    //{
    //	InitializeComponent();
    //}
    internal TubeControl(int tubeId, LiquidColor[] liquidColors)
    {
        InitializeComponent();
        //(this.Content as FrameworkElement).BindingContext = this;
        BindingContext = this;

        TubeId = tubeId;
        LiquidColors = liquidColors;
    }
    public TubeControl(){}

    public int TubeId { get; set; }



    internal LiquidColor[] LiquidColors
    {
        get { return (LiquidColor[])GetValue(LiquidColorsProperty); }
        set { SetValue(LiquidColorsProperty, value); }
    }

    // Using a BindableProperty as the backing store for LiquidColors.  This enables animation, styling, binding, etc...
    public static readonly BindableProperty LiquidColorsProperty =
        BindableProperty.Create(nameof(LiquidColors), typeof(LiquidColor[]), typeof(TubeControl));



    //public string LiquidColors
    //{
    //    get { return (string)GetValue(LiquidColorsProperty); }
    //    set { SetValue(LiquidColorsProperty, value); }
    //}

    //// Using a BindableProperty as the backing store for LiquidColors.  This enables animation, styling, binding, etc...
    //public static readonly BindableProperty LiquidColorsProperty =
    //    BindableProperty.Create(nameof(LiquidColors), typeof(string), typeof(TubeControl));
}