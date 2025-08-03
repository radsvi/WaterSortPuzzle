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

        public int NumberOfColorsToGenerate { get; private set; } = 3;
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

        public void LevelFinished()
        {
            Level++;
            IncreaseScore();
        }
        
        void IncreaseScore()
        {
            var scoreMultiplier = Constants.DefaultScoreMultiplier;

            


            Score += scoreMultiplier;

        }
        void CalculateNextLevelParameters()
        {
            var difficulty = GetDifficulty();
            int rand = new Random().Next(-2, 0);

            NumberOfColorsToGenerate = difficulty + Constants.MinColors + rand;
        }
        int GetDifficulty()
        {
            //int difficulty = 0;
            //if (Level <= 10)
            //    difficulty += (int)Level / 2 + Constants.MinColors;
            //else if (Level <= 40)
            //    difficulty += (int)Level / 2 + Constants.MinColors;

            int difficulty;
            if (Level <= 2)
                difficulty = 0;
            else if (Level <= 4)
                difficulty = 1;
            else if (Level <= 6)
                difficulty = 2;
            else if (Level <= 8)
                difficulty = 3;
            else if (Level <= 12)
                difficulty = 4;
            else if (Level <= 16)
                difficulty = 5;
            else if (Level <= 20)
                difficulty = 6;
            else if (Level <= 30)
                difficulty = 7;
            else if (Level <= 40)
                difficulty = 8;
            else
                difficulty = 9;

            return difficulty;
        }

    }
}
