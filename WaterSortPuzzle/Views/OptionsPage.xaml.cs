namespace WaterSortPuzzle.Views;

public partial class OptionsPage : ContentPage
{
	public OptionsPage(OptionsVM optionsVM)
	{
		InitializeComponent();

        BindingContext = optionsVM;
    }
}