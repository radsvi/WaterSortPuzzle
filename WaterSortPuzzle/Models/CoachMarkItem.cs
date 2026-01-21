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
        [ObservableProperty]
        private Rect? sourceBounds;

        public string Id { get; init; } = string.Empty;
        public string Text { get; init; } = string.Empty;
        
        public RelativePosition Position { get; init; }
        [ObservableProperty]
        public Rect? targetBounds;

        public bool IsAvailable => Availability?.Invoke() ?? true;
        [ObservableProperty]
        bool _isVisible;

        public string XToText {
            get
            {
                if (TargetBounds is null)
                    return string.Empty;

                string text = string.Empty;
                text += $"X: {TargetBounds.Value.X}, ";
                text += $"Y: {TargetBounds.Value.Y}, ";
                text += $"Width: {TargetBounds.Value.Width}, ";
                text += $"Height: {TargetBounds.Value.Height}, ";

                return text;
            }
        }
    }
}
