
namespace WaterSortPuzzle.Models
{
    public class StoredLevel : ViewModelBase
    {
        [JsonProperty] public int NumberOfColors { get; private set; }
        [JsonProperty] public DateTime Date { get; private set; }
        [JsonProperty] public string Note { get; private set; }
        [JsonProperty] public BoardState BoardState { get; private set; }
        public List<TubeData> GameGridDisplayList { get; private set; } = new List<TubeData>();
        private bool markedForDeletion;
        public bool MarkedForDeletion
        {
            get { return markedForDeletion; }
            set
            {
                if (value != markedForDeletion)
                {
                    markedForDeletion = value;
                    OnPropertyChanged();
                }
            }
        }
        [JsonConstructor]
        public StoredLevel(BoardState boardState, string noteForSavedLevel)
        {
            if (boardState is null)
                return;

            this.BoardState = boardState;
            this.Date = DateTime.Now;
            this.Note = noteForSavedLevel;

            List<LiquidColorName?> colorIds = new List<LiquidColorName?>();
            for (int x = 0; x < boardState.GetLength(0); x++)
            {
                for (int y = 0; y < boardState.GetLength(1); y++)
                {
                    if (boardState[x, y] is null)
                    {
                        continue;
                    }
                    if (colorIds.Contains(boardState[x,y].Name) == false)
                    {
                        colorIds.Add(boardState[x, y].Name);
                        this.NumberOfColors++;
                    }
                }
            }
        }
        public void GenerateArrayToTubeList()
        {
            GameGridDisplayList?.Clear();
            for (int x = 0; x < BoardState.GetLength(0); x++)
            {
                var row = new TubeData(BoardState[x, 0], BoardState[x, 1], BoardState[x, 2], BoardState[x, 3]);

                GameGridDisplayList!.Add(row);
            }
        }
    }
}
