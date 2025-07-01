namespace WaterSortPuzzle.Views;

public partial class OptionsPage : ContentPage
{
	public OptionsPage(OptionsPageVM optionsPageVM)
	{
		InitializeComponent();

        BindingContext = optionsPageVM;
    }
}