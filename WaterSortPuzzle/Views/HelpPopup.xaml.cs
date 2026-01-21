namespace WaterSortPuzzle.Views;

public partial class HelpPopup : ContentView
{
    public HelpPopup(HelpPopupVM vm)
    {
        InitializeComponent();
        BindingContext = vm;

        Loaded += OnLoaded;
    }

    private void OnLoaded(object? sender, EventArgs e)
    {
        ((HelpPopupVM)BindingContext).CoachMarkManager.Start();
        Loaded -= OnLoaded;
    }
}