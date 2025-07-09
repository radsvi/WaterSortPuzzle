﻿namespace WaterSortPuzzle.Models
{
    /// <summary>
    /// nejnizsi layer je prvni
    /// </summary>
    //[Obsolete]
    //internal class Tube : ViewModelBase
    //{
    //    public int Id { get; set; }
    //    private static int tubeIdCounter = 0;
    //    public Button ButtonElement { get; set; }
    //    public Grid GridElement { get; set; }

    //    private ObservableCollection<LiquidColor> layers = new ObservableCollection<LiquidColor>();
    //    public ObservableCollection<LiquidColor> Layers
    //    {
    //        get { return layers; }
    //        set
    //        {
    //            if (layers != value)
    //            {
    //                layers = value;
    //                //OnPropertyChanged();
    //            }
    //        }
    //    }

    //    //private string margin;
    //    //public string Margin
    //    //{
    //    //    get {
    //    //        if (selected == true)
    //    //            return "0,0,0,30";
    //    //        else
    //    //            return "0,30,0,0";
    //    //    }
    //    //}
    //    private bool selected = false;
    //    public bool Selected
    //    {
    //        get { return selected; }
    //        set
    //        {
    //            selected = value;
    //            //OnPropertyChanged(); // Tube_PropertyChanged se provadi 2x kdyz jsem to tu mel
    //            OnPropertyChanged("Margin");
    //        }
    //    }
    //    public static void ResetCounter()
    //    {
    //        tubeIdCounter = 0;
    //    }
    //    public Tube DeepCopy()
    //    {
    //        Tube clone = (Tube)MemberwiseClone();
    //        clone.Layers = new ObservableCollection<LiquidColor>();
    //        for (int i = 0; i < this.Layers.Count; i++)
    //        {
    //            clone.Layers.Add(new LiquidColor((int)this.Layers[i].Id));
    //        }
    //        return clone;
    //    }
    //    public Tube()
    //    {
    //        Id = tubeIdCounter++;
    //    }
    //    public Tube(int? firstLayer = null, int? secondLayer = null, int? thirdLayer = null, int? fourthLayer = null)
    //    {
    //        Id = tubeIdCounter++;

    //        if (firstLayer is not null) Layers.Add(new LiquidColor((int)firstLayer));
    //        if (secondLayer is not null) Layers.Add(new LiquidColor((int)secondLayer));
    //        if (thirdLayer is not null) Layers.Add(new LiquidColor((int)thirdLayer));
    //        if (fourthLayer is not null) Layers.Add(new LiquidColor((int)fourthLayer));
    //    }
    //    public Tube(LiquidColor? firstLayer = null, LiquidColor? secondLayer = null, LiquidColor? thirdLayer = null, LiquidColor? fourthLayer = null)
    //    {
    //        Id = tubeIdCounter++;

    //        if (firstLayer is not null) Layers.Add(firstLayer);
    //        if (secondLayer is not null) Layers.Add(secondLayer);
    //        if (thirdLayer is not null) Layers.Add(thirdLayer);
    //        if (fourthLayer is not null) Layers.Add(fourthLayer);
    //    }
    //    //private void Tube_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    //    //{
    //    //    Debug.WriteLine("asdf");
    //    //}
    //}

    public partial class TubeData : ObservableObject
    {
        public int TubeId { get; set; }
        private static int tubeIdCounter = 0;

        public ObservableCollection<LiquidColor?> Layers { get; set; } = new ObservableCollection<LiquidColor?>();
        //private List<LiquidColor> layers = new List<LiquidColor>();
        //public List<LiquidColor> Layers
        //{
        //    get { return layers; }
        //    set
        //    {
        //        if (layers != value)
        //        {
        //            layers = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        //public TubeNew DeepCopy()
        //{
        //    TubeNew clone = (TubeNew)MemberwiseClone();
        //    clone.Layers = new List<LiquidColorNew>();
        //    for (int i = 0; i < this.Layers.Count; i++)
        //    {
        //        clone.Layers.Add(new LiquidColorNew((int)this.Layers[i].Id));
        //    }
        //    return clone;
        //}
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
