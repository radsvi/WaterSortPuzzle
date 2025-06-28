namespace WaterSortPuzzle
{
    public partial class MainPage : ContentPage
    {
        //internal AppSettings AppSettings { get; }

        //public MainPage()
        public MainPage()
        {
            InitializeComponent();

            //BindingContext = new MainWindowVM();
            //BindingContext = ServiceHelper.GetService<MainWindowVM>();
            var viewModel = new MainWindowVM(this, NotificationBox);
            BindingContext = viewModel;
        }
    }
}
