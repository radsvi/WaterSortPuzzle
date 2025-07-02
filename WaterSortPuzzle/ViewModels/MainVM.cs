using Microsoft.Maui.Controls.Shapes;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace WaterSortPuzzle.ViewModels
{
    public partial class MainVM : ViewModelBase
    {
        #region Properties
        private ObservableCollection<TubeData> tubesItemsSource = new ObservableCollection<TubeData>();
        public ObservableCollection<TubeData> TubesItemsSource
        {
            get { return tubesItemsSource; }
            private set
            {
                if (value != tubesItemsSource)
                {
                    tubesItemsSource = value;
                    OnPropertyChanged();
                }
            }
        }

        //public IWindowService WindowService { get; }
        public AppSettings AppSettings { get; }
        public MainPage MainPage { get; }
        public Notification Notification { get; }
        //public AutoSolve AutoSolve { get; set; }
        private AutoSolve autoSolve;
        public AutoSolve AutoSolve
        {
            get { return autoSolve; }
            set
            {
                if (value != autoSolve)
                {
                    autoSolve = value;
                    OnPropertyChanged();
                }
            }
        }
        public GameState GameState { get; }
        private LoadLevelVM loadLevelVM;
        public LoadLevelVM LoadLevelVM
        {
            get { return loadLevelVM; }
            set
            {
                if (value != loadLevelVM)
                {
                    loadLevelVM = value;
                    OnPropertyChanged();
                }
            }
        }
        //private Grid ContainerForTubes;

        private ViewModelBase _selectedViewModel;
        public ViewModelBase SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged();
            }
        }
        public ICommand PopupWindow { get; set; }

        //private LiquidColorNew[] selectedTube;
        //public LiquidColorNew[] SelectedTube
        //{
        //    get { return selectedTube; }
        //    set
        //    {
        //        if (selectedTube is null)
        //        {
        //            selectedTube = value;
        //            //selectedTube.Margin = "0,0,0,30";
        //            //selectedTube.Selected = true;
        //            //OnPropertyChanged();
        //            return;
        //        }
        //        if (selectedTube == value) // pokud clicknu na stejnou zkumavku znova
        //        {
        //            //Debug.WriteLine("test");
        //            //selectedTube.Margin = "0,30,0,0";
        //            //selectedTube.Selected = false;
        //            selectedTube = null;
        //            //OnPropertyChanged();
        //            return;
        //        }
        //        if (selectedTube is not null && selectedTube != value)
        //        {
        //            //selectedTube.Selected = false;
        //            //selectedTube.Margin = "0,30,0,0";
        //            selectedTube = value;
        //            selectedTube = null;
        //            //OnPropertyChanged();
        //            return;
        //        }

        //    }
        //}
        public TubeReference? LastClickedTube { get; set; }
        public TubeReference? SourceTube { get; set; }

        private int tubesPerLine = 8;
        public int TubesPerLine
        {
            get
            {
                return tubesPerLine;
                //return (int)Math.Ceiling((decimal)TubesManager.NumberOfColorsToGenerate / 2);
            }
            set
            {
                tubesPerLine = value;
                OnPropertyChanged();
            }
        }

        private bool uiEnabled = false; // also used to mean that level is completed
        public bool UIEnabled
        {
            get { return uiEnabled; }
            set
            {
                if (value != uiEnabled)
                {
                    uiEnabled = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(UIDisabled));
                }
            }
        }
        public bool UIDisabled { get => !UIEnabled; }
        public bool PropertyChangedEventPaused { get; set; } = false;
        public ObservableCollection<PopupScreenActions> PopupActions { get; set; }
        internal readonly string logFolderName = "log";
        #endregion
        #region Constructor
        public MainVM(MainPage mainPage)
        {
            //this.WindowService = new WindowService();
            
            AppSettings = new AppSettings(this);
            Notification = new Notification(this);
            MainPage = mainPage;
            GameState = new GameState(this);
            //Tubes = TubesManager.Tubes;

            //PropertyChanged += RegenerateTubeDisplay;
            //PropertyChanged += TubeCount_PropertyChanged;
            //TubesManager.GlobalPropertyChanged += TubeCount_PropertyChanged;
            //Tubes.CollectionChanged += Tubes_CollectionChanged;

            //PopupWindow = new PopupScreenCommand(this);
            //if (AppSettings.DontShowHelpScreenAtStart == false)
            //{
            //    SelectedViewModel = new HelpVM(this);
            //}

            LoadLevelVM = new LoadLevelVM(this);
            //PopupActions = new ObservableCollection<PopupScreenActions>
            //{
            //    new PopupScreenActions(PopupParams.NewLevel, new NewLevelVM(this), null, () => GenerateNewLevel()),
            //    new PopupScreenActions(PopupParams.RestartLevel, new RestartLevelVM(this), null, () => RestartLevel()),
            //    new PopupScreenActions(PopupParams.LevelComplete, new LevelCompleteVM(this), null, () => GenerateNewLevel()),
            //    new PopupScreenActions(PopupParams.Help, new HelpVM(this), null, () => ClosePopupWindow()),
            //    new PopupScreenActions(PopupParams.LoadLevel, LoadLevelVM, () => LoadLevelVM.LoadLevelScreen(), () => LoadLevelVM.LoadLevel()),
            //    //new PopupScreenActions(PopupParams.LoadLevel, loadLevelVM, () => loadLevelVM.LoadLevelScreen(), () => loadLevelVM.LoadLevel()),
            //    //new PopupScreenActions(PopupParams.GameSaved, new GameSavedNotificationVM(this), null, () => CloseNotification()),
            //    new PopupScreenActions(PopupParams.SaveLevel, new SaveLevelVM(this), null, () => SaveLevel()),
            //};

            
            //ContainerForTubes = mainPage.FindByName("NotificationBox") as Grid;
            //ContainerForTubes = mainPage.FindByName("GridForTubes") as Grid;


            //if (System.IO.Directory.Exists(logFolderName) == false) System.IO.Directory.CreateDirectory(logFolderName);

            AutoSolve = new AutoSolve(this);


            //TubeControl(MainVM mainPageVM, int tubeId, LiquidColor[] liquidColors)


            OnStartingLevel();
        }
        #endregion
        #region Navigation
        //[RelayCommand]
        //async Task NavigateToOptions() => await AppShell.Current.GoToAsync(nameof(OptionsPage));
        [RelayCommand]
        async Task NavigateToPage(PopupParams menuItem)
        {
            string destination;
            switch (menuItem)
            {
                case PopupParams.OptionsPage:
                    destination = nameof(OptionsPage);
                    break;
                //case PopupParams.LoadLevelPage:
                //    destination = nameof(LoadLevelPage);
                //    break;
                default:
                    return;
            }

            await AppShell.Current.GoToAsync(destination);
        }
        [RelayCommand]
        public async Task NavigateBack() => await Shell.Current.GoToAsync($"..", true);
        [RelayCommand]
        private async Task NavigationMenuPopup(PopupParams menuItem)
        {
            switch (menuItem)
            {
                case PopupParams.NewLevel:
                    bool answer = await MainPage.DisplayAlert("New level", "Do you want to start a new level?", "OK", "Cancel");
                    if (answer)
                        GenerateNewLevel();
                    break;
                case PopupParams.RestartLevel:
                    bool answer1 = await MainPage.DisplayAlert("Restart level", "Do you want to restart current level?", "OK", "Cancel");
                    if (answer1)
                        RestartLevel();
                    break;
                case PopupParams.LevelComplete:
                    bool answer2 = await MainPage.DisplayAlert("Level complete", "Congratulation! You won!", "Next level", "Restart level");
                    if (answer2)
                        GenerateNewLevel();
                    else
                        RestartLevel();
                    break;
                case PopupParams.Help:
                    await MainPage.DisplayAlert("Help", "## Dodelat text ##", "OK");
                    break;
                //case PopupParams.LoadLevel:
                    
                //    break;
                case PopupParams.SaveLevel:
                    await MainPage.DisplayAlert("Save Level", "## Dodelat text ##", "OK");
                    break;
            }
            
            
        }








        [RelayCommand]
        private async void CloseWindow()
        {
            if (SelectedViewModel == null)
            {
                //WindowService?.CloseWindow();
                await NavigateBack();
            }
            else
            {
                ClosePopupWindow();
            }
        }
        //[RelayCommand]
        //private void ConfirmPopup()
        //{
        //    if (SelectedViewModel == null)
        //    {
        //        return;
        //    }
        //    //var action = Array.Find(PopupActions, x => x.SelectedViewModel.GetType() == SelectedViewModel.GetType());
        //    var action = PopupActions.Where(x => x.SelectedViewModel.GetType() == SelectedViewModel.GetType());
        //    action.ElementAt(0)?.ConfirmationAction.Invoke();
        //}
        [RelayCommand]
        internal void ClosePopupWindow()
        {
            //PopupWindow.Execute(null);
        }
        [RelayCommand(CanExecute = nameof(CanAddExtraTube))]
        private void AddExtraTube()
        {
            if (!CanAddExtraTube())
                return;
            
            GameState.AddExtraTube();
            AddExtraTubeCommand.NotifyCanExecuteChanged();
            RecalculateTubesPerLine();
            DrawTubes();
        }
        public bool CanAddExtraTube()
        {
            return GameState.ColorCount + AppSettings.MaximumExtraTubes + 2 - GameState.TubeCount > 0;
        }
        private void GenerateNewLevel()
        {
            ClosePopupWindow();
            GameState.GenerateNewLevel();
            OnStartingLevel();
        }
        [RelayCommand]
        public void RestartLevel()
        {
            ClosePopupWindow();
            GameState.RestartLevel();
            OnStartingLevel();
        }
        public void OnStartingLevel()
        {
            UIEnabled = true;
            DeselectTube(AnimationSpeed.Animation);
            GameState.SavedGameStates.Clear();
            GameState.LastGameState = null;
            GameState.SaveGameState();
            AutoSolve = new AutoSolve(this); // guarantees that we remove stuff like previous moves in autosolving
            RecalculateTubesPerLine();
            DrawTubes();
        }
        public string NoteForSavedLevel { get; set; }
        private void SaveLevel()
        {
            ClosePopupWindow();

            ObservableCollection<StoredLevel>? savedLevelList;
            try
            {
                savedLevelList = JsonConvert.DeserializeObject<ObservableCollection<StoredLevel>>(AppSettings.SavedLevels);
            }
            catch
            {
                return;
            }
            if (savedLevelList is null)
            {
                savedLevelList = new ObservableCollection<StoredLevel>();
            }

            savedLevelList.Add(new StoredLevel(GameState.StartingPosition, NoteForSavedLevel));

            AppSettings.SavedLevels = JsonConvert.SerializeObject(savedLevelList);
            //Settings.Default.SavedLevels = JsonConvert.SerializeObject(new ObservableCollection<StoredLevel>() { new StoredLevel(TubesManager.SavedStartingTubes) });
            NoteForSavedLevel = null;

            TokenSource = new CancellationTokenSource();
            var token = TokenSource.Token;
            PopupWindowNotification(token);
        }
        //public RelayCommand AddPresetLevels_Command => new RelayCommand(execute => LoadLevelVM.AddPresetLevels());
        public CancellationTokenSource TokenSource { get; set; } = null;
        public async void PopupWindowNotification(CancellationToken token)
        {
            //PopupWindow.Execute(PopupParams.GameSaved);
            //Thread.Sleep(500);
            //await Task.Delay(2000);
            //Task.Delay(2000);
            for (int i = 0; i < 20; i++)
            {
                await Task.Delay(100);
                if (token.IsCancellationRequested)
                {
                    break;
                }
            }

            ClosePopupWindow();
        }
        private void CloseNotification()
        {
            TokenSource?.Cancel();
        }
        //public RelayCommand OpenOptionsWindowCommand => new RelayCommand(execute => WindowService?.OpenOptionsWindow(this));
        //public RelayCommand LevelCompleteWindowCommand => new RelayCommand(execute => windowService?.OpenLevelCompleteWindow(this));
        //public RelayCommand OpenHelpFromOptionsCommand => new RelayCommand(execute =>
        //{
        //    WindowService?.CloseWindow();
        //    SelectedViewModel = new HelpVM(this);
        //});
        //public RelayCommand DisplayQuickNotificationCommand => new RelayCommand(execute => DisplayQuickNotification("asdf"));
        //private void DisplayQuickNotification(string displayText)
        //{
        //    Notification.Show(displayText);
        //}
        [RelayCommand]
        private void TestMethod()
        {
#if DEBUG
            TreeNode<ValidMove> node = new TreeNode<ValidMove>(new ValidMove(null));
            var startingNode = node;
            TreeNode<ValidMove> nextNode;

            node.Data.Priority = 100;

            nextNode = new TreeNode<ValidMove>(new ValidMove(null));
            node.AddSibling(nextNode);
            node = nextNode;

            node.Data.Priority = 1;

            nextNode = new TreeNode<ValidMove>(new ValidMove(null));
            node.AddSibling(nextNode);
            node = nextNode;

            node.Data.Priority = 5;

            nextNode = new TreeNode<ValidMove>(new ValidMove(null));
            node.AddSibling(nextNode);
            node = nextNode;

            node.Data.Priority = 3;

            //int number = TreeNodeHelper.CountSiblings(startingNode) + 1; // siblings, plus the original node
            //Notification.Show("Total Nodes: " + number);

            //Notification.Show("StepNumber of last node: " + TreeNodeHelper.GetTailNode(startingNode).Data.StepNumber, 10000);

            TreeNodeHelper.QuickSort(startingNode);

            node = startingNode;
            string displayText = string.Empty;
            while (node is not null)
            {
                //displayText += $"[{node.Data.StepNumber}: {node.Data.Priority}], ";
                displayText += $"[{node.Data.Priority}], ";

                node = node.NextSibling;
            }
            Notification.Show("Priorities list: " + displayText, 10000);
#endif
        }

        #endregion
        #region Moving Liquids
        //internal void OnTubeButtonClick(object obj)
        [RelayCommand]
        public void TubeButtonClick(object obj)
        {
            //await MainPage.DisplayAlert("Alert", $"Tube number [{tubeId}] was clicked", "OK");

            if (UIEnabled == false)
            {
                return;
            }

            if (obj is not TubeReference currentTubeReference)
                return;

            if (LastClickedTube == null)
            {
                SourceTube = currentTubeReference;
                GetTopmostLiquid(SourceTube);
                return;
            }
            if (LastClickedTube == currentTubeReference)
            {
                DeselectTube(AnimationSpeed.Animation);
                return;
            }

            // if selecting different tube:
            bool success = false;
            int successAtLeastOnce = 0;

            do
            {
                success = AddLiquidToTargetTube(currentTubeReference);
                if (success == true)
                {
                    successAtLeastOnce++;
                    RemoveColorFromSourceTube();
                    GetTopmostLiquid(SourceTube); // picks another liquid from the same tube
                }
            } while (success == true && SourceTube.TopMostLiquid is not null);
            if (successAtLeastOnce > 0)
            {
                DrawTubes();
                currentTubeReference.NumberOfRepeatingLiquids = successAtLeastOnce;
                //RippleSurfaceAnimation(currentTubeReference);
                OnChangingGameState();
            }
            if (successAtLeastOnce == 0 && AppSettings.UnselectTubeEvenOnIllegalMove == true)
            {
                DeselectTube(AnimationSpeed.Animation);
            }
        }
        public void OnChangingGameState()
        {
            DeselectTube(AnimationSpeed.Instant);

            IsLevelCompleted();
            GameState.SaveGameState();
        }
        private void GetTopmostLiquid(TubeReference sourceTube) // selects topmost liquid in a sourceTube
        {
            for (int i = Constants.Layers - 1; i >= 0; i--)
            {
                if (GameState[sourceTube.TubeId, i] is not null)
                {
                    if (LastClickedTube != sourceTube)
                        LastClickedTube = sourceTube;
                    sourceTube.TopMostLiquid = GameState[sourceTube.TubeId, i];
                    MoveTubeVertically(sourceTube, VerticalAnimation.Raise);
                    //MoveAndTiltTube(sourceTube);
                    return;
                }
            }
        }

        private bool AddLiquidToTargetTube(TubeReference currentTubeReference)
        {
            int firstEmptyLayer = -1;
            for (int y = 0; y < Constants.Layers; y++)
            {
                if (GameState[currentTubeReference.TubeId, y] == null)
                {
                    firstEmptyLayer = y;
                    break;
                }
            }
            if (firstEmptyLayer == -1)
            {
                return false;
            }

            if (firstEmptyLayer > 0)
            {
                if (SourceTube.TopMostLiquid.Name != GameState[currentTubeReference.TubeId, firstEmptyLayer - 1].Name)
                {
                    return false; // Pokud ma zkumavka v sobe uz nejaky barvy a nejvrchnejsi barva neshoulasi se SourceLiquid tak vratit false
                }
            }

            currentTubeReference.LastColorMoved = SourceTube.TopMostLiquid.Clone(); // saving this to use in CreateImageBackground(). Musim dat Clone protoze jinak se to deselectne
            GameState[currentTubeReference.TubeId, firstEmptyLayer] = SourceTube.TopMostLiquid;
            currentTubeReference.TargetEmptyRow = firstEmptyLayer;
            return true;
        }
        private void RemoveColorFromSourceTube()
        {
            for (int y = Constants.Layers - 1; y >= 0; y--)
            {
                if (GameState[SourceTube.TubeId, y] is not null)
                {
                    GameState[SourceTube.TubeId, y] = null;
                    SourceTube.TopMostLiquid = null;
                    return;
                }
            }
        }
        private void DeselectTube(AnimationSpeed type = AnimationSpeed.Animation)
        {
            if (LastClickedTube is not null)
            {
                if (SourceTube is not null)
                    MoveTubeVertically(SourceTube, VerticalAnimation.Lower, type);
                LastClickedTube = null;
            }
        }
        private void IsLevelCompleted()
        {
            if (GameState.IsLevelCompleted())
            {
                if (AppSettings.AdvancedOptionsVisible == false)
                    UIEnabled = false;
                //PopupWindow.Execute(PopupParams.LevelComplete);
                NavigationMenuPopup(PopupParams.LevelComplete);
            }
        }

        #endregion
        #region Draw tubes from code
        //[Obsolete] public RelayCommand TestDraw_Command => new RelayCommand(execute => DrawTubes());
        public void DrawTubes()
        {
            TubesItemsSource.Clear();
            TubeData.ResetCounter();

            for (int x = 0; x < GameState.GetLength(0); x++)
            {
                TubesItemsSource.Add(new TubeData(GameState[x, 0], GameState[x, 1], GameState[x, 2], GameState[x, 3]));
            }
            OnPropertyChanged(nameof(TubesItemsSource));
        }
        private void RecalculateTubesPerLine()
        {
            if (GameState.TubeCount > Constants.MaxTubesPerLine * 2)
            {
                TubesPerLine = (int)Math.Ceiling((decimal)GameState.TubeCount / 3);
            }
            else
            {
                TubesPerLine = (int)Math.Ceiling((decimal)GameState.TubeCount / 2);
            }
        }
        /// <summary>
        /// Draws border that is filled with an image that will later be animated.
        /// Surface as in water surface
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        //public static VisualElement GetDescendantByTypeAndName(VisualElement element, Type type, string layerName)
        //{
        //    if (element == null)
        //    {
        //        return null;
        //    }
        //    if (element.GetType() == type)
        //    {
        //        Grid foundElementPanel = element as Grid;
        //        if (foundElementPanel.Name == layerName)
        //        {
        //            return element;
        //        }
        //    }
        //    VisualElement foundElement = null;
        //    if (element is FrameworkElement)
        //    {
        //        (element as FrameworkElement).ApplyTemplate();
        //    }
        //    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
        //    {
        //        VisualElement visual = VisualTreeHelper.GetChild(element, i) as VisualElement;
        //        foundElement = GetDescendantByTypeAndName(visual, type, layerName);
        //        if (foundElement != null)
        //        {
        //            Panel foundElementPanel = foundElement as Panel;
        //            if (foundElementPanel.Name == layerName)
        //            {
        //                break;
        //            }
        //        }
        //    }
        //    return foundElement;
        //}
        //public static Visual GetDescendantByType(Visual element, Type type)
        //{
        //    if (element == null)
        //    {
        //        return null;
        //    }
        //    if (element.GetType() == type)
        //    {
        //        return element;
        //    }
        //    Visual foundElement = null;
        //    if (element is FrameworkElement)
        //    {
        //        (element as FrameworkElement).ApplyTemplate();
        //    }
        //    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
        //    {
        //        Visual visual = VisualTreeHelper.GetChild(element, i) as Visual;
        //        foundElement = GetDescendantByType(visual, type);
        //        if (foundElement != null)
        //        {
        //            break;
        //        }
        //    }
        //    return foundElement;
        //}
        #endregion
        #region Animation
        private static async void MoveTubeVertically(TubeReference tubeReference, VerticalAnimation verticalAnimation, AnimationSpeed speed = AnimationSpeed.Animation)
        {
            if (tubeReference.VisualElement is null)
                return;

            uint speedMS;
            if (speed == AnimationSpeed.Animation)
                speedMS = 150;
            else
                speedMS = 0;

            if (verticalAnimation == VerticalAnimation.Raise)
            {
                await tubeReference.GridElement.TranslateTo(0, -20, speedMS);
            }
            else
            {
                await tubeReference.GridElement.TranslateTo(0, 0, speedMS);
            }
        }
        //private int GetFirstEmptyLayer(TubeReference lastClickedTube)
        //{
        //    for (int y = 0; y < GameState.Layers; y++)
        //    {
        //        if (GameState[lastClickedTube.TubeId, y] is null)
        //        {
        //            return y;
        //        }
        //    }
        //    throw new Exception("This tube should always have empty space.");
        //}
        //private (ImageBrush, Grid) CreateVerticalTubeAnimationBackground(TubeReference currentTubeReference)
        //{
        //    Grid gridElement = new Grid();

        //    Border borderRoundedCorner = new Border();
        //    gridElement.Children.Add(borderRoundedCorner);
        //    borderRoundedCorner.CornerRadius = new CornerRadius(0, 0, 16, 16);
        //    borderRoundedCorner.Background = currentTubeReference.LastColorMoved.Brush; // sem poslat barvu kterou presouvam
        //    borderRoundedCorner.Margin = new Thickness(5);

        //    Binding binding = new Binding();
        //    binding.Source = borderRoundedCorner;


        //    //BindingOperations.SetBinding(column, GridViewColumn.WidthProperty, binding);
        //    //BindingOperations.SetBinding("referal elementu v kterym definuju binding", "nazev/typ property kterou chci bindovat", bindingPromenna);
        //    VisualBrush visualBrush = new VisualBrush();
        //    BindingOperations.SetBinding(visualBrush, VisualBrush.VisualProperty, binding);

        //    gridElement.OpacityMask = visualBrush;

        //    ImageBrush brush = new ImageBrush();
        //    BitmapImage bmpImg = new BitmapImage();

        //    bmpImg.BeginInit();
        //    bmpImg.UriSource = new Uri("Images\\TubeSurfaceRippleTallest.png", UriKind.Relative);
        //    bmpImg.EndInit();

        //    brush.ImageSource = bmpImg;
        //    brush.TileMode = TileMode.Tile;
        //    brush.ViewportUnits = BrushMappingMode.Absolute;
        //    //brush.Viewport = new Rect(0, 200, 129, 52);

        //    Rectangle tileSizeRectangle = new Rectangle();
        //    tileSizeRectangle.VerticalAlignment = VerticalAlignment.Top;
        //    tileSizeRectangle.Margin = new Thickness(0, -1, 0, 0);
        //    tileSizeRectangle.Width = 50;

        //    tileSizeRectangle.Height = 52 * currentTubeReference.NumberOfRepeatingLiquids;

        //    tileSizeRectangle.Fill = brush;

        //    gridElement.Children.Add(tileSizeRectangle);

        //    return (brush, gridElement);
        //}
        //internal void RippleSurfaceAnimation(TubeReference currentTubeReference)
        //{
        //    TubeControl tubeControl = (ContainerForTubes.Children[currentTubeReference.TubeId] as TubeControl)!;

        //    // Getting reference to the main grid that contains individual liquids in a tube.
        //    //Grid container = (GetDescendantByTypeAndName(tubeControl, typeof(Grid), "TubeGrid")) as Grid;
        //    Grid container = (tubeControl.FindByName("TubeGrid") as Grid)!;

        //    (var brush, var gridElement) = CreateVerticalTubeAnimationBackground(currentTubeReference);
        //    container.Children.Add(gridElement);

        //    Grid.SetRow(gridElement, GameState.Layers - 1 - currentTubeReference.TargetEmptyRow - currentTubeReference.NumberOfRepeatingLiquids + 1);
        //    Grid.SetRowSpan(gridElement, currentTubeReference.NumberOfRepeatingLiquids > 0 ? currentTubeReference.NumberOfRepeatingLiquids : 1); // I need to have this here in case of AutoSolve "skips" one step through PickNeverincorectMovesFirst()

        //    //Canvas.SetZIndex(borderElement, 3);
        //    //Grid.SetZIndex(borderElement, 4);

        //    StartAnimatingSurface(brush, container, gridElement, currentTubeReference.NumberOfRepeatingLiquids);
        //}
        //private void StartAnimatingSurface(ImageBrush brush, Grid container, Grid gridElement, int numberOfLiquids)
        //{
        //    if (brush is null)
        //    {
        //        return;
        //    }

        //    int yPosFrom = 390;
        //    int xSize = 129;
        //    int ySize = 800;

        //    var viewportAnimation = new RectAnimation()
        //    {
        //        From = new Rect(0, yPosFrom + 52 * numberOfLiquids, xSize, ySize),
        //        To = new Rect(180 * numberOfLiquids, yPosFrom, xSize, ySize),
        //        Duration = TimeSpan.FromSeconds(0.8 * numberOfLiquids)
        //    };
        //    viewportAnimation.Completed += new EventHandler((sender, e) => ViewportAnimation_Completed(sender, e, container, gridElement));
        //    brush.BeginAnimation(ImageBrush.ViewportProperty, viewportAnimation);
        //}
        private void ViewportAnimation_Completed(object? sender, EventArgs e, Grid container, Grid gridElement)
        {
            container.Children.Remove(gridElement);
        }
        //private void MoveAndTiltTube(TubeReference tubeReference)
        //{
        //    if (tubeReference.ButtonElement is null)
        //    {
        //        return;
        //    }

        //    //tubeReference.ButtonElement.RenderTransform = new RotateTransform();

        //    //Storyboard storyboard = new Storyboard();
        //    //storyboard.Duration = new Duration(TimeSpan.FromSeconds(10.0));

        //    //DoubleAnimation rotateAnimation = new DoubleAnimation()
        //    //{
        //    //    From = 0,
        //    //    To = 60,
        //    //    Duration = TimeSpan.FromSeconds(2)
        //    //};
        //    //Storyboard.SetTarget(rotateAnimation, tubeReference.ButtonElement);
        //    //Storyboard.SetTargetProperty(rotateAnimation, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));

        //    //storyboard.Children.Add(rotateAnimation);
        //    //MainPage.Resources.Add("Storyboard", storyboard);

        //    //storyboard.Begin();



        //    //var HeightAnimation = new ThicknessAnimation() { To = new Thickness(0, 0, 0, 15), Duration = TimeSpan.FromSeconds(0.1) };
        //    //tubeReference.ButtonElement.BeginAnimation(Button.MarginProperty, HeightAnimation);

        //    //tubeReference.ButtonElement.RenderTransform = new RotateTransform();
        //    //var bodymove = new TranslateTransform();


        //    //DoubleAnimation rotateAnimation = new DoubleAnimation()
        //    //{
        //    //    From = 0,
        //    //    To = 60,
        //    //    Duration = TimeSpan.FromSeconds(2)
        //    //};
        //    ////var propertypath = new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)");

        //    //tubeReference.ButtonElement.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);



        //    RotateTransform rotateTransform = new RotateTransform();
        //    DoubleAnimation doubleAnimation = new DoubleAnimation()
        //    {
        //        From = 0,
        //        To = 60,
        //        Duration = TimeSpan.FromSeconds(1)
        //    };
        //    //doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
        //    //tubeReference.ButtonElement.RenderTransformOrigin = new Point(x, y);
        //    tubeReference.ButtonElement.RenderTransform = rotateTransform;
        //    rotateTransform.BeginAnimation(RotateTransform.AngleProperty, doubleAnimation);

        //}
        #endregion
        #region Other Methods
        //private void Tubes_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{
        //    TubesPerLineCalculation();
        //}
        //private void TubesPerLineCalculation()
        //{
        //    //TubeCount = Tubes.Count;
        //    //TubeCount = Tubes.Where(tube => tube.Layers.Count > 0).Count();

        //    TubeCount = (int)Math.Ceiling((decimal)GameState.GetLength(0) / 2);
        //}
        //private void TubeCount_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        //{
        //    //TubeCount = (int)Math.Ceiling((decimal)Tubes.Count / 2);
        //    TubeCount = Tubes.Count;
        //}
        //public RelayCommand StepThroughCommand => new RelayCommand(execute => AutoSolve.StepThrough(), canExecute => AutoSolve.CompleteSolution.Count > 0 && AutoSolve.CurrentSolutionStep > 0);
        #endregion
    }
}
