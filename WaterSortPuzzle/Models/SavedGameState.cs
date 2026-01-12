using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    public class SavedGameState
    {
        public SavedGameState(BoardState boardState, int sourceTube, int targetTube)
        {
            Grid = boardState.Grid;
            Source = sourceTube;
            Target = targetTube;
            ColorsCounter = boardState.GetTubeCount();
            ExtraTubesCounter = boardState.ExtraTubesCounter;
        }
        public SavedGameState(SavedGameState savedGameState, LiquidColor[,] grid)
        {
            Grid = grid;
            Source = savedGameState.Source;
            Target = savedGameState.Target;
            ColorsCounter = savedGameState.ColorsCounter;
            ExtraTubesCounter = savedGameState.ExtraTubesCounter;
        }
        public SavedGameState(LiquidColor[,] gameGrid, int sourceTube, int targetTube, int colorsCounter, int extraTubeCounter)
        {
            Grid = gameGrid;
            Source = sourceTube;
            Target = targetTube;
            ColorsCounter = colorsCounter;
            ExtraTubesCounter = extraTubeCounter;
        }


        public LiquidColor[,] Grid { get; private set; }
        public int Source { get; private set; }
        public int Target { get; private set; }
        public int ColorsCounter { get; private set; }
        public int ExtraTubesCounter { get; private set; }
        public static SavedGameState Clone(SavedGameState original)
        {
            return new SavedGameState(original, GridHelper.CloneGrid(original.Grid));
        }
        //public SavedGameState Clone(BoardState boardState, int sourceTube, int targetTube)
        //{
        //    return new SavedGameState(this, boardState, sourceTube, targetTube);
        //}
        
        public void UpdateSourceNTarget(int source, int target)
        {
            Source = source;
            Target = target;
        }
    }
}
