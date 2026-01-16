using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    public sealed partial class CoachMarkItem : ObservableObject
    {
        public Func<bool>? Availability { get; init; }
        private Rect? sourceBounds;

        public string Id { get; init; } = string.Empty;
        public string Text { get; init; } = string.Empty;
        public Rect? SourceBounds
        {
            get => sourceBounds;
            set
            {
                if (sourceBounds == value)
                    return;

                sourceBounds = value;
                //IsAvailable = true;
                OnPropertyChanged(nameof(TargetBounds));
            }
        }
        public RelativePosition Position { get; init; }
        public Rect? TargetBounds { get; set; }

        //bool _isAvailable;
        //public bool IsAvailable
        //{
        //    get => _isAvailable;
        //    private set
        //    {
        //        if (_isAvailable == value)
        //            return;

        //        _isAvailable = value;
        //        OnPropertyChanged(nameof(IsAvailable));
        //    }
        //}
        public bool IsAvailable => Availability?.Invoke() ?? true;
        bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                if (_isVisible == value)
                    return;

                _isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }
    }
}
