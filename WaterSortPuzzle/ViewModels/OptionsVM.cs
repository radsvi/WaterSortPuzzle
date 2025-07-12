namespace WaterSortPuzzle.ViewModels
{
    public partial class OptionsVM : ObservableObject //  : ViewModelBase
    {
        public MainVM MainVM { get; }
        public AppPreferences AppPreferences { get; }
        public GameState GameState { get; }
        public OptionsVM(MainVM mainVM, AppPreferences appPreferences, GameState gameState)
        {
            MainVM = mainVM;
            AppPreferences = appPreferences;
            GameState = gameState;
        }
        //[RelayCommand]
        //public async Task NavigateBack() => await Shell.Current.GoToAsync($"..", true);

        //[ObservableProperty]
        //RadioButton selectedTheme;
        //public RadioButton SelectedTheme { get; set; }
        private AppTheme selectedTheme;
        public AppTheme SelectedTheme
        {
            get { return selectedTheme; }
            set
            {
                if (value != selectedTheme)
                {
                    selectedTheme = value;
                    App.Current!.UserAppTheme = value;
                    OnPropertyChanged();
                }
            }
        }


        [RelayCommand]
        private void SwitchTheme()
        {
            //App.Current.UserAppTheme = AppTheme.Dark; // for testing
            if (App.Current!.UserAppTheme == AppTheme.Unspecified)
                App.Current.UserAppTheme = AppTheme.Dark;
            else if (App.Current.UserAppTheme == AppTheme.Dark)
                App.Current.UserAppTheme = AppTheme.Light;
            else
            {
                App.Current.UserAppTheme = AppTheme.Unspecified;
                //await MainPage.DisplayAlert("Theme", $"Theme set to {App.Current.UserAppTheme}", "OK");
            }

            AppPreferences.ThemeUserSetting = App.Current.UserAppTheme;
        }

    }
}
