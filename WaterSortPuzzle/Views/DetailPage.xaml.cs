namespace WaterSortPuzzle.Views;

public partial class DetailPage : ContentPage
{
	public DetailPage()
	{
		InitializeComponent();

        BindingContext = ServiceHelper.GetService<MainWindowVM>();
    }
}