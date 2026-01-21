using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    public static class VisualTreeExtensions
    {
        public static IEnumerable<T> FindBehaviors<T>(this Element element)
            where T : Behavior
        {
            if (element is VisualElement ve)
            {
                foreach (var b in ve.Behaviors.OfType<T>())
                    yield return b;
            }

            foreach (var child in element.LogicalChildren)
            {
                if (child is Element e)
                {
                    foreach (var b in e.FindBehaviors<T>())
                        yield return b;
                }
            }
        }
    }
}
