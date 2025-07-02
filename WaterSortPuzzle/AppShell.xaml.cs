namespace WaterSortPuzzle
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //Routing.RegisterRoute(nameof(AppSettings), typeof(AppSettings));
            Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
            Routing.RegisterRoute(nameof(OptionsPage), typeof(OptionsPage));
            Routing.RegisterRoute(nameof(LoadLevelPage), typeof(LoadLevelPage));
        }
    }
}
