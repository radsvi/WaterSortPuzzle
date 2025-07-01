
namespace WaterSortPuzzle.Models
{
    public class LiquidColor
    {
        [JsonProperty] public LiquidColorName Name { get; private set; }
        [JsonProperty] public Color Brush { get; private set; }
        //public static List<LiquidColorNew> ColorKeys { get; } = new List<LiquidColorNew>() {
        //    new LiquidColorNew(LiquidColorName.Blank, new SolidColorBrush(Color.FromRgb(0,0,0))),
        //    new LiquidColorNew(LiquidColorName.Blue, new SolidColorBrush(Color.FromRgb(20,93,239))),
        //    new LiquidColorNew(LiquidColorName.GrayBlue, new SolidColorBrush(Color.FromRgb(63,68,130))),
        //    new LiquidColorNew(LiquidColorName.LightBlue, new SolidColorBrush(Color.FromRgb(136,170,255))),
        //    new LiquidColorNew(LiquidColorName.Orange, new SolidColorBrush(Color.FromRgb(242,121,20))),
        //    new LiquidColorNew(LiquidColorName.Gray, new SolidColorBrush(Color.FromRgb(108,116,144))),
        //    new LiquidColorNew(LiquidColorName.Purple, new SolidColorBrush(Color.FromRgb(191,60,191))),
        //    new LiquidColorNew(LiquidColorName.Yellow, new SolidColorBrush(Color.FromRgb(244,201,22))),
        //    new LiquidColorNew(LiquidColorName.Pink, new SolidColorBrush(Color.FromRgb(255,148,209))),
        //    new LiquidColorNew(LiquidColorName.Green, new SolidColorBrush(Color.FromRgb(0,129,96))),
        //    new LiquidColorNew(LiquidColorName.LightGreen, new SolidColorBrush(Color.FromRgb(179,214,102))),
        //    new LiquidColorNew(LiquidColorName.Olive, new SolidColorBrush(Color.FromRgb(128,153,23))),
        //    new LiquidColorNew(LiquidColorName.Red, new SolidColorBrush(Color.FromRgb(188,36,94))),
        //};
        public static Dictionary<LiquidColorName, LiquidColor> ColorKeys { get; } = new Dictionary<LiquidColorName, LiquidColor>() {
            //{ LiquidColorName.Blank, new LiquidColor(LiquidColorName.Blank, Color.FromArgb("0xFFFFFFFF")) }, // blank mam to tu kvuli typu NullLiquidColorNew. a ted uz i kvuli DrawTubes()
            { LiquidColorName.Blank, new LiquidColor(LiquidColorName.Blank, Color.FromRgb(173,216,230)) }, // blank mam to tu kvuli typu NullLiquidColorNew. a ted uz i kvuli DrawTubes()
            { LiquidColorName.Blue, new LiquidColor(LiquidColorName.Blue, Color.FromRgb(20,93,239)) },
            { LiquidColorName.Indigo, new LiquidColor(LiquidColorName.Indigo, Color.FromRgb(63,68,130)) },
            { LiquidColorName.Turquoise, new LiquidColor(LiquidColorName.Turquoise, Color.FromRgb(136,170,255)) },
            { LiquidColorName.Orange, new LiquidColor(LiquidColorName.Orange, Color.FromRgb(242,121,20)) },
            { LiquidColorName.Gray, new LiquidColor(LiquidColorName.Gray, Color.FromRgb(108,116,144)) },
            { LiquidColorName.Purple, new LiquidColor(LiquidColorName.Purple, Color.FromRgb(191,60,191)) },
            { LiquidColorName.Yellow, new LiquidColor(LiquidColorName.Yellow, Color.FromRgb(244,201,22)) },
            { LiquidColorName.Pink, new LiquidColor(LiquidColorName.Pink, Color.FromRgb(255,148,209)) },
            { LiquidColorName.Green, new LiquidColor(LiquidColorName.Green, Color.FromRgb(0,129,96)) },
            { LiquidColorName.LightGreen, new LiquidColor(LiquidColorName.LightGreen, Color.FromRgb(179,214,102)) },
            { LiquidColorName.Olive, new LiquidColor(LiquidColorName.Olive, Color.FromRgb(128,153,23)) },
            { LiquidColorName.Red, new LiquidColor(LiquidColorName.Red, Color.FromRgb(237,50,41)) },
            { LiquidColorName.Brown, new LiquidColor(LiquidColorName.Brown, Color.FromRgb(114,74,23)) },
            { LiquidColorName.Lime, new LiquidColor(LiquidColorName.Lime, Color.FromRgb(74,219,36)) },
            { LiquidColorName.Scarlet, new LiquidColor(LiquidColorName.Scarlet, Color.FromRgb(188,36,94)) },
        };
        //public static List<LiquidColorNew> CloneColorKeys()
        //{
        //    List<LiquidColorNew> result = new List<LiquidColorNew>();
        //    foreach(var item in ColorKeys)
        //    {
        //        result.Add(item.Clone());
        //    }
        //    return result;
        //}
        private LiquidColor() { } // used for JSON serialization
        public LiquidColor(int colorId)
        {
            var obj = GetColor((LiquidColorName)colorId);
            Name = obj.Name;
            Brush = obj.Brush;
        }
        public LiquidColor(LiquidColorName colorName)
        {
            var obj = GetColor((LiquidColorName)colorName);
            Name = obj.Name;
            Brush = obj.Brush;
        }
        protected LiquidColor(LiquidColorName name, Color brush)
        {
            Name = name;
            Brush = brush;
        }
        //private SolidColorBrush GetColor(LiquidColorNames Name)
        //{
        //    return ColorKeys.Where(key => key.Name == Name).ToList()[0].Brush;
        //}
        private static LiquidColor GetColor(LiquidColorName name)
        {
            //if ((int)name > 11)
            //    return ColorKeys[LiquidColorName.Blue];

            //return ColorKeys.Where(key => key.Name == Name).ToList()[0];
            return ColorKeys[name];
        }
        public LiquidColor Clone()
        {
            return new LiquidColor(this.Name, this.Brush);
        }
        //private static bool OperatorOverload(LiquidColorNew first, LiquidColorNew second)
        //{
        //    //Debug.WriteLine($"first.Source.X [{first.Source.X}] == second.Source.X [{second.Source.X}] && first.Source.Y [{first.Source.Y}] == second.Source.Y [{second.Source.Y}]");
        //    //Debug.WriteLine($"&& first.Target.X [{first.Target.X}] == second.Target.X[{second.Target.X}] && first.Target.Y [{first.Target.Y}] == second.Target.Y [{second.Target.Y}]");
        //    //Debug.WriteLine($"&& first.Liquid.Name [{first.Liquid.Name}] == second.Liquid.Name [{second.Liquid.Name}]");

