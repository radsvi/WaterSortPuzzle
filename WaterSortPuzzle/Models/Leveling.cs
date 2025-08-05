using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    public partial class Leveling : ObservableObject
    {
        //readonly AppPreferences appPreferences;
        //public Leveling(AppPreferences appPreferences)
        //{
        //    this.appPreferences = appPreferences;
        //}
        public Leveling()
        {
            CalculateNextLevelParameters();
        }

        //public int NumberOfColorsToGenerate { get; private set; } = 3;
        private int numberOfColorsToGenerate = 3;
        public int NumberOfColorsToGenerate
        {
            get => numberOfColorsToGenerate;
            set 
            {
                if (numberOfColorsToGenerate != value)
                {
                    if (value < 3)
                    {
                        numberOfColorsToGenerate = Constants.MinColors;
                    }
                    else if (value > Constants.ColorCount)
                    {
                        numberOfColorsToGenerate = Constants.ColorCount;
                    }
                    else
                    {
                        numberOfColorsToGenerate = value;
                    }

                    OnPropertyChanged();
                }
            }
        }
        //public int Difficulty
        //{
        //    get => Preferences.Default.Get(nameof(Difficulty), 0);
        //    set => Preferences.Set(nameof(Difficulty), value);
        //}
        public int Level
        {
            get => Preferences.Default.Get(nameof(Level), 1);
            set {
                Preferences.Set(nameof(Level), value);
                OnPropertyChanged();
                CalculateNextLevelParameters();
            }
        }
        public int Score
        {
            get => Preferences.Default.Get(nameof(Score), 0);
            set { Preferences.Set(nameof(Score), value); OnPropertyChanged(); }
        }

        public void LevelFinished(int colorCount)
        {
            Level++;
            IncreaseScore(colorCount);
        }
        
        void IncreaseScore(int colorCount)
        {
            double difficultyMultiplier = colorCount / Constants.ColorCount;
            var scoreMultiplier = (int)(Constants.DefaultScoreMultiplier * difficultyMultiplier);

            Score += scoreMultiplier;
        }
        void CalculateNextLevelParameters()
        {
            var difficulty = GetDifficulty();

            int rand;
            if (difficulty > 11)
                rand = (-(int)(new Random().Next(0, 4) / 3));
            else if (difficulty == 11)
                rand = new Random().Next(-1, 0);
            else
                rand = new Random().Next(-2, 0);

            NumberOfColorsToGenerate = difficulty + Constants.MinColors + rand;
        }
        int GetDifficulty()
        {
            int difficulty;

            if (Score >= 2892)
                difficulty = 11;
            else if (Score >= 2082)
                difficulty = 10;
            else if (Score >= 1332)
                difficulty = 9;
            else if (Score >= 892)
                difficulty = 8;
            else if (Score >= 492)
                difficulty = 7;
            else if (Score >= 312)
                difficulty = 6;
            else if (Score >= 152)
                difficulty = 5;
            else if (Score >= 96)
                difficulty = 4;
            else if (Score >= 48)
                difficulty = 3;
            else if (Score >= 28)
                difficulty = 2;
            else if (Score >= 12)
                difficulty = 1;
            else
                difficulty = 0;

            return difficulty;
        }
    }
}
