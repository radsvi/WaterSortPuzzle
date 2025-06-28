using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.ViewModels
{
    public partial class DetailPageVM
    {
        [RelayCommand]
        public async Task NavigateBack() => await Shell.Current.GoToAsync($"..", true);
    }
}
