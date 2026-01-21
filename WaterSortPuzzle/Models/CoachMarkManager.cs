using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    public partial class CoachMarkManager : ObservableObject
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(PreviousCommand))]
        [NotifyCanExecuteChangedFor(nameof(NextCommand))]
        private int index;
        private HelpPopupVM? _vm;

        //public int Index
        //{
        //    get => index;
        //    private set { if (value == index) return; index = value; OnPropertyChanged(); OnPropertyChanged(nameof(CanNavigatePrevious)); OnPropertyChanged(nameof(CanNavigateNext)); }
        //}
        
        public void Attach(HelpPopupVM vm)
        {
            _vm = vm;

            //InitializeCoachMarks();
        }
        public void Start()
        {
            InitializeCoachMarks();

            //UpdateAvailableMarks();
            //        //RefreshAvailable();
            //        TryActivate();
            //        MoveToFirst();
        }

        public void ResetIndex()
        {
            Index = -1;
            Navigate(CoachMarkNavigation.Next);
        }

        public CoachMarkItem? Current { get; private set; }

        //public ICommand NextCommand { get; }

        private void InitializeCoachMarks()
        {
            if (_vm is null)
                throw new NullReferenceException($"{nameof(_vm)} is null");

            _vm.MainVM.CoachMarks.Add(new CoachMarkItem
            {
                Id = "StepBackButton",
                Text = "Takes you one step back. 5 uses per level",
                Position = RelativePosition.TopLeft
            });
            _vm.MainVM.CoachMarks.Add(new CoachMarkItem
            {
                Id = "AddExtraTubeButton",
                Text = "Adds extra empty flask (decreases the final score for the level).",
                Position = RelativePosition.Top
            });
            _vm.MainVM.CoachMarks.Add(new CoachMarkItem
            {
                Id = "RestartButton",
                Text = "Restarts the level",
                Position = RelativePosition.BottomLeft
            });
            _vm.MainVM.CoachMarks.Add(new CoachMarkItem
            {
                Id = "RestartButton2",
                Text = "Restarts the level",
                Position = RelativePosition.BottomLeft
            });
            _vm.MainVM.CoachMarks.Add(new CoachMarkItem
            {
                Id = "NextLevelButton",
                Text = "Generates new level",
                Position = RelativePosition.Bottom
            });
            _vm.MainVM.CoachMarks.Add(new CoachMarkItem
            {
                Id = "AutoSolveNextStepButton",
                Text = "Next step to check automatically generated solution",
                Position = RelativePosition.TopRight
            });

            MessagingCenter.Subscribe<CoachMarkBehavior, (string Id, Rect Bounds)>(
                this,
                "CoachMarkBounds",
                (_, data) =>
                {
                    var mark = _vm.MainVM.CoachMarks.FirstOrDefault(x => x.Id == data.Id);
                    if (mark != null)
                        mark.SourceBounds = data.Bounds;

                    TryActivate();
                });

            //NextCommand = new Command(Next);
        }
        private void TryActivate()
        {
            if (_vm is null)
                throw new NullReferenceException($"{nameof(_vm)} is null");

            if (_vm.MainVM.CoachMarks.Any(x => x.IsVisible))
                return;

            var firstAvailable = _vm.MainVM.CoachMarks.FirstOrDefault(x => x.IsAvailable);
            if (firstAvailable == null)
                return;

            HideAll();
            firstAvailable.IsVisible = true;

            //if (Current == null &&
            //    CoachMarks.All(x => x.SourceBounds.HasValue))
            //{
            //    Current = CoachMarks[0];
            //    OnPropertyChanged(nameof(Current));
            //}
        }
        private void HideAll()
        {
            if (_vm is null)
                throw new NullReferenceException($"{nameof(_vm)} is null");

            foreach (var mark in _vm.MainVM.CoachMarks)
                mark.IsVisible = false;
        }
        partial void OnIndexChanged(int value) // hooked automatically by MVVM toolkit
        {
            NextCommand.NotifyCanExecuteChanged();
            PreviousCommand.NotifyCanExecuteChanged();
        }
        //private bool CanNavigate(CoachMarkNavigation direction)
        //{
        //    int newIndex = Index + (int)direction;
        //    var length = CoachMarks.Where(n => n.IsAvailable).Count();
        //    return newIndex >= 0 && newIndex <= length;
        //}
        private bool CanNavigatePrevious => Index + (int)CoachMarkNavigation.Previous >= 0;

        [RelayCommand(CanExecute = nameof(CanNavigatePrevious))]
        private void Previous() => Navigate(CoachMarkNavigation.Previous);
        private bool CanNavigateNext
        {
            get
            {
                if (_vm is null)
                    throw new NullReferenceException($"{nameof(_vm)} is null");

                int newIndex = Index + (int)CoachMarkNavigation.Next;
                var length = _vm.MainVM.CoachMarks.Where(n => n.IsAvailable).Count();
                return newIndex < length;
            }
        }

        [RelayCommand(CanExecute = nameof(CanNavigateNext))]
        private void Next() => Navigate(CoachMarkNavigation.Next);
        public void Navigate(CoachMarkNavigation direction)
        {
            if (_vm is null)
                throw new NullReferenceException($"{nameof(_vm)} is null");

            int delta = (int)direction;
            if (Index + delta < _vm.MainVM.CoachMarks.Count && Index + delta >= 0)
            {
                Index += delta;

                if (_vm.MainVM.CoachMarks[Index].IsAvailable == false)
                {
                    Navigate(direction);
                    return;
                }
                else
                {
                    Current = _vm.MainVM.CoachMarks[Index];
                }
            }
            else
            {
                //Current = CoachMarks[0];
                //_index = 0;

                //Current = null;
                return;
            }

            if (Current != null && Current.TargetBounds == null && Current.SourceBounds is not null)
            {
                Current.TargetBounds = ShiftPosition((Rect)Current.SourceBounds, Current.Position);
            }

            OnPropertyChanged(nameof(Current));
        }
        //[RelayCommand]
        //void Next()
        //{
        //    var ordered = CoachMarks
        //        .Where(x => x.IsAvailable)
        //        .ToList();

        //    var index = ordered.FindIndex(x => x.IsVisible);
        //    if (index == -1)
        //        return;

        //    ordered[index].IsVisible = false;

        //    if (index + 1 < ordered.Count)
        //        ordered[index + 1].IsVisible = true;
        //}
        private static Rect ShiftPosition(Rect source, RelativePosition position)
        {
            const int markWidth = 200;
            const int markHeight = 100;

            Rect rect;
            if (position == RelativePosition.Bottom)
            {
                rect = new Rect(
                    (source.X + (source.Width / 2) - (markWidth / 2)),
                    source.Y + source.Height,
                    markWidth,
                    markHeight);
            }
            else if (position == RelativePosition.BottomLeft)
            {
                rect = new Rect(
                    (source.X + source.Width - markWidth),
                    source.Y + source.Height,
                    markWidth,
                    markHeight);
            }
            else if (position == RelativePosition.BottomRight)
            {
                rect = new Rect(
                    source.X,
                    source.Y + source.Height,
                    markWidth,
                    markHeight);
            }
            else if (position == RelativePosition.Top)
            {
                rect = new Rect(
                    (source.X + (source.Width / 2) - (markWidth / 2)),
                    source.Y - markHeight,
                    markWidth,
                    markHeight);
            }
            else if (position == RelativePosition.TopLeft)
            {
                rect = new Rect(
                    (source.X + source.Width - markWidth),
                    source.Y - markHeight,
                    markWidth,
                    markHeight);
            }
            else if (position == RelativePosition.TopRight)
            {
                rect = new Rect(
                    source.X,
                    source.Y - markHeight,
                    markWidth,
                    markHeight);
            }
            else
            {
                rect = new Rect(
                    source.X,
                    source.Y,
                    markWidth,
                    markHeight);
            }

            return rect;
        }
    }
    //public partial class CoachMarkManager : ObservableObject
    //{
    //    [ObservableProperty]
    //    [NotifyCanExecuteChangedFor(nameof(PreviousCommand))]
    //    [NotifyCanExecuteChangedFor(nameof(NextCommand))]
    //    private int index;
    //    //public int Index
    //    //{
    //    //    get => index;
    //    //    private set { if (value == index) return; index = value; OnPropertyChanged(); OnPropertyChanged(nameof(CanNavigatePrevious)); OnPropertyChanged(nameof(CanNavigateNext)); }
    //    //}
    //    private HelpPopupVM? _vm;
    //    //public CoachMarkManager()
    //    //{
    //    //    //InitializeCoachMarks();
    //    //    RefreshAvailable();
    //    //    MoveToFirst();
    //    //}
    //    public void Attach(HelpPopupVM vm)
    //    {
    //        _vm = vm;
    //    }
    //    //private void RefreshAvailable()
    //    //{
    //    //    _vm!.AvailableCoachMarks.Clear();

    //    //    foreach (var mark in _vm.CoachMarks.Where(m => m.IsAvailable))
    //    //        _vm.AvailableCoachMarks.Add(mark);
    //    //}
    //    public void MoveToFirst()
    //    {
    //        Index = -1;
    //        NavigateTo(CoachMarkNavigation.Next);
    //    }
    //    //public void MoveToFirst()
    //    //{
    //    //    Index = 0;
    //    //    _vm!.Current = _vm.CoachMarks.FirstOrDefault(x => x.IsAvailable);
    //    //}
    //    public void Start()
    //    {
    //        //UpdateAvailableMarks();
    //        InitializeCoachMarks();
    //        //RefreshAvailable();
    //        TryActivate();
    //        MoveToFirst();
    //    }
    //    //public void UpdateAvailableMarks()
    //    //{
    //    //    //AvailableCoachMarks = (ObservableCollection<CoachMarkItem>)CoachMarks.Where(m => m.IsAvailable);
    //    //    AvailableCoachMarks.Clear();
    //    //    foreach (var mark in CoachMarks.Where(m => m.IsAvailable))
    //    //    {
    //    //        AvailableCoachMarks.Add(mark);
    //    //    }
    //    //}
    //    private void InitializeCoachMarks()
    //    {
    //        if (_vm is null)
    //            throw new NullReferenceException($"{nameof(_vm)} is null");

    //        _vm.CoachMarks.Add(new CoachMarkItem
    //        {
    //            Id = "StepBackButton",
    //            Text = "One step back. 5 uses per level",
    //            Position = RelativePosition.TopLeft
    //        });
    //        _vm.CoachMarks.Add(new CoachMarkItem
    //        {
    //            Id = "AddExtraTubeButton",
    //            Text = "Adds extra empty flask (decreases the final score for the level).",
    //            Position = RelativePosition.Top
    //        });
    //        _vm.CoachMarks.Add(new CoachMarkItem
    //        {
    //            Id = "RestartButton",
    //            Text = "Restarts the level",
    //            Position = RelativePosition.BottomLeft
    //        });
    //        _vm.CoachMarks.Add(new CoachMarkItem
    //        {
    //            Id = "RestartButton2",
    //            Text = "Restarts the level",
    //            Position = RelativePosition.BottomLeft
    //        });
    //        _vm.CoachMarks.Add(new CoachMarkItem
    //        {
    //            Id = "NextLevelButton",
    //            Text = "Generates new level",
    //            Position = RelativePosition.Bottom
    //        });
    //        _vm.CoachMarks.Add(new CoachMarkItem
    //        {
    //            Id = "AutoSolveNextStepButton",
    //            Text = "Next step to check automatically generated solution",
    //            Position = RelativePosition.TopRight
    //        });

    //        MessagingCenter.Subscribe<CoachMarkBehavior, (string Id, Rect Bounds)>(
    //            this,
    //            "CoachMarkBounds",
    //            (_, data) =>
    //            {
    //                var mark = _vm.CoachMarks.FirstOrDefault(x => x.Id == data.Id);
    //                if (mark != null)
    //                    mark.SourceBounds = data.Bounds;

    //                TryActivate();
    //            });

    //        //NextCommand = new Command(Next);
    //    }
    //    private void TryActivate()
    //    {
    //        if (_vm == null)
    //            return;

    //        if (_vm.Current != null)
    //            return;

    //        //if (_vm.CoachMarks.Any(x => x.IsVisible))
    //        //    return;

    //        var firstAvailable = _vm.CoachMarks.FirstOrDefault(x => x.IsAvailable);
    //        if (firstAvailable == null)
    //            return;

    //        HideAll();
    //        firstAvailable.IsVisible = true;
    //        _vm.Current = firstAvailable;

    //        //if (Current == null &&
    //        //    CoachMarks.All(x => x.SourceBounds.HasValue))
    //        //{
    //        //    Current = CoachMarks[0];
    //        //    OnPropertyChanged(nameof(Current));
    //        //}
    //    }
    //    private void HideAll()
    //    {
    //        if (_vm == null)
    //            throw new NullReferenceException($"{nameof(_vm)} is null");

    //        foreach (var mark in _vm.CoachMarks)
    //            mark.IsVisible = false;
    //    }
    //    partial void OnIndexChanged(int value) // hooked automatically by MVVM toolkit
    //    {
    //        NextCommand.NotifyCanExecuteChanged();
    //        PreviousCommand.NotifyCanExecuteChanged();
    //    }
    //    private bool CanNavigate(CoachMarkNavigation direction)
    //    {
    //        int newIndex = Index + (int)direction;
    //        if (_vm is null)
    //            return false;

    //        var length = _vm.CoachMarks.Where(n => n.IsAvailable).Count();
    //        return newIndex >= 0 && newIndex < length;
    //    }
    //    private bool CanNavigatePrevious => CanNavigate(CoachMarkNavigation.Previous);
    //    [RelayCommand(CanExecute = nameof(CanNavigatePrevious))]
    //    private void Previous() => NavigateTo(CoachMarkNavigation.Previous);
    //    private bool CanNavigateNext => CanNavigate(CoachMarkNavigation.Next);

    //    [RelayCommand(CanExecute = nameof(CanNavigateNext))]
    //    private void Next() => NavigateTo(CoachMarkNavigation.Next);
    //    //private void NavigateTo(CoachMarkNavigation direction)
    //    //{
    //    //    if (_vm!.AvailableCoachMarks.Count == 0)
    //    //        return;

    //    //    if (_vm.Current is null)
    //    //        return;

    //    //    int delta = (int)direction;

    //    //    var index = _vm.AvailableCoachMarks.IndexOf(_vm.Current);
    //    //    index = Math.Clamp(index + delta, 0, _vm.AvailableCoachMarks.Count - 1);
    //    //    _vm.Current = _vm.AvailableCoachMarks[index];
    //    //}
    //    public void NavigateTo(CoachMarkNavigation direction)
    //    {
    //        if (_vm == null)
    //            throw new NullReferenceException($"{nameof(_vm)} is null");

    //        int delta = (int)direction;
    //        if (Index + delta < _vm.CoachMarks.Count && Index + delta >= 0)
    //        {
    //            Index += delta;

    //            if (_vm.CoachMarks[Index].IsAvailable == false)
    //            {
    //                NavigateTo(direction);
    //                return;
    //            }
    //            else
    //            {
    //                _vm.Current = _vm.CoachMarks[Index];
    //            }
    //        }
    //        else
    //        {
    //            //Current = CoachMarks[0];
    //            //_index = 0;

    //            //Current = null;
    //            return;
    //        }

    //        if (_vm.Current != null && _vm.Current.TargetBounds == null && _vm.Current.SourceBounds is not null)
    //        {
    //            _vm.Current.TargetBounds = ShiftPosition((Rect)_vm.Current.SourceBounds, _vm.Current.Position);
    //        }

    //        OnPropertyChanged(nameof(_vm.Current));
    //    }
    //    //[RelayCommand]
    //    //void Next()
    //    //{
    //    //    var ordered = CoachMarks
    //    //        .Where(x => x.IsAvailable)
    //    //        .ToList();

    //    //    var index = ordered.FindIndex(x => x.IsVisible);
    //    //    if (index == -1)
    //    //        return;

    //    //    ordered[index].IsVisible = false;

    //    //    if (index + 1 < ordered.Count)
    //    //        ordered[index + 1].IsVisible = true;
    //    //}
    //    private static Rect ShiftPosition(Rect source, RelativePosition position)
    //    {
    //        const int markWidth = 200;
    //        const int markHeight = 100;

    //        Rect rect;
    //        if (position == RelativePosition.Bottom)
    //        {
    //            rect = new Rect(
    //                (source.X + (source.Width / 2) - (markWidth / 2)),
    //                source.Y + source.Height,
    //                markWidth,
    //                markHeight);
    //        }
    //        else if (position == RelativePosition.BottomLeft)
    //        {
    //            rect = new Rect(
    //                (source.X + source.Width - markWidth),
    //                source.Y + source.Height,
    //                markWidth,
    //                markHeight);
    //        }
    //        else if (position == RelativePosition.BottomRight)
    //        {
    //            rect = new Rect(
    //                source.X,
    //                source.Y + source.Height,
    //                markWidth,
    //                markHeight);
    //        }
    //        else if (position == RelativePosition.Top)
    //        {
    //            rect = new Rect(
    //                (source.X + (source.Width / 2) - (markWidth / 2)),
    //                source.Y - markHeight,
    //                markWidth,
    //                markHeight);
    //        }
    //        else if (position == RelativePosition.TopLeft)
    //        {
    //            rect = new Rect(
    //                (source.X + source.Width - markWidth),
    //                source.Y - markHeight,
    //                markWidth,
    //                markHeight);
    //        }
    //        else if (position == RelativePosition.TopRight)
    //        {
    //            rect = new Rect(
    //                source.X,
    //                source.Y - markHeight,
    //                markWidth,
    //                markHeight);
    //        }
    //        else
    //        {
    //            rect = new Rect(
    //                source.X,
    //                source.Y,
    //                markWidth,
    //                markHeight);
    //        }

    //        return rect;
    //    }
    //}
}
