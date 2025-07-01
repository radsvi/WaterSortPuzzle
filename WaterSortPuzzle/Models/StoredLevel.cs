﻿
namespace WaterSortPuzzle.Models
{
    public class StoredLevel : ViewModelBase
    {
        [JsonProperty] public int NumberOfColors { get; private set; }
        [JsonProperty] public DateTime Date { get; private set; }
        [JsonProperty] public string Note { get; private set; }
        [JsonProperty] public LiquidColor[,] GameGrid { get; private set; }
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
        public StoredLevel(LiquidColor[,] gameGrid, string noteForSavedLevel)
        {
            if (gameGrid is null)
            {
                return;
            }
            this.GameGrid = gameGrid;
            this.Date = DateTime.Now;
            this.Note = noteForSavedLevel;

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
            //List<Tube> list = new List<Tube>();
            GameGridDisplayList?.Clear();
            for (int x = 0; x < GameGrid.GetLength(0); x++)
            {
                var row = new TubeData(GameGrid[x, 0], GameGrid[x, 1], GameGrid[x, 2], GameGrid[x, 3]);

                GameGridDisplayList!.Add(row);
            }
            //return list;
        }
        //public StoredLevel(int?[,] gameGridInt, string noteForSavedLevel)
        //{
        //    if (gameGridInt is null)
        //    {
        //        return;
        //    }
        //    this.GameGrid = new LiquidColorNew[gameGridInt.GetLength(0), gameGridInt.GetLength(1)];
        //    this.Date = DateTime.Now;
        //    this.Note = noteForSavedLevel;

        //    List<LiquidColorNames?> colorIds = new List<LiquidColorNames?>();
        //    for (int x = 0; x < gameGridInt.GetLength(0); x++)
        //    {
        //        for (int y = 0; y < gameGridInt.GetLength(1); y++)
        //        {
        //            if (gameGridInt[x, y] is null)
        //            {
        //                continue;
        //            }
        //            this.GameGrid[x, y] = new LiquidColorNew((int)gameGridInt[x, y]);
        //            if (colorIds.Contains(this.GameGrid[x, y].Name) == false)
        //            {
        //                colorIds.Add(this.GameGrid[x, y].Name);
        //                this.NumberOfColors++;
        //            }
        //        }
        //    }
        //}
        //public StoredLevel(int?[,] gameGridInt, string noteForSavedLevel) : this(gameGrid, noteForSavedLevel)
        //{
        //    var gameGrid = new LiquidColorNew[gameGridInt.GetLength(0), gameGridInt.GetLength(1)];
        //    for (int x = 0; x < gameGridInt.GetLength(0); x++)
        //    {
        //        for (int y = 0; y < gameGridInt.GetLength(1); y++)
        //        {
        //            if (gameGridInt[x, y] is null)
        //            {
        //                continue;
        //            }
        //            GameGrid[x, y] = new LiquidColorNew((int)gameGridInt[x, y]);
        //        }
        //    }


        //}
    }
}
