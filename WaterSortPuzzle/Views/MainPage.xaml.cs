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

        private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            // otevrit notification okno
            await DisplayAlert("Alert", "You have been alerted", "OK");
        }
    }
}
