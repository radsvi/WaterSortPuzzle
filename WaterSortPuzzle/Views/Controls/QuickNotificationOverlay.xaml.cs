namespace WaterSortPuzzle.Views.Controls;

public partial class QuickNotificationOverlay : ContentView
{
	public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(QuickNotificationOverlay),
		propertyChanged: (bindable, oldValue, newValue) => {
			var control = (QuickNotificationOverlay)bindable;

			control.Titlelabel.Text = newValue as string;
		});

	private string title;

    public QuickNotificationOverlay()
	{
		InitializeComponent();
	}

    public string Title 
	{
		get => GetValue(TitleProperty) as string;
		set => SetValue(TitleProperty, value);
	}

    
}