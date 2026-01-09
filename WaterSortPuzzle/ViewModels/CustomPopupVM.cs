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
        public string name = string.Empty;

        readonly IPopupService popupService;

        public CustomPopupVM(IPopupService popupService)
        {
            this.popupService = popupService;
        }
        [RelayCommand]
        private async Task OnCancel()
        {
            await popupService.ClosePopupAsync(Shell.Current);
        }

        [RelayCommand(CanExecute = nameof(CanSave))]
        private async Task OnSave()
        {
            await popupService.ClosePopupAsync(Shell.Current, Name);
        }

        bool CanSave() => string.IsNullOrWhiteSpace(Name) is false;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Name = (string)query[nameof(CustomPopupVM.Name)];
        }
    }
}
