using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    public partial class CoachMarkManager : ObservableObject
    {
        public ObservableCollection<CoachMarkItem> CoachMarks { get; } = new();
        public ObservableCollection<CoachMarkItem> AvailableCoachMarks { get; set; }
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(PreviousCommand))]
        [NotifyCanExecuteChangedFor(nameof(NextCommand))]
        private int index;
        //public int Index
        //{
        //    get => index;
        //    private set { if (value == index) return; index = value; OnPropertyChanged(); OnPropertyChanged(nameof(CanNavigatePrevious)); OnPropertyChanged(nameof(CanNavigateNext)); }
        //}
        public CoachMarkManager()
        {
            InitializeCoachMarks();
        }
        public CoachMarkItem? Current { get; private set; }
        public void MoveToFirst()
        {
            Index = -1;
            NavigateTo(CoachMarkNavigation.Next);
        }
        public void Start()
        {
            UpdateAvailableMarks();
            MoveToFirst();
        }
        public void UpdateAvailableMarks()
        {
            AvailableCoachMarks = (ObservableCollection<CoachMarkItem>)CoachMarks.Where(m => m.IsAvailable);
        }
        private void InitializeCoachMarks()
        {
            CoachMarks.Add(new CoachMarkItem
            {
                Id = "StepBackButton",
                Text = "One step back. 5 uses per level",
                Position = RelativePosition.TopLeft
            });
            CoachMarks.Add(new CoachMarkItem
            {
                Id = "AddExtraTubeButton",
                Text = "Adds extra empty flask (decreases the final score for the level).",
                Position = RelativePosition.Top
            });
            CoachMarks.Add(new CoachMarkItem
            {
                Id = "RestartButton",
                Text = "Restarts the level",
                Position = RelativePosition.BottomLeft
            });
            CoachMarks.Add(new CoachMarkItem
            {
                Id = "RestartButton2",
                Text = "Restarts the level",
                Position = RelativePosition.BottomLeft
            });
            CoachMarks.Add(new CoachMarkItem
            {
                Id = "NextLevelButton",
                Text = "Generates new level",
                Position = RelativePosition.Bottom
            });
            CoachMarks.Add(new CoachMarkItem
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
                    var mark = CoachMarks.FirstOrDefault(x => x.Id == data.Id);
                    if (mark != null)
                        mark.SourceBounds = data.Bounds;

                    TryActivate();
                });

            //NextCommand = new Command(Next);
        }
        private void TryActivate()
        {
            if (CoachMarks.Any(x => x.IsVisible))
                return;

            var firstAvailable = CoachMarks.FirstOrDefault(x => x.IsAvailable);
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
            foreach (var mark in CoachMarks)
                mark.IsVisible = false;
        }
        partial void OnIndexChanged(int value) // hooked automatically by MVVM toolkit
        {
            NextCommand.NotifyCanExecuteChanged();
            PreviousCommand.NotifyCanExecuteChanged();
        }
        private bool CanNavigate(CoachMarkNavigation direction)
        {
            int newIndex = Index + (int)direction;
            var length = CoachMarks.Where(n => n.IsAvailable).Count();
            return newIndex >= 0 && newIndex <= length;
        }
        private bool CanNavigatePrevious => CanNavigate(CoachMarkNavigation.Previous);
        [RelayCommand(CanExecute = nameof(CanNavigatePrevious))]
        private void Previous() => NavigateTo(CoachMarkNavigation.Previous);
        private bool CanNavigateNext => CanNavigate(CoachMarkNavigation.Next);

        [RelayCommand(CanExecute = nameof(CanNavigateNext))]
        private void Next() => NavigateTo(CoachMarkNavigation.Next);
        public void NavigateTo(CoachMarkNavigation direction)
        {
            int delta = (int)direction;
            if (Index + delta < CoachMarks.Count && Index + delta >= 0)
            {
                Index += delta;

                if (CoachMarks[Index].IsAvailable == false)
                {
                    NavigateTo(direction);
                    return;
                }
                else
                {
                    Current = CoachMarks[Index];
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
}
