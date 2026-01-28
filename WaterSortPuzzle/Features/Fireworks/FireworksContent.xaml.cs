namespace WaterSortPuzzle.Features.Fireworks;

public partial class FireworksContent : ContentView
{
    private readonly FireworksViewModel _vm = new();
    public FireworksView FireworksCanvas { get; private set; }
    public FireworksContent()
	{
        InitializeComponent();
        BindingContext = _vm;

        FireworksCanvas = new FireworksView(_vm);

        MainGrid.Children.Add(FireworksCanvas);
        MainGrid.SetRow(FireworksCanvas, 1);
    }
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        if (BindingContext is FireworksViewModel vm)
        {
            vm.PageWidth = width;
            vm.PageHeight = height;
        }
    }
}