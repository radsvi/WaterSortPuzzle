using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    public class SavedGameState
    {
        public SavedGameState(LiquidColor[,] gameState, int source, int target)
        {
            GameState = gameState;
            Source = source;
            Target = target;
        }

        public LiquidColor[,] GameState { get; private set; }
        public int Source { get; private set; }
        public int Target { get; private set; }
    }
}
