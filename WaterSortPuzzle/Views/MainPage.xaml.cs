
using Microsoft.Extensions.Logging.Abstractions;

namespace WaterSortPuzzle.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly MainVM mainVM;
        readonly Dictionary<string, VisualElement> _coachTargets = new();

        public MainPage(MainVM mainVM)
        {
            InitializeComponent();

            //BindingContext = new MainVM();
            //BindingContext = ServiceHelper.GetService<MainVM>();
            //var mainVM = new MainVM(this);
            BindingContext = mainVM;
            this.mainVM = mainVM;

            //var overlay = new CoachMarkOverlay();
            //overlay.Show(StepBackButton, "Tap here to start");

            //(MainLayout as Layout).Children.Add(overlay);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            mainVM.InitializeOnce();

            _coachTargets["Restart"] = RestartButton;
            _coachTargets["StepBack"] = StepBackButton;

            mainVM.PropertyChanged += OnViewModelPropertyChanged;
            ShowCurrentStep();
        }
        void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(mainVM.CurrentStep))
                ShowCurrentStep();
        }
        async void ShowCurrentStep()
        {
            var step = mainVM.CurrentStep;

            if (step == null)
            {
                CoachOverlay.IsVisible = false;
                return;
            }

            //var target = _coachTargets[step.TargetKey];
            //CoachOverlay.Show(target, step.Text);
            await ShowStepAsync(step);
        }
        async Task ShowStepAsync(CoachMarkItem step)
        {
            if (!_coachTargets.TryGetValue(step.TargetKey, out var target))
                return;

            // Ensure layout is complete
            await target.Dispatcher.DispatchAsync(async () =>
            {
                //target.InvalidateMeasure(); // <- vratit?
                await Task.Yield();

                CoachOverlay.Show(target, step.Text);
            });

            // One-frame delay — critical
            await Task.Yield();

            CoachOverlay.Show(target, step.Text);
        }
    }
}
