using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    public class SavedGameState
    {
        public SavedGameState(LiquidColor[,] gameGrid, int source, int target, int colorsCounter, int extraTubeCounter)
        {
            GameGrid = gameGrid;
            Source = source;
            Target = target;
            ColorsCounter = colorsCounter;
            ExtraTubeCounter = extraTubeCounter;
        }

        public LiquidColor[,] GameGrid { get; private set; }
        public int Source { get; private set; }
        public int Target { get; private set; }
        public int ColorsCounter { get; private set; }
        public int ExtraTubeCounter { get; private set; }
        public static SavedGameState Clone(SavedGameState original)
        {
            return new SavedGameState(GameState.CloneGrid(original.GameGrid), original.Source, original.Target, original.ColorsCounter, original.ExtraTubeCounter);
        }
        public void UpdateSourceNTarget(int source, int target)
        {
            Source = source;
            Target = target;
        }
    }
}
