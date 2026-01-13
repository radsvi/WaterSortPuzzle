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
                    if (value < Constants.MinColors)
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
            IncreaseScore(colorCount);
            Level++; // V setteru mam rovnou kalkulovani obtiznosti dalsiho levelu. blbej design pro priste...
        }
        
        void IncreaseScore(int colorCount)
        {
            double difficultyMultiplier = (double)colorCount / Constants.ColorCount;
            var scoreMultiplier = (int)(Constants.DefaultScoreMultiplier * difficultyMultiplier);

            // sometimes it doesn't increase score on the very first level, and I haven't figured out why, or even when it happens
            // so introducing this workaround to hopefully make it work correctly always:

            if (scoreMultiplier == 0)
                scoreMultiplier = 6;

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
                    NumberOfColorsToGenerate = difficulty + Constants.MinColors;
                }
            }
        }
        private int GetDifficulty()
        {
            int difficulty;

            if (Score >= 2892)
                difficulty = 11;
            else if (Score >= 1040)
                difficulty = 10;
            else if (Score >= 540)
                difficulty = 9;
            else if (Score >= 320)
                difficulty = 8;
            else if (Score >= 120)
                difficulty = 7;
            else if (Score >= 30)
                difficulty = 6;
            else if (Score >= 16)
                difficulty = 4;
            else if (Score >= 6)
                difficulty = 2;
            else
                difficulty = 0;

            return difficulty;
        }
    }
}
