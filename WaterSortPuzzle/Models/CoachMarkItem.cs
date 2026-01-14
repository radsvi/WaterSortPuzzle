using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    public sealed class CoachMarkItem
    {
        public string Id { get; init; } = string.Empty;
        public string Text { get; init; } = string.Empty;
        public Rect? SourceBounds { get; set; }
        public RelativePosition Position { get; init; }
        public Rect? TargetBounds { get; set; }

    }
}
