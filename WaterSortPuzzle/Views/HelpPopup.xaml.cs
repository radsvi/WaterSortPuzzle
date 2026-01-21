namespace WaterSortPuzzle.Views;

public partial class HelpPopup : ContentView
{
	public HelpPopup(HelpPopupVM popupViewModel)
    {
        InitializeComponent();
        BindingContext = popupViewModel;

        //this.Loaded += OnLoaded;
    }
    //private void OnLoaded(object? sender, EventArgs e)
    //{
    //    if (BindingContext is HelpPopupVM vm)
    //    {
    //        vm.CoachMarkManager.Start();
    //    }

    //    this.Loaded -= OnLoaded; // only fire once
    //}
}