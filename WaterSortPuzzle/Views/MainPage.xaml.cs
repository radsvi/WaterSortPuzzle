namespace WaterSortPuzzle.Views
{
    public partial class MainPage : ContentPage
    {
        //internal AppSettings AppSettings { get; }

        //public MainPage()
        public MainPage()
        {
            InitializeComponent();

            //BindingContext = new MainVM();
            //BindingContext = ServiceHelper.GetService<MainVM>();
            var viewModel = new MainVM(this);
            BindingContext = viewModel;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}
