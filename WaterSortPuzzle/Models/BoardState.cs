using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    public class BoardState
    {
        public string ReadableState
        {
            get
            {
                return BoardStateToString(Grid, StringFormat.Numbers);
            }
        }

        public LiquidColor[,] Grid { get; set; }
        public LiquidColor this[int tubes, int layers]
        {
            get => Grid[tubes, layers];
            set
            {
                Grid[tubes, layers] = value;
                //OnLiquidMoving();
            }
        }
        public EmptyTubes EmptyTubes { get; }





        public BoardState(EmptyTubes emptyTubes)
        {
            EmptyTubes = emptyTubes;
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
        public void AddTube(int tubeNumber, int[] layers)
        {
            for (int i = 0; i < layers.Length; i++)
            {
                Grid[tubeNumber, i] = new LiquidColor(layers[i]);
            }
        }
        public void AddTube(int tubeNumber, LiquidColorName[] liquids)
        {
            for (int i = 0; i < liquids.Length; i++)
            {
                Grid[tubeNumber, i] = new LiquidColor((int)liquids[i]);
            }
        }
        /// <summary>
        /// Adding extra (empty) tube during gameplay
        /// </summary>
        public void AddExtraTube()
        {
            EmptyTubes.IncrementCounter();
            Grid = CloneGrid(Grid, Grid.GetLength(0) + 1);
        }
        public static LiquidColor[,] CloneGrid(LiquidColor[,] grid)
        {
            return CloneGrid(grid, grid.GetLength(0));
        }
        public static LiquidColor[,] CloneGrid(LiquidColor[,] grid, int newNumberOfTubes)
        {
            LiquidColor[,] gridClone = new LiquidColor[newNumberOfTubes, grid.GetLength(1)];
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (grid[x, y] is not null)
                    {
                        gridClone[x, y] = grid[x, y].Clone();
                    }
                }
            }
            return gridClone;
        }
        public int GetTubeCount() => Grid.GetLength(0);

    }
}
