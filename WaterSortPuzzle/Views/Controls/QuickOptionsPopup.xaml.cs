namespace WaterSortPuzzle.Views.Controls;

public partial class QuickOptionsPopup : ContentView
{
	public QuickOptionsPopup(FullscreenPopupVM popupViewModel)
    {
        InitializeComponent();
        BindingContext = popupViewModel;
    }
}