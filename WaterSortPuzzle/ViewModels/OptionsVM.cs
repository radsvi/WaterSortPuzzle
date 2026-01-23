namespace WaterSortPuzzle.ViewModels
{
    public partial class OptionsVM : ObservableObject //  : ViewModelBase
    {
        public MainVM MainVM { get; }
        public AppPreferences AppPreferences { get; }
        public Leveling Leveling { get; }
        public GameState GameState { get; }

        DateTime firstTapTime;
        int tapCount;
        readonly TimeSpan tapWindow = TimeSpan.FromSeconds(2);

        public OptionsVM(MainVM mainVM, AppPreferences appPreferences, GameState gameState, Leveling leveling)
        {
            MainVM = mainVM;
            AppPreferences = appPreferences;
            GameState = gameState;
            Leveling = leveling;
        }
        [RelayCommand]
        private void RevealDeveloperMenu()
        {
            if (AppPreferences.RevealDeveloperOptions == true)
                return;

            if (tapCount == 0)
                firstTapTime = DateTime.UtcNow;

            tapCount++;

            if (DateTime.UtcNow - firstTapTime > tapWindow)
            {
                tapCount = 1;
                firstTapTime = DateTime.UtcNow;
            }

            if (tapCount >= 7)
            {
                //tapCount = 0;
                AppPreferences.RevealDeveloperOptions = true;
            }
        }
        //[RelayCommand]
        //public async Task NavigateBack() => await Shell.Current.GoToAsync($"..", true);

        //[ObservableProperty]
        //RadioButton selectedTheme;
        //public RadioButton SelectedTheme { get; set; }
        //private AppTheme selectedTheme;
        //public AppTheme SelectedTheme
        //{
        //    get { return selectedTheme; }
        //    set
        //    {
        //        if (value != selectedTheme)
        //        {
        //            selectedTheme = value;
        //            App.Current!.UserAppTheme = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}


        //[RelayCommand]
        //private void SwitchTheme()
        //{
        //    //App.Current.UserAppTheme = AppTheme.Dark; // for testing
        //    if (App.Current!.UserAppTheme == AppTheme.Unspecified)
        //        App.Current.UserAppTheme = AppTheme.Dark;
        //    else if (App.Current.UserAppTheme == AppTheme.Dark)
        //        App.Current.UserAppTheme = AppTheme.Light;
        //    else
        //    {
        //        App.Current.UserAppTheme = AppTheme.Unspecified;
        //        //await MainPage.DisplayAlert("Theme", $"Theme set to {App.Current.UserAppTheme}", "OK");
        //    }

        //    AppPreferences.ThemeUserSetting = App.Current.UserAppTheme;
        //}

    }
}
