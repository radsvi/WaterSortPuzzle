using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    public partial class CoachMarkManager : ObservableObject
    {
        private HelpPopupVM? _vm;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(NextCommand))]
        [NotifyCanExecuteChangedFor(nameof(PreviousCommand))]
        private int index = -1;

        public void Attach(HelpPopupVM vm)
        {
            _vm = vm;
        }

        public void Start()
        {
            if (_vm == null)
                return;

            InitializeCoachMarks();
            ActivateFirst();
        }

        private void InitializeCoachMarks()
        {
            _vm!.CoachMarks.Clear();

            _vm.CoachMarks.Add(new CoachMarkItem
            {
                Id = "StepBackButton",
                Text = "One step back. 5 uses per level",
                Position = RelativePosition.TopLeft
            });

            _vm.CoachMarks.Add(new CoachMarkItem
            {
                Id = "RestartButton",
                Text = "Restart the level",
                Position = RelativePosition.BottomLeft
            });

            _vm.CoachMarks.Add(new CoachMarkItem
            {
                Id = "NextLevelButton",
                Text = "Generate next level",
                Position = RelativePosition.Bottom
            });
        }

        private void ActivateFirst()
        {
            HideAll();

            var first = _vm!.CoachMarks.FirstOrDefault(x => x.IsAvailable);
            if (first == null)
                return;

            Index = _vm.CoachMarks.IndexOf(first);
            _vm.Current = first;
            first.IsVisible = true;

            //_vm.OnPropertyChanged(nameof(_vm.Current));
        }

        public void OnBoundsReported(string id, Rect bounds)
        {
            if (_vm == null)
                return;

            var mark = _vm.CoachMarks.FirstOrDefault(x => x.Id == id);
            if (mark == null)
                return;

            mark.SourceBounds = bounds;

            if (_vm.Current == mark && mark.TargetBounds == null)
            {
                mark.TargetBounds = ShiftPosition(bounds, mark.Position);
                //_vm.OnPropertyChanged(nameof(_vm.Current));
            }
        }

        private void HideAll()
        {
            foreach (var mark in _vm!.CoachMarks)
                mark.IsVisible = false;
        }

        private bool CanNavigate(int delta)
        {
            if (_vm == null)
                return false;

            int next = Index + delta;
            return next >= 0 && next < _vm.CoachMarks.Count;
        }

        [RelayCommand(CanExecute = nameof(CanNavigateNext))]
        private void Next() => Navigate(+1);

        [RelayCommand(CanExecute = nameof(CanNavigatePrevious))]
        private void Previous() => Navigate(-1);

        private bool CanNavigateNext() => CanNavigate(+1);
        private bool CanNavigatePrevious() => CanNavigate(-1);

        private void Navigate(int delta)
        {
            if (_vm == null)
                return;

            int next = Index + delta;

            while (next >= 0 && next < _vm.CoachMarks.Count)
            {
                var candidate = _vm.CoachMarks[next];
                if (candidate.IsAvailable)
                    break;

                next += delta;
            }

            if (next < 0 || next >= _vm.CoachMarks.Count)
                return;

            HideAll();

            Index = next;
            _vm.Current = _vm.CoachMarks[Index];
            _vm.Current.IsVisible = true;

            if (_vm.Current.SourceBounds != null && _vm.Current.TargetBounds == null)
            {
                _vm.Current.TargetBounds =
                    ShiftPosition(_vm.Current.SourceBounds.Value, _vm.Current.Position);
            }

            //_vm.OnPropertyChanged(nameof(_vm.Current));
        }

        //private static Rect ShiftPosition(Rect source, RelativePosition position)
        //{
        //    const int w = 200;
        //    const int h = 100;

        //    return position switch
        //    {
        //        RelativePosition.Top =>
        //            new Rect(source.Center.X - w / 2, source.Y - h, w, h),

        //        RelativePosition.Bottom =>
        //            new Rect(source.Center.X - w / 2, source.Bottom, w, h),

        //        RelativePosition.TopLeft =>
        //            new Rect(source.Right - w, source.Y - h, w, h),

        //        RelativePosition.BottomLeft =>
        //            new Rect(source.Right - w, source.Bottom, w, h),

        //        RelativePosition.TopRight =>
        //            new Rect(source.X, source.Y - h, w, h),

        //        RelativePosition.BottomRight =>
        //            new Rect(source.X, source.Bottom, w, h),

        //        _ => new Rect(source.X, source.Y, w, h)
        //    };
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
