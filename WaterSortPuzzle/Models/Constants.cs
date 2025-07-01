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
        public static readonly int Layers = 4;
        //public readonly int MaxTubes = LiquidColor.ColorKeys.Count - 1;
        public static readonly int MinTubes = 3;
        public static readonly int MaxTubes = LiquidColor.ColorKeys.Count - 1;
    }
}
