using CommunityToolkit.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.ViewModels
{
    public partial class QuickOptionsPopupVM : ObservableObject
    {
        readonly IPopupService popupService;

        public QuickOptionsPopupVM(IPopupService popupService)
        {
            this.popupService = popupService;
        }
        [RelayCommand]
        private async Task OnCancel()
        {
            await popupService.ClosePopupAsync(Shell.Current, false);
        }
    }
}
