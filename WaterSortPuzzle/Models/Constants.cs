using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public static class Constants
    {
        public const int MaximumExtraTubesUpperLimit = 20;
        public const int Layers = 4;
        public const int MinColors = 2;
        public const int MinimumNumberOfExtraTubesAllowedToBeAdded = 0;
        public const int MaximumNumberOfExtraTubesAllowedToBeAdded = 4;
        public const int MaxTubesPerLine = 7;
        public const int MaxStepBack = 5;

        //public readonly int MaxTubes = LiquidColor.ColorKeys.Count - 1;
        //public static readonly int MaxTubes = LiquidColor.ColorKeys.Count - 1;
        public static readonly int MaxColors = LiquidColor.ColorKeys.Count - 1;

    }
}
