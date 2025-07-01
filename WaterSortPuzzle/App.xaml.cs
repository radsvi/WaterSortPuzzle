namespace WaterSortPuzzle
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        //protected override Window CreateWindow(IActivationState? activationState)
        //{
        //    return new Window(new AppShell());
        //}
        protected override Window CreateWindow(IActivationState? activationState)
        {
            const int newHeight = 900;
            //const int newWidth = 450;
            const int newWidth = 1200;

            var newWindow = new Window(new AppShell())
            {
                Height = newHeight,
                Width = newWidth
            };

            return newWindow;
        }
    }
}