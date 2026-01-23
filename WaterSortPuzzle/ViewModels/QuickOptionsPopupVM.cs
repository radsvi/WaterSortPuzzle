using CommunityToolkit.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.ViewModels
{
    public partial class QuickOptionsPopupVM : FullscreenParameterlessPopupBaseVM
    {
        public QuickOptionsPopupVM(IPopupService popupService, MainVM mainVM) : base(popupService, mainVM) {}

        [RelayCommand(CanExecute = nameof(CanStart))]
        private async Task StartAutoSolve()
        {
            await popupService.ClosePopupAsync(Shell.Current, false);
            mainVM.StartAutoSolve();
        }
        private bool CanStart() => mainVM.AutoSolve.CanStart();
        [RelayCommand]
        private async Task ShowHelp()
        {
            await popupService.ClosePopupAsync(Shell.Current, false);
            mainVM.RestartTutorial();
        }
        //[RelayCommand]
        //private async Task TestMethod()
        //{
        //    mainVM.TestMethod();
        //}
    }
}