        //    //Debug.WriteLine($"[{first.Source.X}] == [{second.Source.X}] && [{first.Source.Y}] == [{second.Source.Y}]");
        //    //Debug.WriteLine($"&& [{first.Target.X}] == [{second.Target.X}] && [{first.Target.Y}] == [{second.Target.Y}]");
        //    //Debug.WriteLine($"&& [{first.Liquid.Name}] == [{second.Liquid.Name}]");

        //    //if (first.Source.X == second.Source.X && first.Source.Y == second.Source.Y
        //    //    && first.Target.X == second.Target.X && first.Target.Y == second.Target.Y
        //    //    && first.Liquid.Name == second.Liquid.Name)
        //    //{
        //    //    return true;
        //    //}
        //    //return false;

        //    if (first == null || second == null)
        //    {
        //        if (first == second)
        //            return true;

        //        return false;
        //    }

        //    if (first.Name == second.Name)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        //public static bool operator ==(LiquidColorNew first, LiquidColorNew second)
        //{
        //    return OperatorOverload(first, second);
        //}
        //public static bool operator !=(LiquidColorNew first, LiquidColorNew second)
        //{
        //    return !OperatorOverload(first, second);
        //}
    }
    internal class NullLiquidColorNew : LiquidColor
    {
        public NullLiquidColorNew() : base(LiquidColorName.Blank, Color.FromRgb(0, 0, 0)) { }
    }
}
