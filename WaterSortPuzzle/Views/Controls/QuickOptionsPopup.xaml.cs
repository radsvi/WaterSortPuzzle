namespace WaterSortPuzzle.Views.Controls;

public partial class QuickOptionsPopup : ContentView
{
	public QuickOptionsPopup(QuickOptionsPopupVM popupViewModel)
    {
        InitializeComponent();
        BindingContext = popupViewModel;
    }
}