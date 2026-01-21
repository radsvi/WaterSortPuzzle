using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    public partial class CoachMarkBehavior : Behavior<VisualElement>
    {
        private VisualElement? _associatedObject;
        public VisualElement? AssociatedObject => _associatedObject;

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
            _associatedObject = bindable;
            bindable.SizeChanged += OnSizeChanged;

            OnSizeChanged(bindable, EventArgs.Empty);
        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            bindable.SizeChanged -= OnSizeChanged;
            _associatedObject = null;
            base.OnDetachingFrom(bindable);
        }
        public void TriggerInitialBounds()
        {
            if (AssociatedObject != null)
                OnSizeChanged(AssociatedObject, EventArgs.Empty);
        }

        private void OnSizeChanged(object? sender, EventArgs e)
        {
            if (sender is not VisualElement element)
                return;
            if (ReportBounds == null)
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
