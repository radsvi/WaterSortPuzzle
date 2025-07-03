namespace WaterSortPuzzle.ViewModels
{
    public partial class OptionsVM : ObservableObject //  : ViewModelBase
    {
        public MainVM MainVM { get; }
        public AppPreferences AppPreferences { get; }
        public GameState GameState { get; }
        public OptionsVM(MainVM mainVM)
        {
            MainVM = mainVM;
            AppPreferences = mainVM.AppPreferences;
            GameState = MainVM.GameState;
        }
        //[RelayCommand]
        //public async Task NavigateBack() => await Shell.Current.GoToAsync($"..", true);


    }
}
