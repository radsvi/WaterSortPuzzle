using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    public class CoachMarkBehavior : Behavior<VisualElement>
    {
        public static readonly BindableProperty IdProperty =
            BindableProperty.Create(nameof(Id), typeof(string), typeof(CoachMarkBehavior));

        public static readonly BindableProperty ReportBoundsProperty =
            BindableProperty.Create(nameof(ReportBounds), typeof(Action<string, Rect>), typeof(CoachMarkBehavior));

        public string Id
        {
            get => (string)GetValue(IdProperty);
            set => SetValue(IdProperty, value);
        }

        public Action<string, Rect>? ReportBounds
        {
            get => (Action<string, Rect>?)GetValue(ReportBoundsProperty);
            set => SetValue(ReportBoundsProperty, value);
        }

        protected override void OnAttachedTo(VisualElement bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.SizeChanged += OnSizeChanged;
        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            bindable.SizeChanged -= OnSizeChanged;
            base.OnDetachingFrom(bindable);
        }

        private void OnSizeChanged(object? sender, EventArgs e)
        {
            if (sender is not VisualElement element || ReportBounds == null)
                return;

            var bounds = GetAbsoluteBounds(element);
            ReportBounds.Invoke(Id, bounds);
        }

        private static Rect GetAbsoluteBounds(VisualElement element)
        {
            double x = element.X;
            double y = element.Y;

            var parent = element.Parent as VisualElement;
            while (parent != null)
            {
                x += parent.X;
                y += parent.Y;
                parent = parent.Parent as VisualElement;
            }

            return new Rect(x, y, element.Width, element.Height);
        }
    }
}
