namespace WaterSortPuzzle.Views;

public partial class DetailPage : ContentPage
{
	//public DetailPage()
	public DetailPage(DetailPageVM detailPageVM)
	{
		InitializeComponent();

		//BindingContext = ServiceHelper.GetService<AppSettings>();
		BindingContext = detailPageVM;
	}
}