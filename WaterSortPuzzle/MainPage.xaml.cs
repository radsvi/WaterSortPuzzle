namespace WaterSortPuzzle
{
    public partial class MainPage : ContentPage
    {
        internal AppSettings AppSettings { get; }

        public MainPage()
        {
            InitializeComponent();

            AppSettings = new AppSettings();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            //count++;

            //if (count == 1)
            //    CounterBtn.Text = $"Clicked {count} time";
            //else
            //    CounterBtn.Text = $"Clicked {count} times";

            

            //CounterBtn.Text = $"qwerqwer {AppSettings.ZkouskaAAA}";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private void EditBtn_Clicked(object sender, EventArgs e)
        {
            //AppSettings.ZkouskaAAA = sejvEntry.Text.ToString()!;
        }
    }

}
