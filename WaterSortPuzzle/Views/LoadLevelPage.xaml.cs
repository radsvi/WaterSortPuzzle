namespace WaterSortPuzzle.Views;

public partial class LoadLevelPage : ContentPage
{
	public LoadLevelPage(LoadLevelVM loadLevelVM)
	{
		InitializeComponent();

		BindingContext = loadLevelVM;
	}
}