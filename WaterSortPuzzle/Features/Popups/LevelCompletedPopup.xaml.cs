using WaterSortPuzzle.Features.Fireworks;

namespace WaterSortPuzzle.Features.Popups;

public partial class LevelCompletedPopup : ContentView
{
    public FireworksView FireworksCanvas { get; private set; }
    private readonly LevelCompletedPopupVM viewModel;
    public LevelCompletedPopup(LevelCompletedPopupVM popupViewModel)
	{
		InitializeComponent();
        this.viewModel = popupViewModel;
        BindingContext = popupViewModel;

        FireworksCanvas = new FireworksView(popupViewModel);

        MainGrid.Children.Add(FireworksCanvas);
        MainGrid.SetRow(FireworksCanvas, 1);
    }
}