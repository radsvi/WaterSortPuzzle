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
        public double DeviceWidth { get; set; }
        public double DeviceHeight { get; set; }

        public QuickOptionsPopupVM(IPopupService popupService)
        {
            this.popupService = popupService;
            var displayInfo = DeviceDisplay.MainDisplayInfo;
            DeviceWidth = displayInfo.Width / displayInfo.Density;
            DeviceHeight = displayInfo.Height / displayInfo.Density;
        }
        [RelayCommand]
        private async Task OnCancel()
        {
            await popupService.ClosePopupAsync(Shell.Current, false);
        }

    }
}
