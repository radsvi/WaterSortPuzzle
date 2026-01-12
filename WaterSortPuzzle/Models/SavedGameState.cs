using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    public class SavedGameState
    {
        public SavedGameState(LiquidColor[,] gameGrid, int source, int target)
        {
            GameGrid = gameGrid;
            Source = source;
            Target = target;
        }

        public LiquidColor[,] GameGrid { get; private set; }
        public int Source { get; private set; }
        public int Target { get; private set; }
        public int ExtraTubeCounter { get; private set; }
        public static SavedGameState Clone(SavedGameState original)
        {
            return new SavedGameState(GameState.CloneGridStatic(original.GameGrid), original.Source, original.Target);
        }
        public void UpdateSourceNTarget(int source, int target)
        {
            Source = source;
            Target = target;
        }
    }
}
