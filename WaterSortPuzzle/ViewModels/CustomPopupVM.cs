using CommunityToolkit.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.ViewModels
{
    public partial class CustomPopupVM : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        public string title = string.Empty;
        [ObservableProperty]
        public string message = string.Empty;
        [ObservableProperty]
        public string accept = string.Empty;
        [ObservableProperty]
        public string cancel = string.Empty;

        readonly IPopupService popupService;

        public CustomPopupVM(IPopupService popupService)
        {
            this.popupService = popupService;
        }
        [RelayCommand]
        private async Task OnCancel()
        {
            await popupService.ClosePopupAsync(Shell.Current, false);
        }

        //[RelayCommand(CanExecute = nameof(CanSave))]
        [RelayCommand]
        private async Task OnSave()
        {
            await popupService.ClosePopupAsync(Shell.Current, true);
        }

        //bool CanSave() => string.IsNullOrWhiteSpace(Title) is false;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Title = (string)query[nameof(CustomPopupVM.Title)];
            Message = (string)query[nameof(CustomPopupVM.Message)];
            Accept = (string)query[nameof(CustomPopupVM.Accept)];
            Cancel = (string)query[nameof(CustomPopupVM.Cancel)];
        }
    }
}
