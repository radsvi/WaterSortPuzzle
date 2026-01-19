
using System.Collections.Specialized;
using ColN = WaterSortPuzzle.Enums.LiquidColorName; // creating alias so that I dont have to have long names in GenerateDebugLevel();

namespace WaterSortPuzzle.Models
{
    public partial class GameState : ViewModelBase
    {
        readonly AppPreferences appPreferences;
        readonly Notification notification;
        readonly Leveling leveling;
        public BoardState BoardState { get; }

        public GameState() { }
        public GameState(AppPreferences appPreferences, Notification notification, Leveling leveling, BoardState boardState)
        {
            this.appPreferences = appPreferences;
            this.notification = notification;
            this.leveling = leveling;
            BoardState = boardState;
        }

        public bool SolvedAtLeastOnce { get; set; } = false;

        private int stepBackPressesCounter;
        public int StepBackCounter
        {
            get { return stepBackPressesCounter; }
            set
            {
                if (value != stepBackPressesCounter)
                {
                    stepBackPressesCounter = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(StepBackDisplay));
                }
            }
        }
        public int StepBackDisplay
        {
            get
            {
                if (appPreferences.UnlimitedStepBack)
                {
                    return SavedGameStates.Count - 1;
                }
                else
                {
                    return Constants.MaxStepBack - StepBackCounter;
                }
            }
        }
        
        //[ObservableProperty]
        //[NotifyCanExecuteChangedFor(nameof(mainVM.AddExtraTubeCommand))]
        //private int colorCount = 0;
        private int colorsCounter;
        public int ColorsCounter
        {
            get { return colorsCounter; }
            private set
            {
                if (value != colorsCounter)
                {
                    colorsCounter = value;
                    OnPropertyChanged();
                    //OnPropertyChanged(nameof(mainVM.AddExtraTubeCommand));
                    //mainVM.AddExtraTubeCommand.NotifyCanExecuteChanged();
                }
            }
        }


        //[ObservableProperty]
        //[NotifyCanExecuteChangedFor(nameof(StepBackCommand), nameof(StepBackDisplay))]

        //private ObservableCollection<SavedGameState> savedGameStates = new ObservableCollection<SavedGameState>();
        /// <summary>
        /// Starting board position right after generating new level
        /// </summary>
        public ObservableCollection<SavedGameState> SavedGameStates { get; } = [];
        //public ObservableCollection<SavedGameState> SavedGameStates
        //{
        //    get { return savedGameStates; }
        //    private set
        //    {
        //        if (value != savedGameStates)
        //        {
        //            savedGameStates = value;
        //            OnPropertyChanged();
        //            //OnPropertyChanged(nameof(StepBackCommand));
        //            //StepBackCommand.NotifyCanExecuteChanged();
        //            //OnPropertyChanged(nameof(StepBackDisplay));
        //        }
        //    }
        //}

        //private void SetGameGrid(int numberOfTubes)
        //{
        //    //gameGrid = new LiquidColorNew[(NumberOfTubes + ExtraTubesAdded + 2), NumberOfLayers];
        //    gameGrid = new LiquidColorNew[numberOfTubes, NumberOfLayers];
        //}
        //private void OnLiquidMoving()
        //{
        //    MainPage.DrawTubes();
        //}

