namespace WaterSortPuzzle.Views;

public partial class HelpPopup : ContentView
{
    public HelpPopup(HelpPopupVM vm)
    {
        InitializeComponent();
        BindingContext = vm;

        Loaded += OnLoaded;

        //this.Loaded += (_, _) =>
        //{
        //    foreach (var behavior in this.FindBehaviors<CoachMarkBehavior>())
        //    {
        //        behavior.ReportBounds = vm.CoachMarkManager.OnBoundsReported;
        //        behavior.TriggerInitialBounds(); // optional
        //    }
        //};
    }

    private void OnLoaded(object? sender, EventArgs e)
    {
        //((HelpPopupVM)BindingContext).CoachMarkManager.Start();
        //Loaded -= OnLoaded;



        if (BindingContext is not HelpPopupVM vm)
            return;

        foreach (var behavior in this.FindBehaviors<CoachMarkBehavior>())
        {
            behavior.ReportBounds = vm.CoachMarkManager.OnBoundsReported;
            behavior.TriggerInitialBounds(); // Optional: fire immediately
        }

        this.Loaded -= OnLoaded; // only run once
    }
    private void OnPopupLoaded(object? sender, EventArgs e)
    {
        if (BindingContext is not HelpPopupVM vm)
            return;

        // Register each button manually
        RegisterCoachMark(vm, StepBackButton, "StepBackButton", RelativePosition.TopLeft);
        RegisterCoachMark(vm, AddExtraTubeButton, "AddExtraTubeButton", RelativePosition.Top);
        // ... repeat for all buttons

        this.Loaded -= OnPopupLoaded;

        vm.CoachMarkManager.Start();
    }

    private void RegisterCoachMark(HelpPopupVM vm, VisualElement element, string id, RelativePosition position)
    {
        var bounds = new Rect(element.X, element.Y, element.Width, element.Height);
        var item = vm.CoachMarks.FirstOrDefault(m => m.Id == id);
        if (item != null)
        {
            item.SourceBounds = bounds;
            item.Position = position;
        }
    }
}