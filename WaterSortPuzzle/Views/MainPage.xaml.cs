
using Microsoft.Extensions.Logging.Abstractions;

namespace WaterSortPuzzle.Views
{
    public partial class MainPage : ContentPage
    {
        //internal AppPreferences AppPreferences { get; }
        readonly MainVM mainVM;

        IDispatcherTimer timer;

        //public MainPage()
        public MainPage()
        {
            InitializeComponent();

            //BindingContext = new MainVM();
            //BindingContext = ServiceHelper.GetService<MainVM>();
            var mainVM = new MainVM(this);
            BindingContext = mainVM;

            //this.Appearing += Page_Appearing;
            //this.NavigatedTo += Page_Appearing;

            //MainThread.BeginInvokeOnMainThread(() =>
            //{
            //    //Update view here
            //    await mainVM.DisplayHelpPopup();
            //});

            //var timer = Application.Current.Dispatcher.CreateTimer();
            //StartTimer(TimeSpan.FromSeconds(4), () =>
            //{
            //    //ShowMessage();
            //    await mainVM.DisplayHelpPopup();
            //    return false; // True = Repeat again, False = Stop the timer
            //});

            //timer = Dispatcher.CreateTimer();
            //timer.Interval = TimeSpan.FromMilliseconds(1000);
            //timer.Tick += async (s, e) =>
            //{
            //    //label.Text = DateTime.Now.ToString();
            //    await mainVM.DisplayHelpPopup();

            //};
            //timer.Start();


            //Task.Run( async () => await mainVM.DisplayHelpPopup()).Wait();
            //var result = Task.Run(async () => await DisplayHelpPopup());
            //Task.Run(async () => await DisplayHelpPopup());

            //var task = DisplayHelpPopup();
            //var result = task.WaitAndUnwrapException();

            //var result = AsyncContext.RunTask(DisplayHelpPopup).Result;

            //var task = Task.Run(async () => await DisplayHelpPopup());
            //var result = task.WaitAsync(TimeSpan.FromMilliseconds(10000));

            //var task = Task.Run(async () => await DisplayHelpPopup());

            //MainAsync().GetAwaiter();

            //var myTask = DisplayHelpPopup(); // call your method which will return control once it hits await
            // now you can continue executing code here
            //string result = myTask.Result; // wait for the task to complete to continue
            // use result

            //Task.Run(async () =>
            //{
            //    await Task.Delay(2000);
            //    App.AlertSvc.ShowConfirmation("Title", "Confirmation message.", (result =>
            //    {
            //        App.AlertSvc.ShowAlert("Result", $"{result}");
            //    }));
            //});

            Task.Run(async () =>
            {
                await Task.Delay(2000);
                App.AlertSvc.ShowConfirmation("Title", "Confirmation message.", (result =>
                {
                    App.AlertSvc.ShowAlert("Result", $"{result}");
                }));
            });

        }
        //async Task MainAsync()
        //{
        //    /*await stuff here*/
        //    string text = "Separate each color into different vials.\n";
        //    text += "You can only move matching colors onto each other.\n";
        //    text += "You can always move colors to empty vial.\n";
        //    text += "You can add empty vials, if you are really stuck.\n";
        //    bool answer = await DisplayAlert("Help", text, "Don't show this again.", "Close");
        //}
        //protected override void OnAppearing()
        //{
        //    this.Appearing += Page_Appearing;
        //    base.OnAppearing();
        //}

        protected async void Page_Appearing(object? sender, EventArgs args)
        {
            //if (App.Database != null)
            //{
            //    Infolistview.ItemsSource = await App.Database.GetInfoesAsync();
            //}
            //this.Appearing -= Page_Appearing; //Unsubscribe (OPTIONAL but advised)

            //await this.mainVM.DisplayHelpPopup();

            string text = "Separate each color into different vials.\n";
            text += "You can only move matching colors onto each other.\n";
            text += "You can always move colors to empty vial.\n";
            text += "You can add empty vials, if you are really stuck.\n";
            bool answer = await DisplayAlert("Help", text, "Don't show this again.", "Close");
            
        }
        public async Task DisplayHelpPopup()
        {
            timer.Stop();
            string text = "Separate each color into different vials.\n";
            text += "You can only move matching colors onto each other.\n";
            text += "You can always move colors to empty vial.\n";
            text += "You can add empty vials, if you are really stuck.\n";
            bool answer = await DisplayAlert("Help", text, "Don't show this again.", "Close");

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //await DisplayAlert("Alert", "Welcome to the app!", "OK");
            bool x = await Application.Current.MainPage.DisplayAlert("Tittle", "Hello", "OK", "NotOK");
        }

    }
}
