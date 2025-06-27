namespace WaterSortPuzzle
{
    public partial class MainPage : ContentPage
    {
        //internal AppSettings AppSettings { get; }

        public MainPage()
        {
            InitializeComponent();

            //AppSettings = new AppSettings();
            //BindingContext = ServiceHelper.GetService<MainWindowVM>();
            BindingContext = new MainWindowVM();
        }
    }
}
