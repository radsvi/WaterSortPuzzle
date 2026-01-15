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
        public const int MinColors = 3;
        public const int MinimumNumberOfExtraTubesAllowedToBeAdded = 0;
        public const int MaximumNumberOfExtraTubesAllowedToBeAdded = 4;
        public const int MaxTubesPerLine = 7;
        public const int MaxStepBack = 5;
        public const int DefaultScoreMultiplier = 25;
        public const int EmptyTubes = 2;

        #region Visual constants
        public const int TubeImageOffset = 30;
        public const int PouringAnimationDuration = 500;
        public const int TubeWidth = 52;
        public const int TubeHeight = 194;
        public const double RippleEffectOffset = -1145;
        public const string InnerBorderElementName = "InnerBorder";
        public const string RippleElementName = "RippleEffectElement";
        public const string InnerGridElementName = "InnerGrid";
        public const double PourEffectHeight = 106;
        public const double CellHeight = 39;
        public const double RippleImageHeight = 30;
        //public const uint RepositionDuration = 250;
        public const uint RaiseTubeDuration = 75;
        //public const uint PouringDuration = 1000;
        //public const uint PouringDuration = 300;
        public const double PouringAngle = 66.0;
        #endregion

        public const string logFolderName = "log";

        //public readonly int MaxTubes = LiquidColor.ColorKeys.Count - 1;
        //public static readonly int MaxTubes = LiquidColor.ColorKeys.Count - 1;
        //public static readonly int ColorCount = Enum.GetNames<LiquidColorName>().Length - 1; // minus one, because Blank isn't a valid color
        public static readonly int ColorCount = 12; // temporary before I fix flask visual scaling

        public static readonly double TubeImageOffsetVisual = TubeImageOffset;
    }
}