        public void FillBoard()
        {
            if (appPreferences.SavedGameStatesBeforeSleepV2 is not null && appPreferences.SavedGameStatesBeforeSleepV2.Count > 0)
                LoadLastLevel();
            else
                GenerateNewLevel();
        }
        public void GenerateNewLevel()
        {
            BoardState.ResetExtraTubesCounter();
            leveling.IncreaseLevel();
            if (appPreferences.SingleLevelMode == false)
            {
                GenerateStandardLevel(leveling.NumberOfColorsToGenerate);
            }
            else if (appPreferences.LoadDebugLevel == true)
            {
                GenerateDebugLevel();
            }
            else
            {
                if (appPreferences.RandomNumberOfTubes)
                {
                    GenerateStandardLevel(new Random().Next(Constants.MinColors, Constants.ColorCount - 1));
                }
                else
                {
                    GenerateStandardLevel(appPreferences.NumberOfColorsToGenerate);
                }
            }

            StoreStartingGrid();
            SolvedAtLeastOnce = false;
        }
        private void GenerateDebugLevel()
        {
            
            //Tube.ResetCounter();
            //Tubes?.Clear();
            
            int i = 0;
            BoardState.Grid = new LiquidColor[Constants.ColorCount + 10, Constants.Layers];

            // Almost solved:
            BoardState.AddStartingTube(i++, new int[] { });
            BoardState.AddStartingTube(i++, new int[] { 1, 1, 1, 3 });
            BoardState.AddStartingTube(i++, new int[] { 2, 3, 1 });
            BoardState.AddStartingTube(i++, new int[] { 2, 2, 2 });
            BoardState.AddStartingTube(i++, new int[] { 3, 3 });
            BoardState.AddStartingTube(i++, new int[] { 4, 4, 4, 4 });



            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Yellow, LiquidColorName.Green, LiquidColorName.Purple, LiquidColorName.LightBlue });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Pink, LiquidColorName.Red, LiquidColorName.Blue, LiquidColorName.LightBlue });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Purple, LiquidColorName.Olive, LiquidColorName.Orange, LiquidColorName.Orange });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Blue, LiquidColorName.Purple, LiquidColorName.GrayBlue, LiquidColorName.Yellow });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.GrayBlue, LiquidColorName.LightBlue, LiquidColorName.Yellow, LiquidColorName.GrayBlue });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.LightGreen, LiquidColorName.Blue, LiquidColorName.Olive, LiquidColorName.LightGreen });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Green, LiquidColorName.LightGreen, LiquidColorName.Olive, LiquidColorName.Pink });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.LightBlue, LiquidColorName.Gray, LiquidColorName.Pink, LiquidColorName.Red });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Red, LiquidColorName.Gray, LiquidColorName.Gray, LiquidColorName.Green });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Yellow, LiquidColorName.Green, LiquidColorName.Olive, LiquidColorName.LightGreen });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Blue, LiquidColorName.Gray, LiquidColorName.Pink, LiquidColorName.Red });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.GrayBlue, LiquidColorName.Orange, LiquidColorName.Purple, LiquidColorName.Orange });

            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Yellow, LiquidColorName.Green, LiquidColorName.Purple });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Pink, LiquidColorName.Red, LiquidColorName.Red });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Purple, LiquidColorName.Olive, LiquidColorName.Orange, LiquidColorName.Orange });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Blue, LiquidColorName.Purple, LiquidColorName.GrayBlue, LiquidColorName.Yellow });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.GrayBlue, LiquidColorName.LightBlue, LiquidColorName.Yellow, LiquidColorName.GrayBlue });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.LightGreen, LiquidColorName.Blue, LiquidColorName.Olive, LiquidColorName.LightGreen });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Green, LiquidColorName.LightGreen, LiquidColorName.Olive });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.LightBlue, LiquidColorName.Gray, LiquidColorName.Pink, LiquidColorName.Pink });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Red, LiquidColorName.Gray, LiquidColorName.Gray, LiquidColorName.Green });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Yellow, LiquidColorName.Green, LiquidColorName.Olive, LiquidColorName.LightGreen });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Blue, LiquidColorName.Gray, LiquidColorName.Pink, LiquidColorName.Red });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.GrayBlue, LiquidColorName.Orange, LiquidColorName.Purple, LiquidColorName.Orange });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.LightBlue, LiquidColorName.LightBlue });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Blue });


            //// Nikdy nevyresenej level (stary cislovani):
            //AddTube(i++, new int[] { 9, 2, 4, 1 });
            //AddTube(i++, new int[] { 3, 8, 11, 5 });
            //AddTube(i++, new int[] { 9, 11, 11, 12 });
            //AddTube(i++, new int[] { 3, 3, 2, 5 });
            //AddTube(i++, new int[] { 1, 7, 6, 10 });
            //AddTube(i++, new int[] { 3, 4, 7, 4 });
            //AddTube(i++, new int[] { 2, 8, 5, 10 });
            //AddTube(i++, new int[] { 6, 1, 2, 9 });
            //AddTube(i++, new int[] { 11, 10, 7, 6 });
            //AddTube(i++, new int[] { 5, 7, 10, 4 });
            //AddTube(i++, new int[] { 8, 12, 6, 12 });
            //AddTube(i++, new int[] { 1, 12, 8, 9 });

            //// Ten starej "nikdy" nevyresenej level (vyresenej za 1772 steps to generate, 46 steps to solve, 17.68 sec):
            //AddTube(i++, new int[] { 8, 1, 3, 0 });
            //AddTube(i++, new int[] { 2, 7, 10, 4 });
            //AddTube(i++, new int[] { 8, 10, 10, 11 });
            //AddTube(i++, new int[] { 2, 2, 1, 4 });
            //AddTube(i++, new int[] { 0, 6, 5, 9 });
            //AddTube(i++, new int[] { 2, 3, 6, 3 });
            //AddTube(i++, new int[] { 1, 7, 4, 9 });
            //AddTube(i++, new int[] { 5, 0, 1, 8 });
            //AddTube(i++, new int[] { 10, 9, 6, 5 });
            //AddTube(i++, new int[] { 4, 6, 9, 3 });
            //AddTube(i++, new int[] { 7, 11, 5, 11 });
            //AddTube(i++, new int[] { 0, 11, 7, 8 });

            //// Ten starej "nikdy" nevyresenej level (testuju napul vyreseny:
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Green, LiquidColorName.Indigo, LiquidColorName.Orange, LiquidColorName.Orange });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Turquoise, LiquidColorName.Pink });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Green, LiquidColorName.Green, LiquidColorName.Green });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Turquoise, LiquidColorName.Turquoise, LiquidColorName.Indigo, LiquidColorName.Indigo });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Lime, LiquidColorName.Lime, LiquidColorName.Lime, LiquidColorName.Lime});
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Turquoise, LiquidColorName.Orange, LiquidColorName.Yellow, LiquidColorName.Orange });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Indigo, LiquidColorName.Pink, LiquidColorName.Pink, LiquidColorName.Pink });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Red, LiquidColorName.Red, LiquidColorName.Red, LiquidColorName.Red });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Olive, LiquidColorName.Olive, LiquidColorName.Olive, LiquidColorName.Olive });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Gray, LiquidColorName.Yellow, LiquidColorName.Yellow, LiquidColorName.Yellow });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Gray, LiquidColorName.Gray, LiquidColorName.Gray });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Blue, LiquidColorName.Blue, LiquidColorName.Blue, LiquidColorName.Blue });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Purple, LiquidColorName.Purple, LiquidColorName.Purple, LiquidColorName.Purple });


            //// sejvnutej level z te novejsi hry. Level jsem vyresil, ale byl tezkej (jeste jednou jsem to overil ze je to resitelny. Uspesne!):
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Pink, LiquidColorName.Red, LiquidColorName.Blue, LiquidColorName.Orange });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Purple, LiquidColorName.Indigo, LiquidColorName.Turquoise, LiquidColorName.Lime });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Pink, LiquidColorName.Turquoise, LiquidColorName.Turquoise, LiquidColorName.Gray });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Purple, LiquidColorName.Purple, LiquidColorName.Red, LiquidColorName.Lime });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Orange, LiquidColorName.Yellow, LiquidColorName.Olive, LiquidColorName.Green });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Purple, LiquidColorName.Blue, LiquidColorName.Yellow, LiquidColorName.Blue });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Red, LiquidColorName.Indigo, LiquidColorName.Lime, LiquidColorName.Green });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Olive, LiquidColorName.Orange, LiquidColorName.Red, LiquidColorName.Pink });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Turquoise, LiquidColorName.Green, LiquidColorName.Yellow, LiquidColorName.Olive });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Lime, LiquidColorName.Yellow, LiquidColorName.Green, LiquidColorName.Blue });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Indigo, LiquidColorName.Gray, LiquidColorName.Olive, LiquidColorName.Gray });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Orange, LiquidColorName.Gray, LiquidColorName.Indigo, LiquidColorName.Pink });

            //AddTube(i++, new int[] { 1, 1, 1, 7 });
            //AddTube(i++, new int[] { 2, 6, 7, 4 });
            //AddTube(i++, new int[] { 3, 1, 11, 5 });
            //AddTube(i++, new int[] { 5, 0, 4, 3 });
            //AddTube(i++, new int[] { 5, 11, 6, 6 });
            //AddTube(i++, new int[] { 8, 2, 1, 7 });
            //AddTube(i++, new int[] { 8, 3, 5, 3 });
            //AddTube(i++, new int[] { 8, 1, 4, 9 });
            //AddTube(i++, new int[] { 9, 0, 8, 2 });
            //AddTube(i++, new int[] { 9, 11, 6, 4 });
            //AddTube(i++, new int[] { 1, 0, 7, 1 });
            //AddTube(i++, new int[] { 11, 0, 2, 9 });


            //AddTube(i++, new int[] { 1,1,1 });

            //// tenhle level jsem nevyresil manualne, tim bruteforce exponencialnim scriptem to trvalo 7 sekund a 685 kroku:
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Red, LiquidColorName.Red, LiquidColorName.Blue, LiquidColorName.Orange });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Turquoise, LiquidColorName.Turquoise, LiquidColorName.Lime, LiquidColorName.Purple });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Scarlet, LiquidColorName.Orange, LiquidColorName.Indigo, LiquidColorName.Indigo });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Indigo, LiquidColorName.Orange, LiquidColorName.Green, LiquidColorName.Gray });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Brown, LiquidColorName.Blue, LiquidColorName.Purple, LiquidColorName.Gray });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Turquoise, LiquidColorName.Scarlet, LiquidColorName.Indigo, LiquidColorName.Lime });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Gray, LiquidColorName.Scarlet, LiquidColorName.Yellow, LiquidColorName.Red });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Scarlet, LiquidColorName.Blue, LiquidColorName.Brown, LiquidColorName.Orange });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Blue, LiquidColorName.Yellow, LiquidColorName.Green, LiquidColorName.Lime });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Gray, LiquidColorName.Yellow, LiquidColorName.Turquoise, LiquidColorName.Green });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Yellow, LiquidColorName.Brown, LiquidColorName.Purple, LiquidColorName.Lime });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Red, LiquidColorName.Brown, LiquidColorName.Green, LiquidColorName.Purple });

            //// Random level kterej zacina s 3 stejnejma barvama nahore (178 states, 48 steps, 2.37sec - s tim checkovanim na zacatku):
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Purple, LiquidColorName.Red, LiquidColorName.Blue, LiquidColorName.Orange });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Indigo, LiquidColorName.Turquoise, LiquidColorName.Orange, LiquidColorName.Lime });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Green, LiquidColorName.Gray, LiquidColorName.Indigo, LiquidColorName.Scarlet });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Brown, LiquidColorName.Brown, LiquidColorName.Brown, LiquidColorName.Purple });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Green, LiquidColorName.Turquoise, LiquidColorName.Green, LiquidColorName.Scarlet });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Yellow, LiquidColorName.Indigo, LiquidColorName.Lime, LiquidColorName.Orange });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Turquoise, LiquidColorName.Gray, LiquidColorName.Yellow, LiquidColorName.Indigo });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Yellow, LiquidColorName.Lime, LiquidColorName.Green, LiquidColorName.Purple });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Gray, LiquidColorName.Red, LiquidColorName.Scarlet, LiquidColorName.Purple });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Red, LiquidColorName.Orange, LiquidColorName.Blue, LiquidColorName.Red });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Gray, LiquidColorName.Lime, LiquidColorName.Turquoise, LiquidColorName.Yellow });
            //AddTube(i++, new LiquidColorName[] { LiquidColorName.Brown, LiquidColorName.Scarlet, LiquidColorName.Blue, LiquidColorName.Blue });

            //// Dalsi random level kterej zaina s 3 stejnejma barvama nahore (114 states, 50 steps, 1.94sec - s tim checkovanim na zacatku):
            //// (175 states, 45 steps, 2.62sec - bez tim checkovanim na zacatku):
            //AddTube(i++, new ColN[] { ColN.Indigo, ColN.Blue, ColN.Turquoise, ColN.Lime });
            //AddTube(i++, new ColN[] { ColN.Scarlet, ColN.Indigo, ColN.Gray, ColN.Scarlet });
            //AddTube(i++, new ColN[] { ColN.Purple, ColN.Brown, ColN.Green, ColN.Orange });
            //AddTube(i++, new ColN[] { ColN.Purple, ColN.Red, ColN.Blue, ColN.Orange });
            //AddTube(i++, new ColN[] { ColN.Yellow, ColN.Lime, ColN.Blue, ColN.Purple });
            //AddTube(i++, new ColN[] { ColN.Turquoise, ColN.Scarlet, ColN.Blue, ColN.Green });
            //AddTube(i++, new ColN[] { ColN.Gray, ColN.Yellow, ColN.Yellow, ColN.Green });
            //AddTube(i++, new ColN[] { ColN.Yellow, ColN.Turquoise, ColN.Brown, ColN.Orange });
            //AddTube(i++, new ColN[] { ColN.Lime, ColN.Red, ColN.Indigo, ColN.Gray });
            //AddTube(i++, new ColN[] { ColN.Green, ColN.Red, ColN.Orange, ColN.Red });
            //AddTube(i++, new ColN[] { ColN.Brown, ColN.Brown, ColN.Scarlet, ColN.Indigo });
            //AddTube(i++, new ColN[] { ColN.Lime, ColN.Gray, ColN.Turquoise, ColN.Purple });

            ////AddTube(i++, new int[] { 1,1,1 });
            //AddTube(i++, new int[] { 3, 4, 5, 6 });
            //AddTube(i++, new int[] { 2, 1 });
            //AddTube(i++, new int[] { 2, 2, 2 });
            //AddTube(i++, new int[] { 3, 4, 5, 6 });
            //AddTube(i++, new int[] { 3, 4, 5, 6 });
            //AddTube(i++, new int[] { 3, 4, 5, 6 });
            //AddTube(i++, new int[] { 1, 1, 1 });

            //AddTube(i++, new LiquidColorName[] { ColN.Pink, ColN.Pink, ColN.Olive, ColN.Gray });
            //AddTube(i++, new LiquidColorName[] { ColN.Gray, ColN.Red, ColN.Pink, ColN.Red });
            //AddTube(i++, new LiquidColorName[] { ColN.Olive, ColN.Red, ColN.Gray, ColN.Olive });
            //AddTube(i++, new LiquidColorName[] { ColN.Pink, ColN.Red, ColN.Gray, ColN.Olive });


            //AddTube(i++, new LiquidColorName[] { ColN. });
            //AddTube(i++, new LiquidColorName[] { });

            // check if puzzle has correct number for each color:
            CheckCorrectColorNumber(BoardState.Grid);

            //gameGrid = CloneGrid(gameGrid, i + 2);
            BoardState.IncrementTubesBy(2);
        }
        private void CheckCorrectColorNumber(LiquidColor?[,] gameGrid)
        {
            int colorCount = 0;
            ColorCount colorList = new ColorCount();
            foreach (LiquidColor? color in gameGrid)
            {
                if (color is not null)
                    colorList.AddColor(color.Name);
            }
            foreach (var color in colorList)
            {
                colorCount++;
                if (color.Value != gameGrid.GetLength(1))
                notification.Show($"{color.Key}: {color.Value}", 60000);
            }
            ColorsCounter = colorCount;
        }

        //public bool CanAddExtraTube()
        //{
        //    return CountColors() + appPreferences.MaximumExtraTubes + 2 - gameGrid.GetLength(0) > 0;
        //}

        //public void SetGameState(LiquidColor[,] newGameState) //smazat. presunul jsem to BoardState
        //{
        //    BoardState.Grid = BoardState.CloneGrid(newGameState);
            
        //}
        public void RestartLevel()
        {
            BoardState.ResetExtraTubesCounter();
            //BoardState.SetBoardState(StartingPosition);
            BoardState.ReplaceWith(SavedGameStates.First().BoardState);
        }
        private void GenerateStandardLevel(int numberOfColorsToGenerate)
        {
            //GenerateStandardLevel_Forward(numberOfColorsToGenerate);
            GenerateStandardLevel_Retrograde(numberOfColorsToGenerate);
        }
        private void GenerateStandardLevel_Forward(int numberOfColorsToGenerate)
        {
            Random rnd = new Random();

            List<LiquidColor> colorsList = new List<LiquidColor>();

            BoardState.Grid = new LiquidColor[numberOfColorsToGenerate + Constants.EmptyTubesAtTheStart, Constants.Layers];
            //Tube.ResetCounter();

            List<int> selectedColors = new List<int>();
            for (int i = 0; i < Constants.ColorCount; i++) // generate list of all colors. Doing '- 1' because color number 0 is blank (and is used for other purposes) but still count towards total.
            {
                selectedColors.Add(i);
            }

            for (int i = 0; i < Constants.ColorCount - numberOfColorsToGenerate; i++) // now remove some random colors. 
            {
                //selectedColors.Remove(selectedColors[NumberOfColorsToGenerate]); // this always keeps the same colors
                selectedColors.Remove(selectedColors[rnd.Next(0, selectedColors.Count)]);
            }
            
            foreach (var color in selectedColors)
            {
                colorsList.Add(new LiquidColor(color));
                colorsList.Add(new LiquidColor(color));
                colorsList.Add(new LiquidColor(color));
                colorsList.Add(new LiquidColor(color));
            }

            // add colors randomly to the grid
            for (int x = 0; x < numberOfColorsToGenerate; x++)
            {
                for (int y = 0; y < Constants.Layers; y++)
                {
                    //var maxNumber = colorsList.Count > 0 ? colorsList.Count - 1 : 0;
                    //int colorNumber = rnd.Next(0, maxNumber);
                    int colorNumber = rnd.Next(0, colorsList.Count);

                    //var asdf = colorsList[colorNumber];
                    BoardState.Grid[x, y] = colorsList[colorNumber];
                    colorsList.Remove(colorsList[colorNumber]);
                }
            }

            ColorsCounter = selectedColors.Count();
        }
        private void GenerateStandardLevel_Retrograde(int numberOfColorsToGenerate)
        {
            var generator = new RetrogradeLevelGenerator(BoardState);
            ColorsCounter = generator.GenerateStandardLevel_Retrograde(numberOfColorsToGenerate);
        }
        public void SaveGameState(int source, int target)
        {
            if (DidGameStateChange() == false)
                return;

            SavedGameStates.Add(new SavedGameState(BoardState.Clone(), source, target));
        }
        //public void RestoreGameState(SavedGameState savedGameState)
        //{
        //    if (BoardState.Grid.GetLength(0) > savedGameState.Grid.GetLength(0))
        //    {
        //        BoardState.IncrementTubesBy()
        //    }
        //    else
        //        BoardState.Grid = savedGameState.Grid;
        //}
        private bool DidGameStateChange()
        {
            if (SavedGameStates.Count == 0)
            {
                return true;
            }
            if (SavedGameStates.Last().BoardState.Grid.Length != BoardState.Grid.Length) // pokud jen pridavam extra prazdnou zkumavku tak to neukladat!
            {
                return true;
            }

            for (int x = 0; x < BoardState.Grid.GetLength(0); x++)
            {
                for (int y = 0; y < BoardState.Grid.GetLength(1); y++)
                {
                    var savedGrid = SavedGameStates.Last().BoardState.Grid[x, y];
                    var currentGrid = BoardState.Grid[x, y];
                    if (savedGrid is null && currentGrid is null)
                    {
                        continue;
                    }
                    if (savedGrid is null && currentGrid is not null
                        || savedGrid is not null && currentGrid is null)
                    {
                        return true;
                    }
                    if (savedGrid is not null && currentGrid is not null && savedGrid.Name != currentGrid.Name)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool IsLevelCompleted()
        {
            return IsLevelCompleted(BoardState.Grid);
        }
        public bool IsLevelCompleted(LiquidColor?[,] internalGameGrid)
        {
            for (int x = 0; x < BoardState.Grid.GetLength(0); x++)
            {
                //if (gameGrid[x, 0] is null || gameGrid[x, 1] is null || gameGrid[x, 2] is null || gameGrid[x, 3] is null)
                //{ // tohle tu je abych nikdy neporovnaval hodnoty GameGridu kdyz je moznost ze budou null:
                //    continue;
                //}

                // tohle tu je abych nikdy neporovnaval hodnoty GameGridu kdyz je moznost ze budou null:
                var firstCell = internalGameGrid[x, 0];
                var secondCell = internalGameGrid[x, 1];
                var thirdCell = internalGameGrid[x, 2];
                var fourthCell = internalGameGrid[x, 3];
                if (firstCell is not null &&
                    secondCell is not null &&
                    thirdCell is not null &&
                    fourthCell is not null)
                {
                    if (!(firstCell.Name == secondCell.Name &&
                        firstCell.Name == thirdCell.Name &&
                        firstCell.Name == fourthCell.Name))
                    {
                        return false;
                    }
                }
                else
                { // v pripade ze aspon jeden objekt je null, otestovat jestli jsou vsechny null
                    if (!(firstCell is null &&
                    secondCell is null &&
                    thirdCell is null &&
                    fourthCell is null))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private int CountColors(LiquidColor?[,] iGameGrid)
        {
            int numberOfColors = 0;
            List<LiquidColorName?> liquidColors = [];
            for (int x = 0; x < iGameGrid.GetLength(0); x++)
            {
                for (int y = 0; y < iGameGrid.GetLength(1); y++)
                {
                    var cell = iGameGrid[x, y];
                    if (cell is null)
                    {
                        continue;
                    }
                    if (liquidColors.Contains(cell.Name) == false)
                    {
                        liquidColors.Add(cell.Name);
                        numberOfColors++;
                    }
                }
            }
            return numberOfColors;
        }
        [RelayCommand]
        public async Task CopyExportString()
        {
            //Clipboard.SetText(ReadableGameState);
            await Clipboard.Default.SetTextAsync(BoardState.ReadableState);
            //mainVM.ClosePopupWindow();
            //await mainVM.NavigateBack();
        }
        //public static string GameStateToString(LiquidColorNew[,] gameState, bool enableSort = false)
        //{
        //    return GameStateToString(gameState, StringFormat.Names, enableSort);
        //}
        public void ResetStepBackCounter()
        {
            StepBackCounter = 0;
        }
        private void StoreStartingGrid()
        {
            //StartingPosition = GridHelper.CloneGrid(BoardState.Grid);
            //StartingPosition = BoardState.Clone();
            SavedGameStates.Clear();
            SavedGameStates.Add(new SavedGameState(BoardState.Clone()));

            appPreferences.StepBackCounter = StepBackCounter;
            appPreferences.SavedGameStatesBeforeSleepV2 = SavedGameStates;
        }
        public void SaveGameStateOnSleep()
        {
            //List<SavedGameState> copySavedGameStatesList = [];
            //foreach (var savedGameState in SavedGameStates)
            //{
            //    copySavedGameStatesList.Add(savedGameState);
            //}
            var savedGameStatesCopy = new ObservableCollection<SavedGameState>(SavedGameStates);
            appPreferences.SavedGameStatesBeforeSleepV2 = savedGameStatesCopy;
            appPreferences.StepBackCounter = StepBackCounter;
        }
        private void LoadLastLevel()
        {
            if (appPreferences.SavedGameStatesBeforeSleepV2 is not null && appPreferences.SavedGameStatesBeforeSleepV2.Count > 0)
            {
                SavedGameStates.Clear();
                foreach(var savedGame in appPreferences.SavedGameStatesBeforeSleepV2) // need this so that when I subscribe to CollectionChanged event in MainVM it doesn't get replaced
                    SavedGameStates.Add(savedGame);

                StepBackCounter = appPreferences.StepBackCounter;
                ReplaceBoardState(SavedGameStates.Last().BoardState);
            }
            else
            {
                //BoardState = StartingPosition.Clone();
                BoardState.ReplaceWith(SavedGameStates.First().BoardState);
                ColorsCounter = CountColors(SavedGameStates.First().BoardState.Grid);
            }
        }
        public void ReplaceBoardState(BoardState newBoardState)
        {
            newBoardState.SetExtraTubesCounter(BoardState.ExtraTubesCounter);
            BoardState.ReturnBoardState(newBoardState);
        }
    }
}
