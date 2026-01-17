namespace WaterSortPuzzle.Models
{
    public partial class AppPreferences : ObservableObject
    {
        public AppPreferences()
        {
            LoadAnimationDuration();
        }

        public bool LoadDebugLevel
        {
            get => Preferences.Default.Get(nameof(LoadDebugLevel), false);
            set
            {
                Preferences.Set(nameof(LoadDebugLevel), value);
                OnPropertyChanged();
            }
        }
        public int NumberOfColorsToGenerate
        {
            get => Preferences.Default.Get(nameof(NumberOfColorsToGenerate), 10);
            set 
            {
                if (Preferences.Default.Get(nameof(NumberOfColorsToGenerate), 10) != value)
                {
                    if (value < Constants.MinColors)
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
        public bool SetSpecificNumberOfTubes { get => !RandomNumberOfTubes; } // for XAML
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
        public ObservableCollection<SavedGameState> SavedGameStatesBeforeSleepV2
        {
            get
            {
                string serialized = Preferences.Default.Get(nameof(SavedGameStatesBeforeSleepV2) + "_Serialized", string.Empty);
                return JsonConvert.DeserializeObject<ObservableCollection<SavedGameState>>(serialized)!;
            }
            set
            {
                string savedLevelList = JsonConvert.SerializeObject(value);
                Preferences.Set(nameof(SavedGameStatesBeforeSleepV2) + "_Serialized", savedLevelList);
            }
        }
        public int StepBackCounter
        {
            get => Preferences.Default.Get(nameof(StepBackCounter), 0);
            set => Preferences.Set(nameof(StepBackCounter), value);
        }
        

        public bool UnlimitedStepBack
        {
            get => Preferences.Default.Get(nameof(UnlimitedStepBack), false);
            set { Preferences.Set(nameof(UnlimitedStepBack), value); OnPropertyChanged(); }
        }
        public AppTheme ThemeUserSetting
        {
            get
            {
                var text = Preferences.Default.Get(nameof(ThemeUserSetting), AppTheme.Unspecified.ToString());
                if (Enum.TryParse(text, out AppTheme result))
                    return result;

                throw new InvalidOperationException();
            }
            set
            {
                Preferences.Set(nameof(ThemeUserSetting), value.ToString());
                App.Current!.UserAppTheme = value;
                //OnPropertyChanged();
            }
        }
        public static uint RepositionDuration { get; private set; } = 0;
        public static uint PouringDuration { get; private set; } = 0;
        public static double AnimationDurationMultiplier { get; private set; } = 0;
        public AnimationSpeed AnimationSpeed
        {
            get
            {
                var text = Preferences.Default.Get(nameof(AnimationSpeed), AnimationSpeed.Fastest.ToString());
                if (Enum.TryParse(text, out AnimationSpeed result))
                    return result;
                
                throw new InvalidOperationException();
            }
            set
            {
                Preferences.Set(nameof(AnimationSpeed), value.ToString());

                LoadAnimationDuration();
            }
        }
        void LoadAnimationDuration()
        {
            switch (AnimationSpeed)
            {
                case AnimationSpeed.Standard:
                    RepositionDuration = 250;
                    PouringDuration = 600;
                    AnimationDurationMultiplier = 1;
                    break;
                case AnimationSpeed.Fast:
                    RepositionDuration = 100;
                    PouringDuration = 220;
                    AnimationDurationMultiplier = 1;
                    break;
                case AnimationSpeed.Fastest:
                    RepositionDuration = 100;
                    PouringDuration = 220;
                    AnimationDurationMultiplier = 0;
                    break;
                //case AnimationSpeed.Instant:
                //    // this option ignores other values and follows different logic
                //    break;
            }
        }
        public bool DeveloperOptions
        {
            get => Preferences.Default.Get(nameof(DeveloperOptions), false);
            set {
                Preferences.Set(nameof(DeveloperOptions), value);
                OnPropertyChanged();
                if (value == false)
                    SingleLevelMode = false;
            }
        }
        public bool SingleLevelMode
        {
            get => Preferences.Default.Get(nameof(SingleLevelMode), false);
            set
            {
                Preferences.Set(nameof(SingleLevelMode), value);
                OnPropertyChanged();
                OnPropertyChanged(nameof(LevelingMode));
            }
        }
        public bool LevelingMode { get => !SingleLevelMode; }
    }
}
