using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    public interface ILevelPreferences
    {
        int Level { get; set; }
        int Score { get; set; }
        bool BoostDifficulty { get; set; }
    }
    public partial class LevelPreferences : ObservableObject, ILevelPreferences
    {
        public int Level
        {
            get => Preferences.Default.Get(nameof(Level), 1);
            set => Preferences.Set(nameof(Level), value);
        }
        public int Score
        {
            get => Preferences.Default.Get(nameof(Score), 0);
            set => Preferences.Set(nameof(Score), value);
        }
        public bool BoostDifficulty
        {
            get => Preferences.Default.Get(nameof(BoostDifficulty), false);
            set => Preferences.Set(nameof(BoostDifficulty), value);
        }
    }
}
