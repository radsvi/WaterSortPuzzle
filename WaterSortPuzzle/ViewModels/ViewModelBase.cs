namespace WaterSortPuzzle.ViewModels
{
    public partial class ViewModelBase : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;

        [ObservableProperty]
        string title = string.Empty;

        public bool IsNotBusy => !IsBusy;
        public bool PropertyChangedEventPaused { get; set; } = false;
    }
}
