using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    public class SavedGameState
    {
        [JsonProperty] public int Source { get; private set; }
        [JsonProperty] public int Target { get; private set; }
        [JsonProperty] public BoardState BoardState { get; private set; }

        public SavedGameState(BoardState boardState, int sourceTube, int targetTube)
        {
            Source = sourceTube;
            Target = targetTube;
            BoardState = boardState;
        }

        public SavedGameState Clone()
        {
            return new SavedGameState(
                this.BoardState.Clone(),
                this.Source,
                this.Target);
        }
        public void UpdateSourceNTarget(int source, int target)
        {
            Source = source;
            Target = target;
        }
    }
}
