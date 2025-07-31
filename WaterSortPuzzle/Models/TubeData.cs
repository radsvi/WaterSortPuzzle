namespace WaterSortPuzzle.Models
{
    public partial class TubeData : ObservableObject
    {
        private static int tubeIdCounter = 0;
        public int TubeId { get; set; }
        public static int TubesPerLine { get; set; }
        public bool IsBusy { get; set; } = false;
        private bool isVisible = true;
        public bool IsVisible
        {
            get => isVisible;
            set { isVisible = value; OnPropertyChanged(); }
        }
        private bool isRaised;
        public bool IsRaised
        {
            get => isRaised;
            set { isRaised = value; OnPropertyChanged(); }
        }
        private bool triggerRippleEffect;
        public bool TriggerRippleEffect
        {
            get => triggerRippleEffect;
            set { triggerRippleEffect = value; OnPropertyChanged(); }
        }
        private bool displayPourEffect = false;
        public bool DisplayPourEffect
        {
            get => displayPourEffect;
            //set { SetProperty(ref displayPourEffect, value); OnPropertyChanged(); }
            set { displayPourEffect = value; OnPropertyChanged(); }
        }
        public double PourEffectHeight
        {
            get
            {
                //if (RippleGridRow < Constants.Layers - 1)
                //{
                //    return (Constants.PourEffectHeight + (RippleGridRow - Constants.Layers + 1) * Constants.CellHeight);
                //}

                //return Constants.PourEffectHeight;
                //return (Constants.PourEffectHeight + (RippleGridRow - Constants.Layers + 1) * Constants.CellHeight);
                //return (Constants.PourEffectHeight + (RippleGridRow - Constants.Layers + 1) * Constants.CellHeight) + (RippleGridRow - 1) * Constants.CellHeight;
                //return (Constants.PourEffectHeight + (RippleGridRow - Constants.Layers + 1 + RippleGridRowSpan - 1) * Constants.CellHeight);

                var bottomCell = RippleGridRow + RippleGridRowSpan - 1;
                var height = (Constants.PourEffectHeight + (bottomCell) * Constants.CellHeight);
                return height;
            }
        }
        private TubeData targetTube;
        public TubeData TargetTube
        {
            get => targetTube;
            set { targetTube = value; OnPropertyChanged(); }
        }
        private AnimationType animate;
        public AnimationType Animate
        {
            get => animate;
            set { animate = value; OnPropertyChanged(); }
        }
        private bool triggerReposition;
        public bool TriggerReposition
        {
            get => triggerReposition;
            set { triggerReposition = value; OnPropertyChanged(); }
        }
        private Rect coordinates = new Rect(0, 10, 46, 194);
        public Rect Coordinates
        {
            get => coordinates;
            set { coordinates = value; OnPropertyChanged(); }
        }
        private double translateX;
        public double TranslateX
        {
            get => translateX;
            set { translateX = value; OnPropertyChanged(); }
        }
        private double translateY;
        public double TranslateY
        {
            get => translateY;
            set { translateY = value; OnPropertyChanged(); }
        }
        private double actualWidth;
        public double ActualWidth
        {
            get => actualWidth;
            set { actualWidth = value; OnPropertyChanged(); }
        }
        private double actualHeight;
        public double ActualHeight
        {
            get => actualHeight;
            set { actualHeight = value; OnPropertyChanged(); }
        }
        //private bool rippleEffectVisible = false;
        //public bool RippleGridVisible
        //{
        //    get => rippleEffectVisible;
        //    set { rippleEffectVisible = value; OnPropertyChanged(); }
        //}
        private Color pourBackgroundColor = Colors.Transparent;
        public Color PourBackgroundColor
        {
            get => pourBackgroundColor;
            set { pourBackgroundColor = value; OnPropertyChanged(); }
        }
        private int rippleGridRow = 0;
        public int RippleGridRow
        {
            get => rippleGridRow;
            set { rippleGridRow = value; OnPropertyChanged(); OnPropertyChanged(nameof(PourEffectHeight)); }
        }
        private int rippleGridRowSpan = 1;
        public int RippleGridRowSpan
        {
            get => rippleGridRowSpan;
            set { rippleGridRowSpan = value; OnPropertyChanged(); OnPropertyChanged(nameof(PourEffectHeight)); }
        }
        private int numberOfRepeatingLiquids;
        public int NumberOfRepeatingLiquids
        {
            get => numberOfRepeatingLiquids;
            set { numberOfRepeatingLiquids = value; OnPropertyChanged(); }
        }

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

            RecalculateTubesPerLine(gameGrid);

            
            Point position = CalculateTubePosition();

            Coordinates = new Rect(
                position.X,
                position.Y,
                Constants.TubeWidth,
                Constants.TubeHeight
            );
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
        private static void RecalculateTubesPerLine(LiquidColor[,] gameGrid)
        {
            int tubeCount = gameGrid.GetLength(0);

            if (tubeCount > Constants.MaxTubesPerLine * 2)
            {
                TubesPerLine = (int)Math.Ceiling((decimal)tubeCount / 3);
            }
            else
            {
                TubesPerLine = (int)Math.Ceiling((decimal)tubeCount / 2);
            }
        }
        private Point CalculateTubePosition()
        {
            double xPos;
            double yPos;

            xPos = (TubeId % TubesPerLine) * (Constants.TubeWidth + 6);
            yPos = (int)(TubeId / TubesPerLine) * Constants.TubeHeight;

            return new Point(xPos, yPos);
        }
    }
}
