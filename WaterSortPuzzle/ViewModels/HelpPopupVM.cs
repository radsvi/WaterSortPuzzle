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
        public CoachMarkManager CoachMarkManager { get; }

        public HelpPopupVM(IPopupService popupService, MainVM mainVM, CoachMarkManager coachMarkManager) : base(popupService, mainVM)
        {
            CoachMarkManager = coachMarkManager;

            CoachMarkManager.MoveToFirst();

            mainVM.PropertyChanged += (_, __) =>
            {
                CoachMarkManager.UpdateAvailableMarks();
            };
        }
    }
}
