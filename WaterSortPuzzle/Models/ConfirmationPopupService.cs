using CommunityToolkit.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    public interface IConfirmationPopupService
    {
        //Task<bool> ShowPopupAsync(string title, string message, string accept, string cancel);
        //Task<bool> ShowFullscreenPopupAsync(string title, string message, string accept, string cancel);
        Task<bool> ShowPopupAsync<T>(string title, string message, string accept, string cancel) where T : CustomPopupVM;
        Task ShowParameterlessPopupAsync<T>() where T : QuickOptionsPopupVM;
    }

    public class ConfirmationPopupService : IConfirmationPopupService
    {
        private readonly IPopupService popupService;

        public ConfirmationPopupService(IPopupService popupService)
        {
            this.popupService = popupService;
        }
        //public async Task<bool> ShowPopupAsync(string title, string message, string accept, string cancel)
        //{
        //    return await ShowPopupAsyncBase<CustomPopupVM>(title, message, accept, cancel);
        //}
        //public async Task<bool> ShowFullscreenPopupAsync(string title, string message, string accept, string cancel)
        //{
        //    return await ShowPopupAsyncBase<FullScreenPopupVM>(title, message, accept, cancel);
        //}
        public async Task<bool> ShowPopupAsync<T>(string title, string message, string accept, string cancel) where T : CustomPopupVM
        {
            var queryAttributes = new Dictionary<string, object>
            {
                [nameof(CustomPopupVM.Title)] = title,
                [nameof(CustomPopupVM.Message)] = message,
                [nameof(CustomPopupVM.Accept)] = accept,
                [nameof(CustomPopupVM.Cancel)] = cancel
            };

            CommunityToolkit.Maui.Core.IPopupResult<bool> result = await this.popupService.ShowPopupAsync<T, bool>(
                Shell.Current,
                options: PopupOptions.Empty,
                shellParameters: queryAttributes);

            if (!result.WasDismissedByTappingOutsideOfPopup)
            {
                return result.Result;
            }
            else
            {
                return false;
            }
        }
        public async Task ShowParameterlessPopupAsync<T>() where T : QuickOptionsPopupVM
        {
            await this.popupService.ShowPopupAsync<T, bool>(
                Shell.Current,
                options: PopupOptions.Empty);
        }
    }
}
