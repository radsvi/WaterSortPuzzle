using CommunityToolkit.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.ViewModels
{
    public partial class CustomPopupViewModel : ObservableObject
    {
        [ObservableProperty]
        public string name = string.Empty;

        readonly IPopupService popupService;

        public CustomPopupViewModel(IPopupService popupService)
        {
            this.popupService = popupService;
        }

        void OnCancel()
        {
        }

        [RelayCommand(CanExecute = nameof(CanSave))]
        void OnSave()
        {
        }

        bool CanSave() => string.IsNullOrWhiteSpace(Name) is false;
    }
}
