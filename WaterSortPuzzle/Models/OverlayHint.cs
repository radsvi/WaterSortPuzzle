using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    public sealed class OverlayHint
    {
        public string Text { get; init; } = string.Empty;
        public VisualElement Target { get; init; } = null!;
        public Point Offset { get; init; } = new(0, 0);
    }
}
