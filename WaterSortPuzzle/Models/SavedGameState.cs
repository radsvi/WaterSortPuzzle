using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    public class SavedGameState
    {
        private readonly BoardState boardState;

        public SavedGameState(BoardState boardState)
        {
            this.boardState = boardState;
        }
        private SavedGameState(SavedGameState original, BoardState boardState, int sourceTube, int targetTube)
        {
            this.boardState = original.boardState;

            GameGrid = boardState.Grid;
            Source = sourceTube;
            Target = targetTube;
            ColorsCounter = boardState.GetTubeCount();
            ExtraTubeCounter = boardState.ExtraTubesCounter;
        }
        //public SavedGameState(BoardState boardState, int sourceTube, int targetTube)
        //{
        //    GameGrid = boardState.Grid;
        //    Source = sourceTube;
        //    Target = targetTube;
        //    ColorsCounter = boardState.GetTubeCount();
        //    ExtraTubeCounter = boardState.ExtraTubesCounter;
        //}
        //private SavedGameState(LiquidColor[,] gameGrid, int sourceTube, int targetTube, int colorsCounter, int extraTubeCounter)
        //{
        //    GameGrid = gameGrid;
        //    Source = sourceTube;
        //    Target = targetTube;
        //    ColorsCounter = colorsCounter;
        //    ExtraTubeCounter = extraTubeCounter;
        //}


        public LiquidColor[,] GameGrid { get; private set; }
        public int Source { get; private set; }
        public int Target { get; private set; }
        public int ColorsCounter { get; private set; }
        public int ExtraTubeCounter { get; private set; }
        //public static SavedGameState Clone(SavedGameState original)
        //{
        //    return new SavedGameState(BoardState.CloneGrid(original.GameGrid), original.Source, original.Target, original.ColorsCounter, original.ExtraTubeCounter);
        //}
        public SavedGameState Clone(BoardState boardState, int sourceTube, int targetTube)
        {
            return new SavedGameState(this, boardState, sourceTube, targetTube);
        }
        public void UpdateSourceNTarget(int source, int target)
        {
            Source = source;
            Target = target;
        }
    }
}
