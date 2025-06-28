﻿namespace WaterSortPuzzle.ViewModels
{
    public class PopupScreenBase : ViewModelBase
    {
        public MainWindowVM MainWindowVM { get; set; }
        public PopupScreenBase(object viewModel)
        {
            MainWindowVM = (MainWindowVM)viewModel;
        }
    }


    internal class HelpVM : PopupScreenBase
    {
        public HelpVM(object viewModel) : base(viewModel) { }
    }
    internal class LevelCompleteVM : PopupScreenBase
    {
        public LevelCompleteVM(object viewModel) : base(viewModel) { }
    }
    internal class NewLevelVM : PopupScreenBase
    {
        public NewLevelVM(object viewModel) : base(viewModel) { }
    }
    internal class RestartLevelVM : PopupScreenBase
    {
        public RestartLevelVM(object viewModel) : base(viewModel) { }
    }
    class GameSavedNotificationVM : PopupScreenBase
    {
        public GameSavedNotificationVM(object viewModel) : base(viewModel) { }
    }
    class SaveLevelVM : PopupScreenBase
    {
        public SaveLevelVM(object viewModel) : base(viewModel) { }
    }
}
