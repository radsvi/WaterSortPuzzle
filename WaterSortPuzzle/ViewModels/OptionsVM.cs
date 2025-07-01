namespace WaterSortPuzzle.ViewModels
{
    public partial class OptionsVM : ObservableObject //  : ViewModelBase
    {
        public MainVM MainVM { get; }
        public AppSettings AppSettings { get; }
        public GameState GameState { get; }
        public OptionsVM(MainVM mainVM)
        {
            MainVM = mainVM;
            AppSettings = mainVM.AppSettings;
            GameState = MainVM.GameState;
        }
        //[RelayCommand]
        //public async Task NavigateBack() => await Shell.Current.GoToAsync($"..", true);

    }
}
