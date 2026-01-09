namespace WaterSortPuzzle.Views.Controls;

public partial class CustomPopup : ContentView
{
    public CustomPopup(CustomPopupVM namePopupViewModel)
    {
        InitializeComponent();
        BindingContext = namePopupViewModel;
    }
}