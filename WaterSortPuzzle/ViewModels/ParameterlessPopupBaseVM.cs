using CommunityToolkit.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.ViewModels
{
    public partial class ParameterlessPopupBaseVM : ObservableObject
    {
        protected readonly IPopupService popupService;
        protected readonly MainVM mainVM;

        public double DeviceWidth { get; set; }
        public double DeviceHeight { get; set; }

        public ParameterlessPopupBaseVM(IPopupService popupService, MainVM mainVM)
        {
            this.popupService = popupService;
            this.mainVM = mainVM;
            var displayInfo = DeviceDisplay.MainDisplayInfo;
            DeviceWidth = displayInfo.Width / displayInfo.Density;
            DeviceHeight = displayInfo.Height / displayInfo.Density;
        }
        [RelayCommand]
        private async Task OnCancel()
        {
            await popupService.ClosePopupAsync(Shell.Current, false);
        }

        [RelayCommand]
        private async Task NavigateToPage(Type pageType)
        {
            await popupService.ClosePopupAsync(Shell.Current, false);
            await mainVM.NavigateToPage(pageType);
        }
        [RelayCommand]
        private async Task NavigationMenuPopup(PopupParams menuItem)
        {
            await popupService.ClosePopupAsync(Shell.Current, false);
            await mainVM.NavigationMenuPopup(menuItem);
        }
    }
}
