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
        public const int MaxTubesPerLine = 9;
        public const int MaxStepBack = 5;
        public const int TubeImageOffset = 30;

        public const string logFolderName = "log";

        //public readonly int MaxTubes = LiquidColor.ColorKeys.Count - 1;
        //public static readonly int MaxTubes = LiquidColor.ColorKeys.Count - 1;
        public static readonly int ColorCount = Enum.GetNames(typeof(LiquidColorName)).Length - 1; // minus one, because Blank isn't a valid color

        public static readonly double TubeImageOffsetVisual = TubeImageOffset;
    }
}
