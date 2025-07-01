namespace WaterSortPuzzle.ViewModels
{
    public partial class OptionsVM //  : ViewModelBase
    {
        [RelayCommand]
        public async Task NavigateBack() => await Shell.Current.GoToAsync($"..", true);

        public static int TestOptionsValue { get; set; } = 5;
    }
}
