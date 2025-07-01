namespace WaterSortPuzzle.ViewModels
{
    public partial class OptionsPageVM //  : ViewModelBase
    {
        [RelayCommand]
        public async Task NavigateBack() => await Shell.Current.GoToAsync($"..", true);
    }
}
