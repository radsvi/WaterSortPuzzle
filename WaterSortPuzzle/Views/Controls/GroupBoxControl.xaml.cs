namespace WaterSortPuzzle.Views.Controls;

public partial class GroupBoxControl : ContentView
{
    
    public GroupBoxControl()
	{
		InitializeComponent();
	}
    private string header = string.Empty;
    public string Header
    {
        get => GetValue(HeaderProperty) as string;
        set => SetValue(HeaderProperty, value);
    }
    public static readonly BindableProperty HeaderProperty = BindableProperty.Create(nameof(Header), typeof(string), typeof(GroupBoxControl), string.Empty);
    
}