namespace WaterSortPuzzle
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //Routing.RegisterRoute(nameof(AppPreferences), typeof(AppPreferences));
            Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
            Routing.RegisterRoute(nameof(OptionsPage), typeof(OptionsPage));
            //Routing.RegisterRoute("MainPage/OptionsPage", typeof(OptionsPage));
            //Routing.RegisterRoute(nameof(MainPage) + "/" + nameof(OptionsPage), typeof(OptionsPage));
            Routing.RegisterRoute(nameof(LoadLevelPage), typeof(LoadLevelPage));
        }
    }
}
