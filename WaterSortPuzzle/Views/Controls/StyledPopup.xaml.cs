namespace WaterSortPuzzle.Views.Controls;

public partial class StyledPopup : ContentView
{
    public StyledPopup(StyledPopupViewModel styledPopupViewModel)
    {
        InitializeComponent();
        BindingContext = styledPopupViewModel;
    }
}