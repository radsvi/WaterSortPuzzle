using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    public partial class CoachMarkManager : ObservableObject
    {
        public MainVM MainVM { get; }

        public event EventHandler? CurrentCoachMarkChanged;
        public CoachMarkManager(MainVM mainVM)
        {
            MainVM = mainVM;

            InitializeCoachMarks();
        }

        private void InitializeCoachMarks()
        {
            MainVM.CoachMarks.Add(new CoachMarkItem
            {
                Id = "StepBackButton",
                Text = "Takes you one step back. 5 uses per level",
                Position = RelativePosition.TopLeft
            });
            MainVM.CoachMarks.Add(new CoachMarkItem
            {
                Id = "AddExtraTubeButton",
                Text = "Adds extra empty flask (decreases the final score for the level).",
                Position = RelativePosition.Top
            });
            MainVM.CoachMarks.Add(new CoachMarkItem
            {
                Id = "RestartButton",
                Text = "Restarts the level",
                Position = RelativePosition.BottomLeft
            });
            MainVM.CoachMarks.Add(new CoachMarkItem
            {
                Id = "RestartButton2",
                Text = "Restarts the level",
                Position = RelativePosition.BottomLeft
            });
            MainVM.CoachMarks.Add(new CoachMarkItem
            {
                Id = "NextLevelButton",
                Text = "Generates new level",
                Position = RelativePosition.Bottom
            });
            MainVM.CoachMarks.Add(new CoachMarkItem
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
                    var mark = MainVM.CoachMarks.FirstOrDefault(x => x.Id == data.Id);
                    if (mark != null)
                        mark.SourceBounds = data.Bounds;

                    TryActivate();
                });

            //NextCommand = new Command(Next);
        }
        private void HideAll()
        {
            foreach (var mark in MainVM.CoachMarks)
                mark.IsVisible = false;
        }
        private void TryActivate()
        {
            if (MainVM.CoachMarks.Any(x => x.IsVisible))
                return;

            var firstAvailable = MainVM.CoachMarks.FirstOrDefault(x => x.IsAvailable);
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

        public void Navigate(CoachMarkNavigation direction)
        {
            int delta = (int)direction;
            if (MainVM.Index + delta < MainVM.CoachMarks.Count && MainVM.Index + delta >= 0)
            {
                MainVM.Index += delta;

                if (MainVM.CoachMarks[MainVM.Index].IsAvailable == false)
                {
                    Navigate(direction);
                    return;
                }
                else
                {
                    MainVM.Current = MainVM.CoachMarks[MainVM.Index];
                }
            }
            else
            {
                //Current = CoachMarks[0];
                //_index = 0;

                //Current = null;
                return;
            }

            if (MainVM.Current != null && MainVM.Current.TargetBounds == null && MainVM.Current.SourceBounds is not null)
            {
                MainVM.Current.TargetBounds = ShiftPosition((Rect)MainVM.Current.SourceBounds, MainVM.Current.Position);
            }

            //OnPropertyChanged(nameof(MainVM.Current));
            CurrentCoachMarkChanged?.Invoke(this, EventArgs.Empty);
        }
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
