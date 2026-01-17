using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    public partial class BoardState : ObservableObject
    {
        private readonly AppPreferences appPreferences;
        public string ReadableState => BoardStateToString(Grid, StringFormat.Numbers);
        public LiquidColor[,] Grid { get; set; }
        private int extraTubesCounter = 0;
        public int ExtraTubesCounter
        {
            get { return extraTubesCounter; }
            private set
            {
                if (value != extraTubesCounter)
                {
                    extraTubesCounter = value;
                    OnPropertyChanged();
                }
            }
        }
        




        public BoardState(AppPreferences appPreferences)
        {
            this.appPreferences = appPreferences;
        }

        private BoardState(BoardState source, int incrementBy = 0)
        {
            appPreferences = source.appPreferences;
            ExtraTubesCounter = source.ExtraTubesCounter;
            Grid = GridHelper.CloneGrid(source.Grid, incrementBy);
        }
        private BoardState(BoardState source, StoredLevel storedLevel)
        {
            appPreferences = source.appPreferences;

            ExtraTubesCounter = storedLevel.BoardState.ExtraTubesCounter;
            Grid = storedLevel.BoardState.Grid;
        }
        private BoardState(BoardState source, LiquidColor[,] grid)
        {
            appPreferences = source.appPreferences;

            ExtraTubesCounter = 0;
            Grid = grid;
        }


        public LiquidColor this[int tubes, int layers]
        {
            get => Grid[tubes, layers];
            set
            {
                Grid[tubes, layers] = value;
                //OnLiquidMoving();
            }
        }




        public static string BoardStateToString(LiquidColor[,] boardState, StringFormat format = StringFormat.Names, bool enableSort = true)
        {
            List<string> internalBoardState = new List<string>();
            for (int x = 0; x < boardState.GetLength(0); x++)
            {
                string tubeString = "[";
                for (int y = boardState.GetLength(1) - 1; y >= 0; y--)
                {
                    if (boardState[x, y] is not null)
                    {
                        //tubeInt += (int)gameState[x, y].Name * (int)Math.Pow(100,y);
                        if (format == StringFormat.Names)
                        {
                            tubeString += (boardState[x, y].Name).ToString();
                        }
                        else
                        {
                            tubeString += ((int)boardState[x, y].Name).ToString("00"); // this format is used for debugging. To easily export the gamestate as a string.
                        }
                    }
                    else
                        tubeString += "-";
                    if (y > 0) tubeString += ".";
                }
                tubeString += "]";
                internalBoardState.Add(tubeString);
            }
            if (enableSort)
            {
                internalBoardState.Sort(); // nechci sortovat kdyz chci vizualizaci
            }
            string stringBoardState = string.Empty;
            foreach (var tube in internalBoardState)
            {
                stringBoardState += tube.ToString();
            }
            return stringBoardState;
        }
        public int GetLength(int dimension)
        {
            return Grid.GetLength(dimension);
        }
        public void AddStartingTube(int tubeNumber, int[] layers)
        {
            for (int i = 0; i < layers.Length; i++)
            {
                Grid[tubeNumber, i] = new LiquidColor(layers[i]);
            }
        }
        public void AddStartingTube(int tubeNumber, LiquidColorName[] liquids)
        {
            for (int i = 0; i < liquids.Length; i++)
            {
                Grid[tubeNumber, i] = new LiquidColor((int)liquids[i]);
            }
        }
        //private static LiquidColor[,] CloneGrid(LiquidColor[,] grid)
        //{
        //    return CloneGrid(grid, grid.GetLength(0));
        //}
        //[Obsolete] public static LiquidColor[,] CloneGrid(LiquidColor[,] grid, int incrementBy = 0) // Nemazat uplne. Jen prestat pouzivat mimo tuhle classu a pak predelat na private
        //{
        //    LiquidColor[,] gridClone = new LiquidColor[grid.GetLength(0) + incrementBy, grid.GetLength(1)];
        //    for (int x = 0; x < grid.GetLength(0); x++)
        //    {
        //        for (int y = 0; y < grid.GetLength(1); y++)
        //        {
        //            if (grid[x, y] is not null)
        //            {
        //                gridClone[x, y] = grid[x, y].Clone();
        //            }
        //        }
        //    }
        //    return gridClone;
        //}
        public BoardState Clone()
        {
            return new BoardState(this);
        }
        public BoardState FactoryCreate(StoredLevel storedLevel)
        {
            return new BoardState(this, storedLevel);
        }
        public BoardState FactoryCreate(LiquidColor[,] grid)
        {
            return new BoardState(this, grid);
        }
        //public SavedGameState CloneFromSavedGameState(SavedGameState savedGameState)
        //{
        //    var newGrid = GridHelper.CloneGrid(savedGameState.Grid, this.extraTubesCounter);

        //    return newGrid;
        //}
        //public BoardState IncrementTubeNumberBy(int incrementBy)
        //{
        //    return new BoardState(this, incrementBy);
        //}
        public void IncrementTubesBy(int incrementBy)
        {
            IncrementExtraTubesCounter();
            Grid = GridHelper.CloneGrid(Grid, incrementBy);
        }
        /// <summary>
        /// Adding extra (empty) tube during gameplay
        /// </summary>
        public void AddExtraTube()
        {
            if (!CanAddExtraTube())
                return;

            IncrementExtraTubesCounter();
            Grid = GridHelper.CloneGrid(Grid, ExtraTubesCounter);
        }
        public bool CanAddExtraTube()
        {
            return appPreferences != null && ExtraTubesCounter < appPreferences.MaximumExtraTubes;
        }
        public int GetTubeCount() => Grid.GetLength(0);

        private void IncrementExtraTubesCounter(int incrementBy = 1)
        {
            ExtraTubesCounter += incrementBy;
        }
        /// <summary>
        /// Overwrites ExtraTubesCounter and resizes the Grid array
        /// </summary>
        /// <param name="newValue"></param>
        public void SetExtraTubesCounter(int newValue)
        {
            var difference = newValue - extraTubesCounter;
            if (difference < 0)
                throw new ArgumentOutOfRangeException("Only allowed to inrement size of the array");

            ExtraTubesCounter = newValue;

            Grid = GridHelper.CloneGrid(Grid, newValue);
        }
        public void ResetExtraTubesCounter()
        {
            ExtraTubesCounter = 0;
        }
        /// <summary>
        /// Used in StepBack(). Doesn't decrease the size of the grid
        /// </summary>
        /// <param name="boardState"></param>
        public void ReturnBoardState(BoardState boardState)
        {
            var origX = this.Grid == null ? 0 : this.Grid.GetLength(0);
            var newX = boardState.Grid.GetLength(0);

            if (origX > newX)
            {
                Grid = GridHelper.CloneGrid(boardState.Grid, origX - newX);
            }
            else
            {
                Grid = boardState.Clone().Grid;
            }
        }
        /// <summary>
        /// (Used in restart level, LoadLevel, and MakeAMove in AutoSolve)
        /// </summary>
        /// <param name="boardState"></param>
        public void ResetBoardState(BoardState boardState)
        {
            Grid = boardState.Clone().Grid;
            ExtraTubesCounter = boardState.ExtraTubesCounter;
        }
        /// <summary>
        /// Simplified SetBoardState that doesnt change number of extra tubes
        /// </summary>
        /// <param name="grid"></param>
        [Obsolete]public void SetBoardState(LiquidColor[,] grid)
        {
            Grid = grid;
        }
    }
}
