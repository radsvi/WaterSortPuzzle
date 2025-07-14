namespace WaterSortPuzzle
{
    public partial class App : Application
    {
        //public static IServiceProvider Services;
        //public static IAlertService AlertSvc;
        readonly GameState gameState;

        //public App(IServiceProvider provider, GameState gameState)
        public App(GameState gameState)
        {
            InitializeComponent();

            //Services = provider;
            //AlertSvc = Services.GetService<IAlertService>();
            this.gameState = gameState;
        }
        protected override Window CreateWindow(IActivationState? activationState)
        {
            //2400x1080 ratio
            //const int newHeight = 900;
            //const int newWidth = 450;
            const int newHeight = 1000;
            const int newWidth = 450;

            var resolution = DeviceDisplay.Current.MainDisplayInfo;

            var newWindow = new Window(new AppShell())
            {
                Height = newHeight,
                Width = newWidth,
                X = resolution.Width - newWidth + 10,
                Y = 0,
            };

            return newWindow;
        }
        protected override void OnSleep()
        {
            base.OnSleep();

            this.gameState.SaveGameStateOnSleep();
        }
    }
}