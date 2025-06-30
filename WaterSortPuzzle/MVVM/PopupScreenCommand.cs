using System.Windows.Input;

namespace WaterSortPuzzle.MVVM
{
    internal class PopupScreenCommand : ICommand
    {
        private MainVM mainVM;
        public PopupScreenCommand(MainVM viewModel)
        {
            this.mainVM = viewModel;
        }
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is null)
            {
                mainVM.SelectedViewModel = null;
                return;
            }

            var output = mainVM.PopupActions.Where(x => x.Key == (PopupParams)parameter);
            //var output = Array.Find(viewModel.PopupActions, x => x.Key == (PopupParams)parameter);
            if (output != null && output.Count() == 1)
            {
                mainVM.SelectedViewModel = output.ElementAt(0).SelectedViewModel;
                output.ElementAt(0).OnShowingWindow?.Invoke();
            }
        }
    }
}