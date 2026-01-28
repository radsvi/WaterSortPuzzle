namespace WaterSortPuzzle.Features.Popups;

public partial class LevelCompletedPopup : ContentView
{
	public LevelCompletedPopup(LevelCompletedPopupVM popupViewModel)
	{
		InitializeComponent();
        BindingContext = popupViewModel;
    }
}