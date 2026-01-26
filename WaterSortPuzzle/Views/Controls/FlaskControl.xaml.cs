namespace WaterSortPuzzle.Views.Controls;

public partial class FlaskControl : ContentView
{


	public TubeData? TubeItemSource
	{
		get { return (TubeData?)GetValue(TubeItemSourceProperty); }
		set { SetValue(TubeItemSourceProperty, value); }
	}
	public static readonly BindableProperty TubeItemSourceProperty =
		BindableProperty.Create(nameof(TubeItemSource), typeof(TubeData), typeof(FlaskControl));




	public FlaskControl()
	{
		InitializeComponent();

		BindingContext = this;
	}
}