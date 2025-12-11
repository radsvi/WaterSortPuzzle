
using System.Collections.Specialized;
using ColN = WaterSortPuzzle.Enums.LiquidColorName; // creating alias so that I dont have to have long names in GenerateDebugLevel();

namespace WaterSortPuzzle.Models
{
    public partial class GameState : ViewModelBase
    {
        readonly AppPreferences appPreferences;
        readonly Notification notification;
        readonly Leveling leveling;
        public GameState() { }
        public GameState(AppPreferences appPreferences, Notification notification, Leveling leveling)
        {
            this.appPreferences = appPreferences;
            this.notification = notification;
            this.leveling = leveling;
            
            if (appPreferences.LastLevelBeforeClosing is not null && appPreferences.LastLevelBeforeClosing.GameGrid.Length > 0)
            {
                LoadLastLevel();
            }
            else
            {
                GenerateNewLevel();
            }
        }
        public bool SolvedAtLeastOnce { get; set; } = false;
        private string readableGameState;
        public string ReadableGameState
        {
            get { 
                return GameStateToString(gameGrid, StringFormat.Numbers); 
            }
            set
            {
                if (value != readableGameState)
                {
                    readableGameState = value;
                    OnPropertyChanged();
                }
            }
        }
        private int stepBackPressesCounter;
        public int StepBackPressesCounter
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
                    return SavedGameStates.Count;
                }
                else
                {
                    return Constants.MaxStepBack - StepBackPressesCounter;
                }
            }
        }
        public int TubeCount { get => gameGrid.GetLength(0); }
        //[ObservableProperty]
        //[NotifyCanExecuteChangedFor(nameof(mainVM.AddExtraTubeCommand))]
        //private int colorCount = 0;
        private int colorCount;
        public int ColorCount
        {
            get { return colorCount; }
            private set
            {
                if (value != colorCount)
                {
                    colorCount = value;
                    OnPropertyChanged();
                    //OnPropertyChanged(nameof(mainVM.AddExtraTubeCommand));
                    //mainVM.AddExtraTubeCommand.NotifyCanExecuteChanged();
                }
            }
        }
        public LiquidColor[,] gameGrid;
        public LiquidColor this[int tubes, int layers]
        {
            get => gameGrid[tubes, layers];
            set
            {
                gameGrid[tubes, layers] = value;
                //OnLiquidMoving();
            }
        }
        private LiquidColor[,] startingPosition;
        public LiquidColor[,] StartingPosition { get; set; }
        //[ObservableProperty]
        //[NotifyCanExecuteChangedFor(nameof(StepBackCommand), nameof(StepBackDisplay))]
        private ObservableCollection<SavedGameState> savedGameStates = new ObservableCollection<SavedGameState>();
        public ObservableCollection<SavedGameState> SavedGameStates
        {
            get { return savedGameStates; }
            private set
            {
                if (value != savedGameStates)
                {
                    savedGameStates = value;
                    OnPropertyChanged();
                    //OnPropertyChanged(nameof(StepBackCommand));
                    //StepBackCommand.NotifyCanExecuteChanged();
                    OnPropertyChanged(nameof(StepBackDisplay));
                }
            }
        }

        public SavedGameState LastGameState { get; set; }
        //private void SetGameGrid(int numberOfTubes)
        //{
        //    //gameGrid = new LiquidColorNew[(NumberOfTubes + ExtraTubesAdded + 2), NumberOfLayers];
        //    gameGrid = new LiquidColorNew[numberOfTubes, NumberOfLayers];
        //}
        //private void OnLiquidMoving()
        //{
        //    MainPage.DrawTubes();
        //}
        public int GetLength(int dimension)
        {
            return gameGrid.GetLength(dimension);
        }
        public void GenerateNewLevel()
        {
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
            gameGrid = new LiquidColor[20, Constants.Layers];

            // Almost solved:
            AddTube(i++, new int[] { });
            AddTube(i++, new int[] { 1, 1, 1, 3 });
            AddTube(i++, new int[] { 2, 3, 1 });
            AddTube(i++, new int[] { 2, 2, 2 });
            AddTube(i++, new int[] { 3, 3 });
            AddTube(i++, new int[] { 4, 4, 4, 4 });



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
            CheckCorrectColorNumber(gameGrid);

            //gameGrid = CloneGrid(gameGrid, i + 2);
            gameGrid = CloneGrid(gameGrid, i + 2);
        }
        private void CheckCorrectColorNumber(LiquidColor[,] gameGrid)
        {
            int colorCount = 0;
            ColorCount colorList = new ColorCount();
            foreach (LiquidColor color in gameGrid)
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
            ColorCount = colorCount;
        }
        private void AddTube(int tubeNumber, int[] layers)
        {
            for (int i = 0; i < layers.Length; i++)
            {
                this[tubeNumber, i] = new LiquidColor(layers[i]);
            }
        }
        private void AddTube(int tubeNumber, LiquidColorName[] liquids)
        {
            for (int i = 0; i < liquids.Length; i++)
            {
                this[tubeNumber, i] = new LiquidColor((int)liquids[i]);
            }
        }
        /// <summary>
        /// Adding extra (empty) tube during gameplay
        /// </summary>
        public void AddExtraTube()
        {
            gameGrid = CloneGrid(gameGrid, gameGrid.GetLength(0) + 1);
        }
        //public bool CanAddExtraTube()
        //{
        //    return CountColors() + appPreferences.MaximumExtraTubes + 2 - gameGrid.GetLength(0) > 0;
        //}
        public static LiquidColor[,] CloneGridStatic(LiquidColor[,] grid)
        {
            return new GameState().CloneGrid(grid, grid.GetLength(0));
        }
        public LiquidColor[,] CloneGrid(LiquidColor[,] grid)
        {
            return CloneGrid(grid, grid.GetLength(0));
        }
        //public SavedGameState CloneGrid(SavedGameState savedGameState)
        //{
        //    return new SavedGameState(CloneGrid(savedGameState.GameGrid, savedGameState.GameGrid.GetLength(0)));
        //}
        public LiquidColor[,] CloneGrid(LiquidColor[,] gameGrid, int newNumberOfTubes)
        {
            LiquidColor[,] gridClone = new LiquidColor[newNumberOfTubes, gameGrid.GetLength(1)];
            for (int x = 0; x < gameGrid.GetLength(0); x++)
            {
                for (int y = 0; y < gameGrid.GetLength(1); y++)
                {
                    if (gameGrid[x, y] is not null)
                    {
                        gridClone[x, y] = gameGrid[x, y].Clone();
                    }
                }
            }
            return gridClone;
        }
        public void SetGameState(LiquidColor[,] newGameState)
        {
            gameGrid = CloneGrid(newGameState);
        }
        public void RestartLevel()
        {
            gameGrid = CloneGrid(StartingPosition);
        }
        private void GenerateStandardLevel(int numberOfColorsToGenerate)
        {
            Random rnd = new Random();

            List<LiquidColor> colorsList = new List<LiquidColor>();

            gameGrid = new LiquidColor[numberOfColorsToGenerate + 2, Constants.Layers];
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
                    gameGrid[x, y] = colorsList[colorNumber];
                    colorsList.Remove(colorsList[colorNumber]);
                }
            }

            ColorCount = selectedColors.Count();
        }
        public void SaveGameState(int source, int target)
        {
            if (DidGameStateChange() == true)
            //if (SolvingSteps[SolvingSteps.Count - 1] != Tubes)
            {
                if (LastGameState != null) // pridavam to tady, protoze nechci v game states mit i current game state.
                {
                    LastGameState.UpdateSourceNTarget(source, target);
                    SavedGameStates.Add(LastGameState);
                }

                LastGameState = new SavedGameState(CloneGrid(gameGrid), source, target);
                return;
            }
        }
        private bool DidGameStateChange()
        {
            if (SavedGameStates.Count == 0 && LastGameState == null)
            {
                return true;
            }
            if (LastGameState.GameGrid.Length != gameGrid.Length) // pokud jen pridavam extra prazdnou zkumavku tak to neukladat!
            {
                return true;
            }

            for (int x = 0; x < gameGrid.GetLength(0); x++)
            {
                for (int y = 0; y < gameGrid.GetLength(1); y++)
                {
                    if (LastGameState.GameGrid[x, y] is null && gameGrid[x, y] is null)
                    {
                        continue;
                    }
                    if (LastGameState.GameGrid[x, y] is null && gameGrid[x, y] is not null || LastGameState.GameGrid[x, y] is not null && gameGrid[x, y] is null)
                    {
                        return true;
                    }
                    if (LastGameState.GameGrid[x,y].Name != gameGrid[x,y].Name)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool IsLevelCompleted()
        {
            return IsLevelCompleted(gameGrid);
        }
        public bool IsLevelCompleted(LiquidColor[,] internalGameGrid)
        {
            for (int x = 0; x < gameGrid.GetLength(0); x++)
            {
                //if (gameGrid[x, 0] is null || gameGrid[x, 1] is null || gameGrid[x, 2] is null || gameGrid[x, 3] is null)
                //{ // tohle tu je abych nikdy neporovnaval hodnoty GameGridu kdyz je moznost ze budou null:
                //    continue;
                //}
                
                // tohle tu je abych nikdy neporovnaval hodnoty GameGridu kdyz je moznost ze budou null:
                if (internalGameGrid[x, 0] is not null &&
                    internalGameGrid[x, 1] is not null &&
                    internalGameGrid[x, 2] is not null &&
                    internalGameGrid[x, 3] is not null)
                {
                    if (!(internalGameGrid[x, 0].Name == internalGameGrid[x, 1].Name &&
                        internalGameGrid[x, 0].Name == internalGameGrid[x, 2].Name &&
                        internalGameGrid[x, 0].Name == internalGameGrid[x, 3].Name))
                    {
                        return false;
                    }
                }
                else
                { // v pripade ze aspon jeden objekt je null, otestovat jestli jsou vsechny null
                    if (!(internalGameGrid[x, 0] is null &&
                    internalGameGrid[x, 1] is null &&
                    internalGameGrid[x, 2] is null &&
                    internalGameGrid[x, 3] is null))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        
        [RelayCommand]
        public void WriteToFileStepBack()
        {
            //await App.Current!.Windows[0].Page!.DisplayAlert("zkouska","zkouska text", "ok");

            string exportString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "\n";
            foreach (var savedState in SavedGameStates)
            {
                exportString += GameStateToString(savedState.GameGrid, StringFormat.Names, true) + "\n";
            }
            exportString += GameStateToString(gameGrid, StringFormat.Names) + "\n";
            exportString += "===================================\n";

            //System.IO.File.WriteAllText("ExportStepBack.log", exportString);
            System.IO.File.AppendAllText("Export-StepBack.log", exportString);

            //await mainVM.NavigateBack();
        }

        private int CountColors(LiquidColor[,] iGameGrid)
        {
            int numberOfColors = 0;
            List<LiquidColorName?> liquidColors = new List<LiquidColorName?>();
            for (int x = 0; x < iGameGrid.GetLength(0); x++)
            {
                for (int y = 0; y < iGameGrid.GetLength(1); y++)
                {
                    if (iGameGrid[x, y] is null)
                    {
                        continue;
                    }
                    if (liquidColors.Contains(iGameGrid[x, y].Name) == false)
                    {
                        liquidColors.Add(iGameGrid[x, y].Name);
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
            await Clipboard.Default.SetTextAsync(ReadableGameState);
            //mainVM.ClosePopupWindow();
            //await mainVM.NavigateBack();
        }
        //public static string GameStateToString(LiquidColorNew[,] gameState, bool enableSort = false)
        //{
        //    return GameStateToString(gameState, StringFormat.Names, enableSort);
        //}
        public static string GameStateToString(LiquidColor[,] gameState, StringFormat format = StringFormat.Names, bool enableSort = true)
        {
            List<string> intGameState = new List<string>();
            for (int x = 0; x < gameState.GetLength(0); x++)
            {
                string tubeString = "[";
                for (int y = gameState.GetLength(1) - 1; y >= 0; y--)
                {
                    if (gameState[x, y] is not null)
                    {
                        //tubeInt += (int)gameState[x, y].Name * (int)Math.Pow(100,y);
                        if (format == StringFormat.Names)
                        {
                            tubeString += (gameState[x, y].Name).ToString();
                        }
                        else
                        {
                            tubeString += ((int)gameState[x, y].Name).ToString("00"); // this format is used for debugging. To easily export the gamestate as a string.
                        }
                    }
                    else
                        tubeString += "-";
                    if (y > 0) tubeString += ".";
                }
                tubeString += "]";
                intGameState.Add(tubeString);
            }
            if (enableSort)
            {
                intGameState.Sort(); // nechci sortovat kdyz chci vizualizaci
            }
            string stringGameState = string.Empty;
            foreach (var tube in intGameState)
            {
                stringGameState += tube.ToString();
            }
            return stringGameState;
        }
        public void ResetStepBackCounter()
        {
            StepBackPressesCounter = 0;
        }
        private void LoadLastLevel()
        {
            StartingPosition = CloneGrid(appPreferences.LastLevelBeforeClosing.GameGrid);
            //gameGrid = CloneGrid(StartingPosition);
            //ColorCount = CountColors(StartingPosition);
            LoadGameState();
        }
        private void StoreStartingGrid()
        {
            StartingPosition = CloneGrid(gameGrid);
            appPreferences.LastLevelBeforeClosing = new StoredLevel(StartingPosition, "Last level");
            appPreferences.StepBackPressesCounter = StepBackPressesCounter;
            appPreferences.SavedGameStatesBeforeSleep = new ObservableCollection<SavedGameState>();
        }
        public void SaveGameStateOnSleep()
        {
            appPreferences.GameStateBeforeSleep = gameGrid;

            ObservableCollection<SavedGameState> copySavedGameStates = [];
            foreach (var savedGameState in SavedGameStates)
            {
                copySavedGameStates.Add(savedGameState);
            }
            copySavedGameStates.Add(LastGameState);
            appPreferences.SavedGameStatesBeforeSleep = copySavedGameStates;
            appPreferences.StepBackPressesCounter = StepBackPressesCounter;
        }
        private void LoadGameState()
        {
            if (appPreferences.SavedGameStatesBeforeSleep is not null && appPreferences.SavedGameStatesBeforeSleep.Count > 0)
            {
                LastGameState = appPreferences.SavedGameStatesBeforeSleep.Last();
                SavedGameStates = appPreferences.SavedGameStatesBeforeSleep;
                SavedGameStates.Remove(SavedGameStates.Last());
                StepBackPressesCounter = appPreferences.StepBackPressesCounter;

                gameGrid = appPreferences.GameStateBeforeSleep;
            }
            else
            {
                gameGrid = CloneGrid(StartingPosition);
                ColorCount = CountColors(StartingPosition);
            }
        }
    }
}
