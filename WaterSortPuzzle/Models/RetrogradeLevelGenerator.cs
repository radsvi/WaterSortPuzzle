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

            boardState.ReplaceWith(newBoard);

            return selectedColors.Count;
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
    }
}
