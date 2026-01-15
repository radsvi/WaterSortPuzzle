using CommunityToolkit.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.ViewModels
{
    public partial class HelpPopupVM : FullscreenParameterlessPopupBaseVM
    {
        public MainVM MainVM { get; }
        public HelpPopupVM(IPopupService popupService, MainVM mainVM) : base(popupService, mainVM)
        {
            MainVM = mainVM;

            MainVM.ResetIndex();
        }
    }
}
