//using CommunityToolkit.Maui;
using System.Collections.Specialized;

namespace WaterSortPuzzle.ViewModels
{
    public partial class MainVM : ViewModelBase
    {
        #region Constructor
        public MainVM(AppPreferences appPreferences, GameState gameState, Notification notification, AutoSolve autoSolve, Leveling leveling, IConfirmationPopupService popupService)
        {
            AppPreferences = appPreferences;
            GameState = gameState;
            Notification = notification;
            AutoSolve = autoSolve;
            Leveling = leveling;
            this.popupService = popupService;
            App.Current!.UserAppTheme = AppPreferences.ThemeUserSetting;

            GameState.SavedGameStates.CollectionChanged += CollectionChangedHandler;
            AppPreferences.PropertyChanged += PropertyChangedHandler;
            GameState.PropertyChanged += PropertyChangedHandler;
            AutoSolve.PropertyChanged += PropertyChangedHandler;

            //if (appPreferences.LastLevelBeforeClosing is null || appPreferences.LastLevelBeforeClosing.GameGrid.Length == 0)
            //{
            //    OnStart();
            //}
            OnStart();
        }
        private void PropertyChangedHandler(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AppPreferences.MaximumExtraTubes) || e.PropertyName == nameof(GameState.ColorsCounter))
            {
                RefreshAddExtraTubeState();
            }
            else if (e.PropertyName == nameof(AppPreferences.UnlimitedStepBack)
                || e.PropertyName == nameof(GameState.SavedGameStates)
                || e.PropertyName == nameof(GameState.StepBackPressesCounter))
            {
                RefreshStepBackState();
            }
            else if (e.PropertyName == nameof(AutoSolve.CurrentSolutionStep))
            {
                OnPropertyChanged(nameof(NextStepButtonText));
            }
            else if (e.PropertyName == nameof(AppPreferences.LoadDebugLevel) || e.PropertyName == nameof(AppPreferences.SingleLevelMode))
            {
                OnPropertyChanged(nameof(NewLevelButtonText));
            }
            else if (e.PropertyName == nameof(AutoSolve.InProgress))
            {
                GrayOutMiddleOfTheScreen = AutoSolve!.InProgress;
            }
        }
        private void CollectionChangedHandler(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Remove)
            {
                RefreshStepBackState();
            }
        }
        private void RefreshStepBackState()
        {
            OnPropertyChanged(nameof(CanStepBack));
            OnPropertyChanged(nameof(StepBackImage));
            OnPropertyChanged(nameof(StepBackButtonText));
            StepBackCommand.NotifyCanExecuteChanged();
        }
        private void RefreshAddExtraTubeState()
        {
            OnPropertyChanged(nameof(CanAddExtraTube));
            OnPropertyChanged(nameof(AddExtraTubeImage));
            AddExtraTubeCommand.NotifyCanExecuteChanged();
            //OnPropertyChanged(nameof(CanStepBack));
            //OnPropertyChanged(nameof(StepBackImage));
            //OnPropertyChanged(nameof(StepBackButtonText));
            //StepBackCommand.NotifyCanExecuteChanged();
        }
        #endregion
        #region Properties
        [ObservableProperty]
        private ObservableCollection<TubeData> tubesItemsSource = new ObservableCollection<TubeData>();
        //public ObservableCollection<TubeData> TubesItemsSource
        //{
        //    get { return tubesItemsSource; }
        //    private set
        //    {
        //        if (value != tubesItemsSource)
        //        {
        //            tubesItemsSource = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        //public IWindowService WindowService { get; }

        // Using these in MainPage.xaml so they need to be public and should remain properties
        public AppPreferences AppPreferences { get; }
        public Notification Notification { get; }
        private AutoSolve? autoSolve;
        public AutoSolve? AutoSolve
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
        private LoadLevelVM? loadLevelVM;
        public LoadLevelVM? LoadLevelVM
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
        private Leveling leveling;
        public Leveling Leveling
        {
            get => leveling;
            set { leveling = value; OnPropertyChanged(); }
        }

        private ViewModelBase? selectedViewModel;
        public ViewModelBase? SelectedViewModel
        {
            get { return selectedViewModel; }
            set
            {
                selectedViewModel = value;
                OnPropertyChanged();
            }
        }
        public TubeReference? LastClickedTube { get; set; }
        public TubeReference? SourceTube { get; set; }

        private int tubesPerLine = 9;
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
                OnPropertyChanged(nameof(TubeStyle));
            }
        }
        public int TubeStyle
        {
            get
            {
                //if (GameState.TubeCount <= 10)
                //    return 0;
                //else if (GameState.TubeCount <= 14)
                //    return 1;
                //else
                //    return 2;

                if (GameState.BoardState.GetTubeCount() <= 14)
                    return 0;
                else
                    return 1;
            }
        }
        public bool TubesClickable
        {
            get
            {
                if (UIEnabled == true || AutoSolve?.InProgress == false)
                    return true;
                else
                    return false;
            }
        }
        private bool uiEnabled = true; // also used to mean that level is completed
        private readonly IConfirmationPopupService popupService;

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
                    OnPropertyChanged(nameof(TubesClickable));
                }
            }
        }
        private bool grayOutMiddleOfTheScreen;
        public bool GrayOutMiddleOfTheScreen
        {
            get { return grayOutMiddleOfTheScreen; }
            private set
            {
                if (value != grayOutMiddleOfTheScreen)
                {
                    grayOutMiddleOfTheScreen = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool UIDisabled { get => !UIEnabled; }
        public bool AutoSolveUsed { get; private set; } = false;
        public ObservableCollection<PopupScreenActions>? PopupActions { get; set; }

        #endregion
        #region Navigation
        public string StepBackButtonText { get => $"({GameState.StepBackDisplay})"; }
        public string NextStepButtonText { get => $"({AutoSolve?.CurrentSolutionStep})"; }
        public string NewLevelButtonText
        {
            get
            {
                if (AppPreferences.LoadDebugLevel && AppPreferences.SingleLevelMode) return $"[DEBUG]{Environment.NewLine}level";
                else return $"New{Environment.NewLine}level";
            }
        }
        [RelayCommand]
        void StartAutoSolve()
        {
            AutoSolve?.Start();
            AutoSolveUsed = true;
        }
        [RelayCommand]
        public async Task StepThrough()
        {
            await MakeAMove(AutoSolve!.CompleteSolution[--AutoSolve.CurrentSolutionStep]);
        }
        private async Task MakeAMove(ValidMove move)
        {
            //Debug.WriteLine($"# [{node.Source.X},{node.Source.Y}] => [{node.Target.X},{node.Target.Y}] {{{node.Source.ColorName}}} {{HowMany {node.Source.NumberOfRepeatingLiquids}}}");

            //Node.Data.GameState = newGameState;

            //var upcomingStep = new SolutionStepsOLD(newGameState, Node.Data);
            //SolvingStepsOLD.Add(upcomingStep);

            //previousGameState = node.Data.GameState; // tohle je gamestate kterej uchovavam jen uvnitr autosolvu
            GameState.BoardState.SetBoardState(move.GameState);
            var currentTubeReference = new TubeReference(
                move.Target.X,
                move.GameState[move.Target.X, move.Target.Y],
                move.Target.Y,
                move.Source.NumberOfRepeatingLiquids,
                TubesItemsSource[move.Target.X]
            );

            var sourceTubeReference = new TubeReference(
                move.Source.X,
                move.GameState[move.Source.X, move.Source.Y],
                move.Source.Y,
                TubesItemsSource[move.Source.X]
            );
            //var task = DrawTubesAsync(move.Source.X, move.Target.X);

            //RippleSurfaceAnimationPrep(currentTubeReference, LiquidHelper.GetKey((LiquidColorName)move.Source.ColorName));

            //currentTubeReference.GridElement
            //VisualTreeElementExtensions.GetVisualTreeDescendants();

            await DisplayChanges(currentTubeReference, sourceTubeReference, LiquidHelper.GetKey((LiquidColorName)move.Source.ColorName!), true);

            OnChangingGameState(move.Source.X, move.Target.X);
        }
        [RelayCommand(CanExecute = nameof(CanStepBack))]
        private async Task StepBack()
        {
            if (CanStepBack == false)
                return;

            DeselectTube();

            GameState.StepBackPressesCounter++;

            SavedGameState lastGameStatus = GameState.SavedGameStates[GameState.SavedGameStates.Count - 1];

            PropertyChangedEventPaused = true;
            GameState.ReplaceBoardState(lastGameStatus.BoardState);
            //GameState.RestoreGameState(lastGameStatus);
            PropertyChangedEventPaused = false;

            GameState.LastGameState = SavedGameState.Clone(lastGameStatus);

            GameState.SavedGameStates.Remove(lastGameStatus);


            if (autoSolve?.CompleteSolution.Count > 0)
                autoSolve.CurrentSolutionStep++;

            RecalculateTubesPerLine();
            autoSolve?.SoftReset();
            await DrawTubesAsync(lastGameStatus.Source, lastGameStatus.Target);
        }
        private bool CanStepBack
        {
            get
            {
                if (GameState.SavedGameStates.Count == 0
                    || (AppPreferences.UnlimitedStepBack == false && Constants.MaxStepBack <= GameState.StepBackPressesCounter))
                    return false;

                return true;
            }
        }

        //public string StepBackImage =>
        //    CanStepBack ? "button_back.png" : "button_gray_back.png";
        public string StepBackImage =>
            CanStepBack ? "button_back.png" : "button_gray_back.png";

        //private void UpdateCanStepBack()
        //{
        //    CanStepBack =
        //        GameState.SavedGameStates.Count > 0 &&
        //        (AppPreferences.UnlimitedStepBack ||
        //         Constants.MaxStepBack > GameState.StepBackPressesCounter);
        //}

        //[RelayCommand]
        //async Task NavigateToOptions() => await AppShell.Current.GoToAsync(nameof(OptionsPage));
        [RelayCommand]
        async Task NavigateToPage(PopupParams menuItem)
        {
            //string destination;
            //switch (menuItem)
            //{
            //    case PopupParams.OptionsPage:
            //        destination = nameof(OptionsPage);
            //        break;
            //    //case PopupParams.LoadLevelPage:
            //    //    destination = nameof(LoadLevelPage);
            //    //    break;
            //    default:
            //        return;
            //}

            string route;
            if (menuItem == PopupParams.MainPage)
            {
                route = "///" + menuItem.ToString();
            }
            else
            {
                route = menuItem.ToString();
            }

            if (Shell.Current.FlyoutIsPresented is true)
                Shell.Current.FlyoutIsPresented = false;
            await AppShell.Current.GoToAsync(route);
        }
        [RelayCommand]
        public async Task NavigateBack() => await Shell.Current.GoToAsync($"..", true);
        [RelayCommand]
        private async Task NavigationMenuPopup(PopupParams menuItem)
        {
            bool answer;
            switch (menuItem)
            {
                case PopupParams.NewLevel:
                    answer = await popupService.ShowPopupAsync<CustomPopupVM>("New level", "Do you want to start a new level?", "OK", "Cancel");
                    //answer = await App.Current!.Windows[0].Page!.DisplayAlert("Restart level", "Do you want to restart current level?", "OK", "Cancel");
                    if (answer)
                        GenerateNewLevel();
                    break;
                case PopupParams.RestartLevel:
                    answer = await popupService.ShowPopupAsync<CustomPopupVM>("Restart level", "Do you want to restart current level?", "OK", "Cancel");
                    //answer = await popupService.ShowPopupAsync<FullscreenPopupVM>("YOU WIN", "Level completed automatically. Would you like to try for yourself?", "Next level", "Restart level");
                    if (answer)
                        RestartLevel();
                    break;
                case PopupParams.LevelComplete:
                    if (AutoSolveUsed)
                    {
                        answer = await popupService.ShowPopupAsync<FullscreenPopupVM>("YOU WIN", "Level completed automatically. Would you like to try for yourself?", "Next level", "Restart level");
                    }
                    else
                    {
                        answer = await popupService.ShowPopupAsync<FullscreenPopupVM>("YOU WIN", "Congratulation! You won!", "Next level", "Restart level");
                    }

                    if (answer)
                        GenerateNewLevel();
                    else
                        RestartLevel();
                    break;
                case PopupParams.Help:
                    await DisplayHelpPopup();
                    break;
                    //case PopupParams.LoadLevel:

                    //    break;
                    //case PopupParams.SaveLevel:
                    //    await ShowCustomPopup("Save Level", "## Dodelat text ##", "OK");
                    //    break;
            }
        }


        //private void DisplayStartupPopup() // predelat, tohle je hrozny to takhle mit dvakrat
        //{
        //    if (AppPreferences.DontShowHelpScreenAtStart)
        //        return;

        //    string text = "Separate each color into different flasks.\n";
        //    text += "You can only move matching colors onto each other.\n";
        //    text += "You can always move colors to empty flask.\n";
        //    text += "You can add empty flasks, if you are really stuck.\n";

        //    Task.Run(async () =>
        //    {
        //        await Task.Delay(1000);
        //        //App.AlertSvc.ShowConfirmation("Title", "Confirmation message.", (result =>
        //        //{
        //        //    App.AlertSvc.ShowAlert("Result", $"{result}");
        //        //}));
        //        //App.AlertSvc.ShowConfirmation("Help", text, new Action<bool>((c) => { }), "Don't show this again.", "Close");
        //        //App.AlertSvc.ShowConfirmationSimple("Help", text, "Don't show this again.", "Close");

        //        App.AlertSvc.ShowConfirmation("Help", text, (result =>
        //        {
        //            //App.AlertSvc.ShowAlert("Result", $"{result}");
        //            if (result)
        //                AppPreferences.DontShowHelpScreenAtStart = true;

        //        }), "Don't show this again.", "Close");
        //    });
        //}
        public async Task DisplayHelpPopup()
        {
            string text = "Separate each color into different flasks.\n";
            text += "You can only move matching colors onto each other.\n";
            text += "You can always move colors to empty flask.\n";
            text += "You can add empty flasks, if you are really stuck.\n";
            bool answer = await App.Current!.Windows[0].Page!.DisplayAlert("Help", text, "Don't show this again.", "Close");
            if (answer)
                AppPreferences.DontShowHelpScreenAtStart = true;
        }
        [RelayCommand(CanExecute = nameof(CanAddExtraTube))]
        private void AddExtraTube()
        {
            if (!CanAddExtraTube())
                return;

            GameState.BoardState.AddExtraTube();
            RefreshAddExtraTubeState();
            RecalculateTubesPerLine();
            _ = DrawTubesAsync(GameState.BoardState.Grid.GetLength(0));
        }
        public bool CanAddExtraTube()
        {
            return GameState.BoardState.CanAddExtraTube();
        }
        public string AddExtraTubeImage =>
            CanAddExtraTube() ? "button_plus_one.png" : "button_gray_plus_one.png";
        private void GenerateNewLevel()
        {
            GameState.GenerateNewLevel();
            OnStartingLevel();
        }
        [RelayCommand]
        public void RestartLevel()
        {
            GameState.RestartLevel();
            OnStartingLevel();
        }
        public string? NoteForSavedLevel { get; set; }
        private void SaveLevel()
        {
            ObservableCollection<StoredLevel>? savedLevelList;
            try
            {
                savedLevelList = JsonConvert.DeserializeObject<ObservableCollection<StoredLevel>>(AppPreferences.SavedLevels);
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

            AppPreferences.SavedLevels = JsonConvert.SerializeObject(savedLevelList);
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

            //ClosePopupWindow();
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
        [RelayCommand]
        void OpenFlyout()
        {
            Shell.Current.FlyoutIsPresented = true;
        }
        #endregion
        #region Moving Liquids
        //internal void OnTubeButtonClick(object obj)
        [RelayCommand]
        public async Task TubeButtonClick(object obj)
        {
            //await MainPage.DisplayAlert("Alert", $"Tube number [{tubeId}] was clicked", "OK");

            if (TubesClickable == false)
                return;

            AutoSolve?.SoftReset(); // Disable autosolve when changing the game grid manually

            if (obj is not TubeReference currentTubeReference)
                return;

            if (currentTubeReference.TubeData.IsBusy == true)
                return;

            if (LastClickedTube == null)
            {
                SourceTube = currentTubeReference;
                GetTopmostLiquid(SourceTube);
                return;
            }
            if (LastClickedTube.TubeId == currentTubeReference.TubeId)
            {
                DeselectTube();
                return;
            }

            // if selecting different tube:
            bool success = false;
            int successAtLeastOnce = 0;
            LiquidColor? currentLiquid = null;

            do
            {
                success = AddLiquidToTargetTube(currentTubeReference);
                if (success == true)
                {
                    successAtLeastOnce++;
                    currentLiquid = SourceTube.TopMostLiquid;
                    RemoveColorFromSourceTube();
                    GetTopmostLiquid(SourceTube); // picks another liquid from the same tube
                }
            } while (success == true && SourceTube.TopMostLiquid is not null);
            if (successAtLeastOnce > 0)
            {
                currentTubeReference.NumberOfRepeatingLiquids = successAtLeastOnce;
                currentTubeReference.TubeData.NumberOfRepeatingLiquids = successAtLeastOnce;
                await DisplayChanges(currentTubeReference, SourceTube, currentLiquid);


                //OnPropertyChanged(nameof(GameState.StepBackDisplay));
                OnPropertyChanged(nameof(StepBackButtonText));
                OnChangingGameState(SourceTube.TubeId, currentTubeReference.TubeId);
            }
            if (successAtLeastOnce == 0 && AppPreferences.UnselectTubeEvenOnIllegalMove == true)
            {
                DeselectTube();
            }
        }
        public void OnChangingGameState(int source, int target)
        {
            DeselectTube();

            IsLevelCompleted();
            GameState.SaveGameState(source, target);
        }
        private void OnStart() // when starting the application
        {
            GameState.SaveGameState(-1, -1);
            RecalculateTubesPerLine();
            RefreshAddExtraTubeState();
            RefreshStepBackState();
            OnPropertyChanged(nameof(StepBackButtonText));
            var task = DrawTubesAsync();
        }
        public void OnStartingLevel()
        {
            GameState.ResetStepBackCounter();
            UIEnabled = true;
            DeselectTube();
            GameState.SavedGameStates.Clear();
            GameState.LastGameState = null;
            GameState.SaveGameState(-1, -1);
            //AutoSolve = new AutoSolve(); // guarantees that we remove stuff like previous moves in autosolving
            AutoSolve?.Reset();
            AutoSolveUsed = false;
            RecalculateTubesPerLine();
            RefreshAddExtraTubeState();
            RefreshStepBackState();
            OnPropertyChanged(nameof(StepBackButtonText));
            var task = DrawTubesAsync();
        }
        private void GetTopmostLiquid(TubeReference sourceTube) // selects topmost liquid in a sourceTube
        {
            for (int i = Constants.Layers - 1; i >= 0; i--)
            {
                if (GameState.BoardState[sourceTube.TubeId, i] is not null)
                {
                    if (LastClickedTube != sourceTube)
                        LastClickedTube = sourceTube;
                    sourceTube.TopMostLiquid = GameState.BoardState[sourceTube.TubeId, i];
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
                if (GameState.BoardState[currentTubeReference.TubeId, y] == null)
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
                if (SourceTube!.TopMostLiquid.Name != GameState.BoardState[currentTubeReference.TubeId, firstEmptyLayer - 1].Name)
                {
                    return false; // Pokud ma zkumavka v sobe uz nejaky barvy a nejvrchnejsi barva nesouhlasi se SourceLiquid tak vratit false
                }
            }

            currentTubeReference.LastColorMoved = SourceTube!.TopMostLiquid.Clone(); // saving this to use in CreateImageBackground(). Musim dat Clone protoze jinak se to deselectne
            GameState.BoardState[currentTubeReference.TubeId, firstEmptyLayer] = SourceTube.TopMostLiquid;
            currentTubeReference.TargetEmptyRow = firstEmptyLayer;
            return true;
        }
        private void RemoveColorFromSourceTube()
        {
            for (int y = Constants.Layers - 1; y >= 0; y--)
            {
                if (GameState.BoardState[SourceTube!.TubeId, y] is not null)
                {
                    GameState.BoardState[SourceTube.TubeId, y] = null;
                    SourceTube.TopMostLiquid = null;
                    return;
                }
            }
        }
        private void DeselectTube()
        {
            if (LastClickedTube is not null)
            {
                if (SourceTube is not null)
                    MoveTubeVertically(SourceTube, VerticalAnimation.Lower);
                LastClickedTube = null;
            }
        }
        private void IsLevelCompleted()
        {
            if (GameState.IsLevelCompleted())
            {
                if (AppPreferences.AdvancedOptionsVisible == false)
                    UIEnabled = false;
                //PopupWindow.Execute(PopupParams.LevelComplete);
                var task = NavigationMenuPopup(PopupParams.LevelComplete);
                if (AppPreferences.SingleLevelMode == false && GameState.SolvedAtLeastOnce == false)
                {
                    Leveling.LevelFinished(GameState.ColorsCounter);
                }
                GameState.SolvedAtLeastOnce = true;
                //Task.Run(() => NavigationMenuPopup(PopupParams.LevelComplete));
                //Task.Run(() => NavigationMenuPopup(PopupParams.LevelComplete)).GetAwaiter().GetResult();
            }
        }

        #endregion
        #region Draw tubes from code
        public void DrawTubes(int source = -1, int target = -1)
        {
            //if (TubesItemsSource.Count == 0)
            if (source == -1 || target == -1)
            {
                //TubesItemsSource.Clear(); // blbne v tomhle specifickym pripade
                //for (int i = TubesItemsSource.Count - 1; i >= 0; i--)
                //{
                //    TubesItemsSource.Remove(TubesItemsSource[i]);
                //}
                //MainThread.BeginInvokeOnMainThread(() => {
                //    TubesItemsSource.Clear();
                //});
                TubeData.ResetCounter();
                var newTubeItems = new ObservableCollection<TubeData>();
                for (int x = 0; x < GameState.BoardState.GetLength(0); x++)
                {
                    //TubesItemsSource.Add(new TubeData(GameState[x, 0], GameState[x, 1], GameState[x, 2], GameState[x, 3]));
                    newTubeItems.Add(new TubeData(GameState.BoardState.Grid, x));
                }
                TubesItemsSource = newTubeItems;
                OnPropertyChanged(nameof(TubesItemsSource));
            }
            else
            {
                TubesItemsSource[source].CopyValuesFrom(GameState.BoardState.Grid, source);
                if (target != -1)
                    TubesItemsSource[target].CopyValuesFrom(GameState.BoardState.Grid, target);
            }
        }
        public async Task DrawTubesAsync(int source = -1, int target = -1)
        {
            await Task.Run(() => DrawTubes(source, target));
        }
        private void RecalculateTubesPerLine()
        {
            if (GameState.BoardState.GetTubeCount() > Constants.MaxTubesPerLine * 2)
            {
                TubesPerLine = (int)Math.Ceiling((decimal)GameState.BoardState.GetTubeCount() / 3);
            }
            else
            {
                TubesPerLine = (int)Math.Ceiling((decimal)GameState.BoardState.GetTubeCount() / 2);
            }
        }
        ///// <summary>
        ///// Draws border that is filled with an image that will later be animated.
        ///// Surface as in water surface
        ///// </summary>
        ///// <param name="container"></param>
        ///// <returns></returns>
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
        [RelayCommand]
        private void TriggerAnimation()
        {
            //ShouldAnimate = false; // reset if needed
            //ShouldAnimate = true;  // trigger animation

            TubesItemsSource[1].IsRaised = !TubesItemsSource[1].IsRaised;

            //TubesItemsSource[1].RippleGridRow += 50;
            //TubesItemsSource[2].RippleGridRow -= 50;
        }
        private static void MoveTubeVertically(TubeReference tubeReference, VerticalAnimation verticalAnimation)
        {
            //if (tubeReference.VisualElement is null)
            //    return;

            //uint speedMS = 75;

            if (verticalAnimation == VerticalAnimation.Raise)
            {
                tubeReference.TubeData.IsRaised = true;
            }
            else
            {
                tubeReference.TubeData.IsRaised = false;
            }
        }
        //private static async void MoveTubeVertically(TubeReference tubeReference, VerticalAnimation verticalAnimation, AnimationSpeed speed = AnimationSpeed.Animation)
        //{
        //    if (tubeReference.VisualElement is null)
        //        return;

        //    uint speedMS = 75;

        //    if (verticalAnimation == VerticalAnimation.Raise)
        //    {
        //        await tubeReference.GridElement.TranslateTo(0, -20, speedMS);
        //    }
        //    else
        //    {
        //        await tubeReference.GridElement.TranslateTo(0, 0, speedMS);
        //    }
        //}
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
        async Task DisplayChanges(TubeReference currentTubeReference, TubeReference sourceTube, LiquidColor currentLiquid, bool disabledAnimation = false)
        {
            if (AppPreferences.AnimationSpeed == AnimationSpeed.Instant || disabledAnimation == true)
            {
                await DrawTubesAsync(sourceTube.TubeId, currentTubeReference.TubeId);
            }
            else
            {
                //var rippleLayoutElement = GetVisualTreeDescendantsByStyleId<Grid>(currentTubeReference.GridElement, "RippleEffectElement");
                //if (rippleLayoutElement is null)
                //    return;

                //(var innerGrid, var image) = RippleSurfaceBackgroundCreation(rippleLayoutElement, currentTubeReference, currentLiquid);



                currentTubeReference.TubeData.RippleGridRow = Constants.Layers - 1 - currentTubeReference.TargetEmptyRow;
                currentTubeReference.TubeData.RippleGridRowSpan = currentTubeReference.NumberOfRepeatingLiquids > 0 ? currentTubeReference.NumberOfRepeatingLiquids : 1; // I need to have this here in case of AutoSolve "skips" one step through PickNeverincorectMovesFirst()

                RepositionSourceTube(sourceTube, currentTubeReference);

                currentTubeReference.TubeData.TriggerRippleEffect = true;
                //currentTubeReference.TubeData.RippleGridVisible = true;
                currentTubeReference.TubeData.Animate = AnimationType.RippleEffect;
                currentTubeReference.TubeData.PourBackgroundColor = currentLiquid.Brush;

                await DrawTubesAsync(sourceTube.TubeId, currentTubeReference.TubeId);

                //currentTubeReference.TubeData.RippleGridRow = 0;
                //currentTubeReference.TubeData.RippleGridRowSpan = 1;

                //uint duration = Constants.PouringAnimationDuration * (uint)currentTubeReference.NumberOfRepeatingLiquids;
                //int distancePerLiquid = 40;
                //int distance = Constants.TubeImageOffset - (currentTubeReference.NumberOfRepeatingLiquids * distancePerLiquid);




                //await image.TranslateTo(0, distance, duration);
                //rippleLayoutElement.Children.Clear();
                //rippleLayoutElement.Children.Remove(innerGrid);

                //currentTubeReference.TubeData.RippleGridVisible = false;
            }
        }
        private void RepositionSourceTube(TubeReference sourceTube, TubeReference targetTube)
        {
            // this is used to determine how long the tube should wait until it returns:
            sourceTube.TubeData.NumberOfRepeatingLiquids = targetTube.NumberOfRepeatingLiquids;

            //var sourceGrid = sourceTube.GridElement;
            //var targetGrid = targetTube.GridElement;


            //var bounds = sourceGrid.GetBoundsRelativeTo((IView)targetGrid);
            //var bounds = sourceGrid.GetBoundsRelativeTo(targetGrid);
            //var rect1 = ElementCoordinates.GetCoordinates(sourceGrid);
            //var rect2 = ElementCoordinates.GetCoordinates(targetGrid);
            var rect1 = sourceTube.TubeData.Coordinates;
            var rect2 = targetTube.TubeData.Coordinates;
            sourceTube.TubeData.TargetTube = targetTube.TubeData;

            Rect diff = new Rect(
                rect2.X - rect1.X,
                rect2.Y - rect1.Y,
                rect1.Width,
                rect1.Height
            );

            //sourceTube.TubeData.Coordinates = new Rect(diff.X, diff.Y, -1, -1);
            sourceTube.TubeData.TranslateX = diff.X;
            sourceTube.TubeData.TranslateY = diff.Y;
            sourceTube.TubeData.ActualWidth = rect1.Width;
            sourceTube.TubeData.ActualHeight = rect1.Height;

            //sourceTube.TubeData.IsVisible = false;



            sourceTube.TubeData.TriggerReposition = true;
            //sourceTube.TubeData.TriggerReposition = false;

        }

        //internal (Grid, Image) RippleSurfaceBackgroundCreation<T>(T rippleLayoutElement, TubeReference currentTubeReference, LiquidColor sourceLiquid) where T : Layout
        //{
        //    var innerGrid = new Grid { BackgroundColor = sourceLiquid.Brush, IsClippedToBounds = true, ZIndex = 1000 };

        //    Grid.SetRow(innerGrid, Constants.Layers - 1 - currentTubeReference.TargetEmptyRow);
        //    Grid.SetRowSpan(innerGrid, currentTubeReference.NumberOfRepeatingLiquids > 0 ? currentTubeReference.NumberOfRepeatingLiquids : 1); // I need to have this here in case of AutoSolve "skips" one step through PickNeverincorectMovesFirst()

        //    var image = new Image {
        //        Source = "tube_surface_ripple_anim.gif",
        //        Aspect = Aspect.AspectFill,
        //        VerticalOptions = LayoutOptions.End,
        //        IsAnimationPlaying = true,
        //        WidthRequest = 46,
        //        HeightRequest = 1200,
        //        TranslationY = Constants.TubeImageOffset,
        //    };
        //    innerGrid.Children.Add(image);
        //    rippleLayoutElement.Children.Add(innerGrid);

        //    return (innerGrid, image);
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
        //[RelayCommand]
        //void TestMethoda()
        //{
        //    var qwer = UIEnabled;
        //}
        //[RelayCommand]
        //void MoveGifUp()
        //{
        //    TubesItemsSource[1].YPos -= 10;
        //}
        //[RelayCommand]
        //void MoveGifDown()
        //{
        //    TubesItemsSource[1].YPos += 10;
        //}
        //[RelayCommand]
        //void MoveGifLeft()
        //{
        //    TubesItemsSource[1].XPos -= 10;
        //}
        //[RelayCommand]
        //void MoveGifRight()
        //{
        //    TubesItemsSource[1].XPos += 10;
        //}
        #endregion
    }
}
