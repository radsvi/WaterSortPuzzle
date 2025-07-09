using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    internal static class LColors
    {
        public static readonly Color Blank = Color.FromRgb(173, 216, 230); // blank mam to tu kvuli typu NullLiquidColorNew. a ted uz i kvuli DrawTubes()
        public static readonly Color Blue = Color.FromRgb(20, 93, 239);
        public static readonly Color Indigo = Color.FromRgb(63, 68, 130);
        public static readonly Color Turquoise = Color.FromRgb(136, 170, 255);
        public static readonly Color Orange = Color.FromRgb(242, 121, 20);
        public static readonly Color Gray = Color.FromRgb(108, 116, 144);
        public static readonly Color Purple = Color.FromRgb(191, 60, 191);
        public static readonly Color Yellow = Color.FromRgb(244, 201, 22);
        public static readonly Color Pink = Color.FromRgb(255, 148, 209);
        public static readonly Color Green = Color.FromRgb(0, 129, 96);
        public static readonly Color LightGreen = Color.FromRgb(179, 214, 102);
        public static readonly Color Olive = Color.FromRgb(128, 153, 23);
        public static readonly Color Red = Color.FromRgb(237, 50, 41);
        public static readonly Color Brown = Color.FromRgb(114, 74, 23);
        public static readonly Color Lime = Color.FromRgb(74, 219, 36);
        public static readonly Color Scarlet = Color.FromRgb(188, 36, 94);

        //private static LiquidColor GetColor(LiquidColorName name)
        //{
        //    switch (name)
        //    {
        //        case LiquidColorName.Blank:

        //            break;

        //        default:
        //            return null;

        //    }
        //}
    }
}
