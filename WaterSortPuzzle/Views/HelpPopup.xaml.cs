namespace WaterSortPuzzle.Views;

public partial class HelpPopup : ContentView
{
	public HelpPopup(HelpPopupVM popupViewModel)
    {
        InitializeComponent();
        BindingContext = popupViewModel;
    }
}