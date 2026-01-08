using CommunityToolkit.Maui.Views;

namespace WaterSortPuzzle.Views.Controls;

public partial class CustomPopup : CommunityToolkit.Maui.Views.Popup
{
    public CustomPopup()
    {
        InitializeComponent();
    }

    private async void OnOkClicked(object sender, EventArgs e)
    {
#pragma warning disable CA1416
        await CloseAsync();
#pragma warning restore CA1416
    }
}