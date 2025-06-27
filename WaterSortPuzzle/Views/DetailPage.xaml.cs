namespace WaterSortPuzzle.Views;

public partial class DetailPage : ContentPage
{
	//internal DetailPage(DetailPageVM detailPageVM)
	public DetailPage()
	{
		InitializeComponent();

		BindingContext = ServiceHelper.GetService<AppSettings>();
		//BindingContext = detailPageVM;
	}
}