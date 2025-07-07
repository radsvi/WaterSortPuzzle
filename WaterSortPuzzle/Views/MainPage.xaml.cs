
using Microsoft.Extensions.Logging.Abstractions;

namespace WaterSortPuzzle.Views
{
    public partial class MainPage : ContentPage
    {
        //internal AppPreferences AppPreferences { get; }
        //readonly MainVM mainVM;

        //public MainPage()
        public MainPage(TestovaniDInjectionVM mainVM)
        {
            InitializeComponent();

            //BindingContext = new MainVM();
            //BindingContext = ServiceHelper.GetService<MainVM>();
            //var mainVM = new MainVM(this);
            BindingContext = mainVM;
        }
    }
}
