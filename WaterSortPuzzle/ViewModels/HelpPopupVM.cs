using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.ViewModels
{
    public partial class HelpPopupVM : FullscreenParameterlessPopupBaseVM
    {

        public HelpPopupVM(IPopupService popupService, MainVM mainVM) : base(popupService, mainVM)
        {

        }
    }
}
