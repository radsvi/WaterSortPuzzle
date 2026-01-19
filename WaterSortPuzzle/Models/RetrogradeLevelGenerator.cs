using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    public class RetrogradeLevelGenerator
    {
        private readonly BoardState boardState;
        /// <summary>
        /// Generates level backwards from solved solution to guarantee the level is always solvable
        /// </summary>
        public RetrogradeLevelGenerator(BoardState boardState)
        {
            this.boardState = boardState;
        }

        public int GenerateStandardLevel_Retrograde(int numberOfColorsToGenerate)
        {
            Random rnd = new Random();

            List<int> selectedColors = GenerateListOfRandomColors(numberOfColorsToGenerate, rnd);
            BoardState newBoard = GenerateSolvedBoard(numberOfColorsToGenerate, rnd, selectedColors);
            ScrambleBoardState(newBoard);

            boardState.ReplaceWith(newBoard);

            return selectedColors.Count;
        }

        private BoardState GenerateSolvedBoard(int numberOfColorsToGenerate, Random rnd, List<int> selectedColors)
        {
            var newBoard = boardState.FactoryCreate(new LiquidColor[numberOfColorsToGenerate + Constants.EmptyTubesAtTheStart, Constants.Layers]);

            // generate solved state:
            for (int x = 0; x < numberOfColorsToGenerate; x++)
            {
                int colorNumber = selectedColors[rnd.Next(0, selectedColors.Count)];
                for (int y = 0; y < Constants.Layers; y++)
                {
                    //newBoard.Grid[x, y] = selectedColors[colorNumber];
                    newBoard.Grid[x, y] = new LiquidColor(colorNumber);
                }
                selectedColors.Remove(colorNumber);
            }

            return newBoard;
        }
        private static List<int> GenerateListOfRandomColors(int numberOfColorsToGenerate, Random rnd)
        {
            List<int> selectedColors = [];
            for (int i = 0; i < Constants.ColorCount; i++) // generate list of all colors.
            {
                selectedColors.Add(i);
            }

            for (int i = 0; i < Constants.ColorCount - numberOfColorsToGenerate; i++) // now remove some random colors. 
            {
                //selectedColors.Remove(selectedColors[selectedColors.Count]); // this always keeps the same colors
                selectedColors.Remove(selectedColors[rnd.Next(0, selectedColors.Count)]);
            }

            return selectedColors;
        }
        private void ScrambleBoardState(BoardState boardState)
        {
            (int maxAdjacentEmptySpots, int emptiestTube) = FindMaximumAdjacentEmptySlots(boardState);

            for (int x = 0; x < boardState.Grid.GetLength(0); x++)
            {
                (LiquidColor currentColor, int pickedColors) = PickAdjacentColors(boardState, x, maxAdjacentEmptySpots);
                DepositPickedColors(boardState, maxAdjacentEmptySpots, emptiestTube, currentColor, pickedColors);

#warning smazat:
                break; // smazat
            }
        }

        private static void DepositPickedColors(BoardState boardState, int maxAdjacentEmptySpots, int emptiestTube, LiquidColor currentColor, int pickedColors)
        {
            var firstXcoord = boardState.Grid.GetLength(1) - maxAdjacentEmptySpots;
            for (int y = firstXcoord; y < firstXcoord + pickedColors; y++)
            {
                boardState.Grid[emptiestTube, y] = currentColor;
            }
        }

        private static (int, int) FindMaximumAdjacentEmptySlots(BoardState boardState)
        {
            int maxAdjacentEmptySpots = 1;
            int? emptiestTube = null;
            for (int x = 0; x < boardState.Grid.GetLength(0); x++)
            {
                int adjacentEmptySpots = 1;
                for (int y = boardState.Grid.GetLength(1) - 1; y >= 0; y--)
                {
                    if (boardState.Grid[x, y] == null)
                    {
                        emptiestTube = x;
                        adjacentEmptySpots++;
                    }

                    if (adjacentEmptySpots > maxAdjacentEmptySpots)
                        maxAdjacentEmptySpots = adjacentEmptySpots;

                    if (maxAdjacentEmptySpots >= boardState.Grid.GetLength(1))
                        break;
                }

                if (maxAdjacentEmptySpots >= boardState.Grid.GetLength(1))
                    break;
            }

            if (emptiestTube == null)
                throw new NullReferenceException($"{nameof(emptiestTube)} is null");

            return (maxAdjacentEmptySpots, (int)emptiestTube);
        }

        /// <summary>
        /// Picks either maximum of 3 colors, or until it reaches liquid of different color
        /// </summary>
        /// <param name="boardState"></param>
        /// <param name="x"></param>
        /// <param name="currentColor"></param>
        /// <returns></returns>
        private static (LiquidColor, int) PickAdjacentColors(BoardState boardState, int x, int maxColors)
        {
            LiquidColor? currentColor = null;
            int pickedColors = 1;
            for (int y = maxColors - 1; y >= 1; y--) // max 3 colors picked
            {
                var cell = boardState.Grid[x, y];
                if (currentColor is null)
                {
                    currentColor = cell;
                    boardState.Grid[x, y] = null;
                }
                else if (cell is not null && currentColor == cell)
                {
                    pickedColors++;
                    boardState.Grid[x, y] = null;
                }
                else
                    break;
            }
            if (currentColor == null)
                throw new NullReferenceException($"{nameof(currentColor)} is null");

            return ((LiquidColor)currentColor, pickedColors);
        }
    }
}
