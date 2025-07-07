
namespace WaterSortPuzzle
{
    public partial class AppShell : Shell
    {
        readonly MainVM vm;
        public AppShell()
        {
            InitializeComponent();

            //Routing.RegisterRoute(nameof(AppPreferences), typeof(AppPreferences));
            Routing.RegisterRoute(nameof(OptionsPage), typeof(OptionsPage));
            //Routing.RegisterRoute("MainPage/OptionsPage", typeof(OptionsPage));
            //Routing.RegisterRoute(nameof(MainPage) + "/" + nameof(OptionsPage), typeof(OptionsPage));
            Routing.RegisterRoute(nameof(LoadLevelPage), typeof(LoadLevelPage));

            BindingContext = vm = IPlatformApplication.Current!.Services.GetService<MainVM>()!;
        }
        //protected override void OnNavigated(ShellNavigatedEventArgs args)
        //{
        //    base.OnNavigated(args);
        //    //title.Text = Shell.Current.CurrentItem.Title;
        //    // make the title always show correctly
        //}
    }
}
