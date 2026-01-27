
using Microsoft.Extensions.Logging.Abstractions;

namespace WaterSortPuzzle.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly MainVM mainVM;
        readonly Dictionary<CoachMarkTarget, VisualElement> _coachTargets = new();

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
            mainVM.PropertyChanged += OnViewModelPropertyChanged;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            mainVM.InitializeOnce();

            LoadCoachMarks();
        }

        void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(mainVM.CurrentCoachMark))
                ShowCurrentStep();
        }
        #region CoachMarks
        private void LoadCoachMarks()
        {
            _coachTargets[CoachMarkTarget.Options] = OptionsButton;
            _coachTargets[CoachMarkTarget.NextLevel] = NextLevelButton;
            _coachTargets[CoachMarkTarget.Restart] = RestartButton;
            _coachTargets[CoachMarkTarget.Restart2] = RestartButton2;
            _coachTargets[CoachMarkTarget.NextStep] = NextStepButton;
            _coachTargets[CoachMarkTarget.AddExtraTube] = AddExtraTubeButton;
            _coachTargets[CoachMarkTarget.StepBack] = StepBackButton;

            StartCoachMarks();
        }
        private async void StartCoachMarks()
        {
            if (mainVM.AppPreferences.ShowHelpScreenAtStart == false)
                return;
            
            await Task.Yield(); // let MAUI finish appearing
            await Task.Delay(Constants.OneFrame);
            ShowCurrentStep();
        }
        private async void ShowCurrentStep()
        {
            var step = mainVM.CurrentCoachMark;

            if (step == null)
            {
                CoachOverlay.IsVisible = false;
                return;
            }
            else if(CoachOverlay.IsVisible == false)
            {
                CoachOverlay.IsVisible = true;
            }

            //var target = _coachTargets[step.TargetKey];
            //CoachOverlay.Show(target, step.Text);
            await ShowStepAsync(step);
        }
        private async Task ShowStepAsync(CoachMarkItem step)
        {
            if (!_coachTargets.TryGetValue(step.TargetKey, out var target))
            {
                mainVM.NextCoachMark();
                return;
            }
            if (!target.IsVisible)
            {
                // Skip this step entirely
                mainVM.NextCoachMark();
                return;
            }

            // Yield to allow layout to complete
            await Task.Yield();

            // If still not laid out, retry once on next frame
            if (target.Width <= 0 || target.Height <= 0)
            {
                await Task.Delay(Constants.OneFrame);
            }

            //if (target.Width <= 0 || target.Height <= 0)
            //{
            //    void OnSizeChanged(object? s, EventArgs e)
            //    {
            //        target.SizeChanged -= OnSizeChanged;
            //        System.Diagnostics.Debug.WriteLine($"#### OnSizeChanged called");
            //        ShowCurrentStep();
            //    }

            //    target.SizeChanged += OnSizeChanged;
            //    return;
            //}

            if (target.Width <= 0 || target.Height <= 0)
            {
                System.Diagnostics.Debug.WriteLine("Target never received size, skipping");
                mainVM.NextCoachMark();
                return;
            }

            CoachOverlay.Show(target, step.Text);
        }
        //private static bool IsTargetVisible(VisualElement target)
        //{
        //    if (!target.IsVisible)
        //        return false;

        //    if (!target.IsLoaded)
        //        return false;

        //    if (target.Width <= 0 || target.Height <= 0)
        //        return false;

        //    return true;
        //}
        #endregion CoachMarks
    }
}
