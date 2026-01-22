using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    public static class VisualElementExtensions
    {
        public static Rect GetBoundsOnScreen(this VisualElement view)
        {
            var x = view.X;
            var y = view.Y;

            var parent = view.Parent as VisualElement;
            while (parent != null)
            {
                x += parent.X;
                y += parent.Y;
                parent = parent.Parent as VisualElement;
            }

            return new Rect(x, y, view.Width, view.Height);
        }
    }
}
