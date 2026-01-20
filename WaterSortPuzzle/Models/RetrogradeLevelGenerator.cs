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
                //int colorNumber = selectedColors[rnd.Next(0, selectedColors.Count)];
                // return to random numbers:
                int colorNumber = selectedColors[selectedColors.Count - 1]; // non-random
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
                selectedColors.Remove(selectedColors[^1]); // this always keeps the same colors
                // return to random numbers:
                //selectedColors.Remove(selectedColors[rnd.Next(0, selectedColors.Count)]);
            }

            return selectedColors;
        }
        private void ScrambleBoardState(BoardState boardState)
        {
            for (int i = 0; i < 3; i++)
            {
                (int maxAdjacentEmptySpots, int emptiestTube) = FindMaximumAdjacentEmptySlots(boardState);
                (LiquidColor currentColor, int pickedColors) = PickMaximumAdjacentColors(boardState, maxAdjacentEmptySpots);
                DepositPickedColors(boardState, maxAdjacentEmptySpots, emptiestTube, currentColor, pickedColors); 
            }
        }

        private static void DepositPickedColors(BoardState boardState, int maxAdjacentEmptySpots, int emptiestTube, LiquidColor currentColor, int numberOfPickedColors)
        {
            var firstXcoord = boardState.Grid.GetLength(1) - maxAdjacentEmptySpots;
            for (int y = firstXcoord; y < firstXcoord + numberOfPickedColors; y++)
            {
                boardState.Grid[emptiestTube, y] = currentColor;
            }
        }
        private static (int, int) FindMaximumAdjacentEmptySlots(BoardState boardState)
        {
            int highestAdjacentEmptySpots = 1;
            int? emptiestTube = null;
            for (int x = 0; x < boardState.Grid.GetLength(0); x++)
            {
                int adjacentEmptySpots = 0;
                for (int y = boardState.Grid.GetLength(1) - 1; y >= 0; y--)
                {
                    if (boardState.Grid[x, y] != null)
                        break;

                    adjacentEmptySpots++;

                    if (adjacentEmptySpots > highestAdjacentEmptySpots)
                    {
                        emptiestTube = x;
                        highestAdjacentEmptySpots = adjacentEmptySpots;
                    }

                    if (highestAdjacentEmptySpots >= boardState.Grid.GetLength(1))
                        break;
                }

                if (highestAdjacentEmptySpots >= boardState.Grid.GetLength(1))
                    break;
            }

            if (emptiestTube == null)
                throw new NullReferenceException($"{nameof(emptiestTube)} is null");

            return (highestAdjacentEmptySpots, (int)emptiestTube);
        }
        /// <summary>
        /// Picks either maximum of 3 colors, or until it reaches liquid of different color
        /// </summary>
        /// <param name="boardState"></param>
        /// <param name="x"></param>
        /// <param name="currentColor"></param>
        /// <returns></returns>
        private static (LiquidColor, int) PickMaximumAdjacentColors(BoardState boardState, int numberOfEmptySlots)
        {
            LiquidColor? selectedColor = null;
            int highestNumberOfPickedColors = 1;
            int? selectedTube = null;
            for (int x = 0; x < boardState.Grid.GetLength(0); x++)
            {
                int pickedColors = 1;
                LiquidColor? currentColor = null;
                for (int y = numberOfEmptySlots - 1; y >= 1; y--) // max 3 colors picked
                {
                    var cell = boardState.Grid[x, y];
                    if (cell == null)
                    {
                        break;
                    }

                    if (currentColor is null)
                    {
                        currentColor = cell;
                    }
                    else if (currentColor == cell)
                    {
                        pickedColors++;
                    }
                }
                if (currentColor is not null && (selectedTube is null || highestNumberOfPickedColors > pickedColors))
                {
                    selectedTube = x;
                    selectedColor = currentColor.Clone();
                    highestNumberOfPickedColors = pickedColors;
                }
                if (highestNumberOfPickedColors >= numberOfEmptySlots)
                    break;
            }

            if (selectedTube == null)
                throw new NullReferenceException($"{nameof(selectedTube)} is null");
            // RemovePickedColors
            for (int y = boardState.Grid.GetLength(1) - highestNumberOfPickedColors; y < boardState.Grid.GetLength(1); y++)
            {
                boardState.Grid[(int)selectedTube, y] = null;
            }

            if (selectedColor == null)
                throw new NullReferenceException($"{nameof(selectedColor)} is null");

            return ((LiquidColor)selectedColor, highestNumberOfPickedColors);
        }
    }
}
