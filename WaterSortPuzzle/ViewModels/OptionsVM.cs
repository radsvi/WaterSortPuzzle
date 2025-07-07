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


    }
}
