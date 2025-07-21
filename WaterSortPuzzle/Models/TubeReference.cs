namespace WaterSortPuzzle.Models
{
    public class TubeReference
    {
        private int targetEmptyRow;
        public TubeReference(TubeData tube, TapGestureRecognizer visualElement, int tubeId, Grid gridElement)
        {
            TubeData = tube;
            VisualElement = visualElement;
            TubeId = tubeId;
            GridElement = gridElement;
        }
        public TubeReference(int tubeId, LiquidColor lastColorMoved, int targetEmptyRow, int numberOfRepeatingLiquids)
        {
            TubeId = tubeId;
            LastColorMoved = lastColorMoved;
            TargetEmptyRow = targetEmptyRow;
            NumberOfRepeatingLiquids = numberOfRepeatingLiquids;
        }

        //public List<object> Contents { get; set; } = new List<object>();
        //public Tube Tube { get; set; }
        public TapGestureRecognizer VisualElement { get; private set; }
        public int TubeId { get; private set; }
        public TubeData TubeData { get; private set; }
        public Grid GridElement { get; private set; }
        public LiquidColor TopMostLiquid {  get; set; }
        public LiquidColor LastColorMoved { get; set; }
        public int TargetEmptyRow
        {
            get => targetEmptyRow;
            set
            {
                if (value != targetEmptyRow)
                {
                    //if (targetEmptyRow <= Constants.Layers)
                    //    targetEmptyRow = value;
                    //else
                    //    targetEmptyRow = Constants.Layers;

                    targetEmptyRow = value;
                }
            }
        }
        private int numberOfRepeatingLiquids;
        public int NumberOfRepeatingLiquids
        {
            get => numberOfRepeatingLiquids;
            set
            {
                if (value != numberOfRepeatingLiquids)
                {
                    //if (TargetEmptyRow + value <= Constants.Layers)
                    //    numberOfRepeatingLiquids = value;
                    //else
                    //    numberOfRepeatingLiquids = Constants.Layers - TargetEmptyRow;

                    numberOfRepeatingLiquids = value;
                }
            }
        }
        //public static bool operator ==(TubeReference first, TubeReference second)
        //{
        //    return IsMatching(first, second);
        //}
        //private static bool IsMatching(TubeReference first, TubeReference second)
        //{
        //    first.TubeType.
        //}
    }
}
