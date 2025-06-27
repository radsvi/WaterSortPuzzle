using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.ViewModels
{
    partial class MainWindowVM : ViewModelBase
    {
        [RelayCommand]
        async Task Navigate() =>
            await AppShell.Current.GoToAsync(nameof(DetailPage));
    }
}
