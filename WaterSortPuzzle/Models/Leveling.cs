using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    public partial class Leveling : ObservableObject
    {
        readonly ILevelPreferences levelPreferences;
        public Leveling(ILevelPreferences levelPreferences)
        {
            //this.levelPreferences = (LevelPreferences)levelPreferences;
            this.levelPreferences = levelPreferences;

            CalculateNextLevelParameters();
        }

        public int Level
        {
            get => levelPreferences.Level;
            set { levelPreferences.Level = value; OnPropertyChanged(); CalculateNextLevelParameters(); }
        }
        public int Score
        {
            get => levelPreferences.Score;
            set { levelPreferences.Score = value; OnPropertyChanged(); }
        }
        public bool BoostDifficulty
        {
            get => levelPreferences.BoostDifficulty;
            set { levelPreferences.BoostDifficulty = value; OnPropertyChanged(); }
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

        //private int score = 5;
        //public int Score
        //{
        //    get => score;
        //    set => score = value;
        //    //set { Preferences.Set(nameof(Score), value); OnPropertyChanged(); }
        //}
        //public int Score
        //{
        //    get => Preferences.Default.Get(nameof(Score), 0);
        //    set { Preferences.Set(nameof(Score), value); OnPropertyChanged(); }
        //}



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
        public void CalculateNextLevelParameters()
        {
            var difficulty = GetDifficulty();

            //rand = 0;
            //if (difficulty > 11)
            //    rand = (-(int)(new Random().Next(0, 4) / 3));
            //else if (difficulty == 11)
            //    rand = new Random().Next(-1, 0);
            //else
            //    rand = new Random().Next(-2, 0);
            if (BoostDifficulty == true)
            {
                if (difficulty + Constants.MinColors > Constants.ColorCount)
                    NumberOfColorsToGenerate = Constants.ColorCount;
                else
                    NumberOfColorsToGenerate = difficulty + Constants.MinColors;
            }
            else
            {
                if (difficulty > (Constants.ColorCount - Constants.MinColors + 1)) // diff 11+
                {
                    NumberOfColorsToGenerate = Constants.ColorCount + (-(int)(new Random().Next(0, 4) / 3));
                }
                else if (difficulty > (Constants.ColorCount - Constants.MinColors)) // diff 10
                {
                    NumberOfColorsToGenerate = Constants.ColorCount + new Random().Next(-1, 0);
                }
                else // diff 0~9
                {
                    NumberOfColorsToGenerate = difficulty + Constants.MinColors + new Random().Next(-2, 0);
                }
            }
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
