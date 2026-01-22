
using Microsoft.Extensions.Logging.Abstractions;

namespace WaterSortPuzzle.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly MainVM mainVM;

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
        }
    }
}
