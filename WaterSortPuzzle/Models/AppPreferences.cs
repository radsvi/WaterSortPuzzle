namespace WaterSortPuzzle.Models
{
    public partial class AppPreferences : ObservableObject
    {
        public AppPreferences() {}

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
                    if (value < 3)
                    {
                        Preferences.Set(nameof(NumberOfColorsToGenerate), Constants.MinColors);
                    }
                    else if (value > Constants.ColorCount)
                    {
                        Preferences.Set(nameof(NumberOfColorsToGenerate), Constants.ColorCount);
                    }
                    else
                    {
                        Preferences.Set(nameof(NumberOfColorsToGenerate), value);
                    }

                    OnPropertyChanged();
                    //OnGlobalPropertyChanged("NumberOfColorsToGenerate");
                }
            }
        }
        public bool RandomNumberOfTubes
        {
            get => Preferences.Default.Get(nameof(RandomNumberOfTubes), true);
            set
            {
                Preferences.Set(nameof(RandomNumberOfTubes), value);
                OnPropertyChanged(nameof(SetSpecificNumberOfTubes));
            }
        }
        public bool SetSpecificNumberOfTubes
        {
            get => !RandomNumberOfTubes;
        }
        
        public int MaximumExtraTubes
        {
            get => Preferences.Default.Get(nameof(MaximumExtraTubes), 1);
            set
            {
                if (Preferences.Default.Get(nameof(MaximumExtraTubes), 1) != value)
                {
                    if (value < Constants.MinimumNumberOfExtraTubesAllowedToBeAdded)
                    {
                        Preferences.Set(nameof(MaximumExtraTubes), Constants.MinimumNumberOfExtraTubesAllowedToBeAdded);
                    }
                    else if (value > Constants.MaximumExtraTubesUpperLimit)
                    {
                        Preferences.Set(nameof(MaximumExtraTubes), Constants.MaximumExtraTubesUpperLimit);
                    }
                    else
                    {
                        Preferences.Set(nameof(MaximumExtraTubes), value);
                    }

                    OnPropertyChanged();
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
        public StoredLevel LastLevelBeforeClosing
        {
            //get => Preferences.Default.Get(nameof(LastLevelBeforeClosing), new NullStoredLevel());
            get
            {
                //var temp = Preferences.Default.Get(nameof(LastLevelBeforeClosing), new StoredLevel(new LiquidColor[0, 0], ""));
                string serialized = Preferences.Default.Get(nameof(LastLevelBeforeClosing) + "_Serialized", string.Empty);
                return JsonConvert.DeserializeObject<StoredLevel>(serialized)!;
            }
            set
            {
                string savedLevelList = JsonConvert.SerializeObject(value);
                Preferences.Set(nameof(LastLevelBeforeClosing) + "_Serialized", savedLevelList);
            }
        }
        public bool UnlimitedStepBack
        {
            get => Preferences.Default.Get(nameof(UnlimitedStepBack), false);
            set { Preferences.Set(nameof(UnlimitedStepBack), value); OnPropertyChanged(); }
        }
        public bool InstantAnimations
        {
            get => Preferences.Default.Get(nameof(InstantAnimations), false);
            set => Preferences.Set(nameof(InstantAnimations), value);
        }
        public AppTheme ThemeUserSetting
        {
            get
            {
                var text = Preferences.Default.Get(nameof(ThemeUserSetting) + "_string", AppTheme.Unspecified.ToString());
                Enum.TryParse(text, out AppTheme result);
                return result;
            }
            set
            {
                Preferences.Set(nameof(ThemeUserSetting) + "_string", value.ToString());
                OnPropertyChanged();
            }
        }
    }
}
