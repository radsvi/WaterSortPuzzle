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
        //public CoachMarkManager CoachMarkManager { get; }
        //public ObservableCollection<CoachMarkItem> CoachMarks { get; } = [];
        ////public ObservableCollection<CoachMarkItem> AvailableCoachMarks { get; } = [];
        //public CoachMarkItem? Current { get; set; }


        public HelpPopupVM(IPopupService popupService, MainVM mainVM, CoachMarkManager coachMarkManager) : base(popupService, mainVM)
        {
            MainVM = mainVM;
            //CoachMarkManager = coachMarkManager;

            //CoachMarkManager.Attach(this);

            //CoachMarkManager.MoveToFirst();

            //mainVM.PropertyChanged += (_, __) =>
            //{
            //    CoachMarkManager.UpdateAvailableMarks();
            //};
        }
    }
}
