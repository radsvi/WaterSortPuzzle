namespace WaterSortPuzzle.Views.Controls;

public partial class FullscreenPopup : ContentView
{
	public FullscreenPopup(FullscreenPopupVM popupViewModel)
    {
        InitializeComponent();
        BindingContext = popupViewModel;
    }
}