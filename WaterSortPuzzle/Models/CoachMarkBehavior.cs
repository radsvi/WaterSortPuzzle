using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    public sealed class CoachMarkBehavior : Behavior<VisualElement>
    {
        public static readonly BindableProperty IdProperty =
            BindableProperty.Create(
                nameof(Id),
                typeof(string),
                typeof(CoachMarkBehavior));

        public string Id
        {
            get => (string)GetValue(IdProperty);
            set => SetValue(IdProperty, value);
        }

        //protected override void OnAttachedTo(VisualElement bindable)
        //{
        //    bindable.Loaded += OnLoaded;
        //}

        //void OnLoaded(object? sender, EventArgs e)
        //{
        //    if (sender is not VisualElement element)
        //        return;

        //    var rect = GetAbsoluteBounds(element);

        //    MessagingCenter.Send(this, "CoachMarkBounds",
        //        (Id, rect));
        //}
        protected override void OnAttachedTo(VisualElement bindable)
        {
            bindable.SizeChanged += OnSizeChanged;
        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            bindable.SizeChanged -= OnSizeChanged;
        }
        void OnSizeChanged(object? sender, EventArgs e)
        {
            if (sender is not VisualElement element)
                return;

            if (!element.IsVisible)
                return;

            // ignore zero-size passes
            if (element.Width <= 0 || element.Height <= 0)
                return;

            var rect = GetAbsoluteBounds(element);

            MessagingCenter.Send(
                this,
                "CoachMarkBounds",
                (Id, rect));
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
