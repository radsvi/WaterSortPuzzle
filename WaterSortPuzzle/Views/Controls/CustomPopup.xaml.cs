namespace WaterSortPuzzle.Views.Controls;

public partial class CustomPopup : ContentView
{
    public CustomPopup(CustomPopupViewModel namePopupViewModel)
    {
        InitializeComponent();
        BindingContext = namePopupViewModel;
    }
}