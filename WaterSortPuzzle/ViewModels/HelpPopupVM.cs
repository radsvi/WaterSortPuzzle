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
        public CoachMarkManager CoachMarkManager { get; }

        public ObservableCollection<CoachMarkItem> CoachMarks { get; } = [];

        [ObservableProperty]
        private CoachMarkItem? current;

        public HelpPopupVM(IPopupService popupService, MainVM mainVM, CoachMarkManager coachMarkManager) : base(popupService, mainVM)
        {
            CoachMarkManager = coachMarkManager;
            CoachMarkManager.Attach(this);
        }
    }
}
