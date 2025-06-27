namespace WaterSortPuzzle
{
    public partial class MainPage : ContentPage
    {
        //internal AppSettings AppSettings { get; }

        //public MainPage(MainWindowVM viewModel)
        public MainPage()
        {
            InitializeComponent();

            //BindingContext = new MainWindowVM();
            BindingContext = ServiceHelper.GetService<MainWindowVM>();
            //BindingContext = viewModel;
        }
    }
}
