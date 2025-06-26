using Microsoft.Maui.Storage;

namespace WaterSortPuzzle.Models
{
    internal class AppSettings
    {
        public AppSettings()
        {
            var value = Preferences.Get("nameOfSetting", "defaultValueForSetting");

        }
    }
}
