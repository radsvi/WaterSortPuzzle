using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    public class Leveling
    {
        readonly AppPreferences appPreferences;
        public Leveling(AppPreferences appPreferences)
        {
            this.appPreferences = appPreferences;
        }

        public int NumberOfColorsToGenerate { get; private set; } = 3;

        public void IncreaseLevel()
        {
            appPreferences.LevelNumber++;
            IncreaseScore();
        }
        
        public void IncreaseScore()
        {
            var scoreMultiplier = Constants.DefaultScoreMultiplier;

            //


            appPreferences.Score++;

        }
    }
}
