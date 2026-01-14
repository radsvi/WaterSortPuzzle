using CommunityToolkit.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.ViewModels
{
    public class FullscreenPopupBaseVM : CustomPopupVM
    {
        public double DeviceWidth { get; set; }
        public double DeviceHeight { get; set; }
        public FullscreenPopupBaseVM(IPopupService popupService) : base(popupService)
        {
            var displayInfo = DeviceDisplay.MainDisplayInfo;
            DeviceWidth = displayInfo.Width / displayInfo.Density;
            DeviceHeight = displayInfo.Height / displayInfo.Density;
        }
    }
}
