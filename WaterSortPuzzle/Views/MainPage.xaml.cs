
using Microsoft.Extensions.Logging.Abstractions;

namespace WaterSortPuzzle.Views
{
    public partial class MainPage : ContentPage
    {
        //internal AppPreferences AppPreferences { get; }
        //readonly MainVM mainVM;

        //public MainPage()
        public MainPage(MainVM mainVM)
        {
            InitializeComponent();

            //BindingContext = new MainVM();
            //BindingContext = ServiceHelper.GetService<MainVM>();
            //var mainVM = new MainVM(this);
            mainVM.RequestMove += async () =>
            {
                await MyLabel.TranslateTo(100, 0, 500); // x, y, duration
            };
            BindingContext = mainVM;
        }
    }
}
