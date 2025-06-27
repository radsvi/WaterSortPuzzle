namespace WaterSortPuzzle
{
    public partial class MainPage : ContentPage
    {
        //internal AppSettings AppSettings { get; }

        //public MainPage()
        public MainPage(MainWindowVM viewModel)
        {
            InitializeComponent();

            //BindingContext = new MainWindowVM();
            //BindingContext = ServiceHelper.GetService<MainWindowVM>();
            BindingContext = viewModel;
        }
    }
}
