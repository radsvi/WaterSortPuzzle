namespace WaterSortPuzzle.Views;

public partial class HelpPopup : ContentView
{
    public HelpPopup(HelpPopupVM vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}