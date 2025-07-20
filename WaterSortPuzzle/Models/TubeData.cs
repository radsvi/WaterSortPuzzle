namespace WaterSortPuzzle.Models
{
    public partial class TubeData : ObservableObject
    {
        public int TubeId { get; set; }
        private static int tubeIdCounter = 0;
        public ObservableCollection<LiquidColor?> Layers { get; set; } = new ObservableCollection<LiquidColor?>();
        public TubeData()
        {
            TubeId = tubeIdCounter++;
        }
        public TubeData(int? firstLayer = null, int? secondLayer = null, int? thirdLayer = null, int? fourthLayer = null)
        {
            TubeId = tubeIdCounter++;

            if (firstLayer is not null) Layers.Add(new LiquidColor((int)firstLayer));
            if (secondLayer is not null) Layers.Add(new LiquidColor((int)secondLayer));
            if (thirdLayer is not null) Layers.Add(new LiquidColor((int)thirdLayer));
            if (fourthLayer is not null) Layers.Add(new LiquidColor((int)fourthLayer));
        }
        public TubeData(LiquidColor? firstLayer = null, LiquidColor? secondLayer = null, LiquidColor? thirdLayer = null, LiquidColor? fourthLayer = null)
        {
            TubeId = tubeIdCounter++;

            Layers.Add(CheckColor(firstLayer));
            Layers.Add(CheckColor(secondLayer));
            Layers.Add(CheckColor(thirdLayer));
            Layers.Add(CheckColor(fourthLayer));
        }

        public TubeData(LiquidColor[,] gameGrid, int tubeId)
        {
            TubeId = tubeIdCounter++;

            for (int i = 0; i < Constants.ColorCount; i++)
            {
                Layers.Add(new NullLiquidColor());
            }
            CopyValuesFrom(gameGrid, tubeId);
        }

        public static void ResetCounter()
        {
            tubeIdCounter = 0;
        }
        private static LiquidColor CheckColor(LiquidColor? color = null)
        {
            if (color is not null)
            {
                return color;
            }
            else
            {
                //return LiquidColor.ColorKeys[LiquidColorName.Blank];
                return new LiquidColor(LiquidColorName.Blank);
            }
        }
        public void CopyValuesFrom(LiquidColor[,] gameGrid, int tubeId)
        {
            for (int y = 0; y < Constants.Layers; y++)
            {
                if (gameGrid[tubeId, y] is null)
                    this.Layers[y]?.CopyFrom(LiquidColorName.Blank);
                else
                    this.Layers[y]?.CopyFrom(gameGrid[tubeId, y].Name);
                    //this.Layers[y] = gameGrid[tubeId, y].Clone();
            }
        }
    }
}
