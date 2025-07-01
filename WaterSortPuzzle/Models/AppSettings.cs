using Microsoft.Maui.Storage;

namespace WaterSortPuzzle.Models
{
    public partial class AppSettings : ObservableObject
    {
        const int maximumExtraTubesUpperLimit = 20;
        public bool LoadDebugLevel
        {
            get => Preferences.Default.Get(nameof(LoadDebugLevel), false);
            set
            {
                Preferences.Set(nameof(LoadDebugLevel), value);
                OnPropertyChanged(nameof(NewLevelButtonText));
            }
        }
        public string NewLevelButtonText
        {
            get
            {
                if (LoadDebugLevel) return "[DEBUG] level";
                else return "New level";
            }
        }
        public int NumberOfColorsToGenerate
        {
            get => Preferences.Default.Get(nameof(NumberOfColorsToGenerate), 10);
            set 
            {
                if (Preferences.Default.Get(nameof(NumberOfColorsToGenerate), 10) != value)
                {
                    if (value >= 3 && value <= LiquidColor.ColorKeys.Count)
                    {
                        Preferences.Set(nameof(NumberOfColorsToGenerate), value);
                    }
                    else if (value < 3)
                    {
                        Preferences.Set(nameof(NumberOfColorsToGenerate), 3);
                    }
                    else if (value > LiquidColor.ColorKeys.Count)
                    {
                        Preferences.Set(nameof(NumberOfColorsToGenerate), LiquidColor.ColorKeys.Count);
                    }
                    //OnPropertyChanged();
                    //OnGlobalPropertyChanged("NumberOfColorsToGenerate");
                }
            }
        }
        public bool RandomNumberOfTubes
        {
            get => Preferences.Default.Get(nameof(RandomNumberOfTubes), true);
            set => Preferences.Set(nameof(RandomNumberOfTubes), value);
        }
        public int MaximumExtraTubes
        {
            get => Preferences.Default.Get(nameof(MaximumExtraTubes), 2);
            set
            {
                if (Preferences.Default.Get(nameof(MaximumExtraTubes), 2) != value)
                {
                    if (value >= 0 && value <= maximumExtraTubesUpperLimit)
                    {
                        Preferences.Set(nameof(RandomNumberOfTubes), value);
                    }
                    else if (value < 0)
                    {
                        Preferences.Set(nameof(RandomNumberOfTubes), 0);
                    }
                    else if (value > maximumExtraTubesUpperLimit)
                    {
                        Preferences.Set(nameof(RandomNumberOfTubes), maximumExtraTubesUpperLimit);
                    }
                }
            }
        }
        public bool DontShowHelpScreenAtStart
        {
            get => Preferences.Default.Get(nameof(DontShowHelpScreenAtStart), false);
            set {
                Preferences.Set(nameof(DontShowHelpScreenAtStart), value);
                OnPropertyChanged();
            }
        }
        public bool AdvancedOptionsVisible
        {
            get => Preferences.Default.Get(nameof(AdvancedOptionsVisible), false);
            set
            {
                Preferences.Set(nameof(AdvancedOptionsVisible), value);
                OnPropertyChanged();
            }
        }
        public bool UnselectTubeEvenOnIllegalMove
        {
            get => Preferences.Default.Get(nameof(UnselectTubeEvenOnIllegalMove), true);
            set
            {
                Preferences.Set(nameof(UnselectTubeEvenOnIllegalMove), value);
                OnPropertyChanged();
            }
        }
        public int OptionsWindowHeight
        {
            get => Preferences.Default.Get(nameof(OptionsWindowHeight), 500);
            set => Preferences.Set(nameof(OptionsWindowHeight), value);
        }
        public int OptionsWindowWidth
        {
            get => Preferences.Default.Get(nameof(OptionsWindowWidth), 800);
            set => Preferences.Set(nameof(OptionsWindowWidth), value);
        }
        public string SavedLevels
        {
            get => Preferences.Default.Get(nameof(SavedLevels), string.Empty);
            set => Preferences.Set(nameof(SavedLevels), value);
        }

        public int TestValue { get; set; } = 5;
    }
}
