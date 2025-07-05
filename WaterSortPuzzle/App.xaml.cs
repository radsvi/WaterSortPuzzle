namespace WaterSortPuzzle
{
    public partial class App : Application
    {
        public static IServiceProvider Services;
        public static IAlertService AlertSvc;
        public App(IServiceProvider provider)
        {
            InitializeComponent();

            Services = provider;
            AlertSvc = Services.GetService<IAlertService>();
        }

        //protected override Window CreateWindow(IActivationState? activationState)
        //{
        //    return new Window(new AppShell());
        //}
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
    }
}