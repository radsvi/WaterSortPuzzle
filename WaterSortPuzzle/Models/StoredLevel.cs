
namespace WaterSortPuzzle.Models
{
    public class StoredLevel : ViewModelBase
    {
        [JsonProperty] public int NumberOfColors { get; private set; }
        [JsonProperty] public DateTime Date { get; private set; }
        [JsonProperty] public string Note { get; private set; }
        [JsonProperty] public LiquidColor[,] GameGrid { get; private set; }
        [JsonProperty] public int ExtraTubesCounter { get; private set; }
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
        public StoredLevel(LiquidColor[,] gameGrid, int extraTubesCounter, string noteForSavedLevel)
        {
            if (gameGrid is null)
            {
                return;
            }
            this.GameGrid = gameGrid;
            this.Date = DateTime.Now;
            this.Note = noteForSavedLevel;
            this.ExtraTubesCounter = extraTubesCounter;

            List<LiquidColorName?> colorIds = new List<LiquidColorName?>();
            for (int x = 0; x < gameGrid.GetLength(0); x++)
            {
                for (int y = 0; y < gameGrid.GetLength(1); y++)
                {
                    if (gameGrid[x, y] is null)
                    {
                        continue;
                    }
                    if (colorIds.Contains(gameGrid[x,y].Name) == false)
                    {
                        colorIds.Add(gameGrid[x, y].Name);
                        this.NumberOfColors++;
                    }
                }
            }
        }
        public void GenerateArrayToTubeList()
        {
            GameGridDisplayList?.Clear();
            for (int x = 0; x < GameGrid.GetLength(0); x++)
            {
                var row = new TubeData(GameGrid[x, 0], GameGrid[x, 1], GameGrid[x, 2], GameGrid[x, 3]);

                GameGridDisplayList!.Add(row);
            }
        }
    }
}
