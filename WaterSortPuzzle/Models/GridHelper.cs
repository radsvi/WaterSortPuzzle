using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    public static class GridHelper
    {
        public static LiquidColor[,] CloneGrid(LiquidColor?[,] grid, int incrementBy = 0)
        {
            return CloneGrid_ForceSize(grid, grid.GetLength(0) + incrementBy);
        }
        //[Obsolete]
        public static LiquidColor[,] CloneGrid_ForceSize(LiquidColor?[,] grid, int numberOfTubes) // Nemazat uplne. Jen prestat pouzivat mimo tuhle classu a pak predelat na private (pokud se to rozhodnu presunout zpet do BoardState)
        {
            if (grid == null)
                throw new NullReferenceException($"{nameof(grid)} is null");

            LiquidColor[,] gridClone = new LiquidColor[numberOfTubes, grid.GetLength(1)];
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    var cell = grid[x, y];
                    if (cell is not null)
                    {
                        gridClone[x, y] = cell.Clone();
                    }
                }
            }
            return gridClone;
        }

    }
}
