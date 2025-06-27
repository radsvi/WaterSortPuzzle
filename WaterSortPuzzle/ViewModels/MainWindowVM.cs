using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.ViewModels
{
    public partial class MainWindowVM : ViewModelBase
    {
        public MainPage MainPage { get; set; }
        public MainWindowVM(MainPage mainPage)
        {
            MainPage = mainPage;
        }

        [RelayCommand]
        async Task Navigate() =>
            await AppShell.Current.GoToAsync(nameof(DetailPage));
    }
}
