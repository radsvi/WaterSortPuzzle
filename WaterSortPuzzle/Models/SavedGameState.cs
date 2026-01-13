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
            Source = sourceTube;
            Target = targetTube;
            BoardState = boardState;
        }


        public int Source { get; private set; }
        public int Target { get; private set; }
        public BoardState BoardState { get; private set; }
        public static SavedGameState Clone(SavedGameState original)
        {
            return new SavedGameState(
                original.BoardState.Clone(),
                original.Source,
                original.Target);
        }
        
        public void UpdateSourceNTarget(int source, int target)
        {
            Source = source;
            Target = target;
        }
    }
}
