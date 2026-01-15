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
            bindable.PropertyChanged += OnPropertyChanged;
        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            bindable.SizeChanged -= OnSizeChanged;
            bindable.PropertyChanged -= OnPropertyChanged;
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
        void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == VisualElement.IsVisibleProperty.PropertyName)
                TryReport(sender as VisualElement);
        }
        void TryReport(VisualElement? element)
        {
            if (element == null)
                return;

            // Skip hidden elements
            if (!element.IsVisible)
                return;

            // Skip non-laid-out elements
            if (element.Width <= 0 || element.Height <= 0)
                return;

            var rect = GetAbsoluteBounds(element);

            MessagingCenter.Send(
                this,
                "CoachMarkAvailable",
                (Id, rect));
        }



        private Rect GetAbsoluteBounds(VisualElement element)
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

            y += 36;

            return new Rect(x, y, element.Width, element.Height);
        }
        //public Rect GetAbsoluteBounds(VisualElement element)
        //{
        //    double x = element.X;
        //    double y = element.Y;

        //    Element parent = element.Parent;

        //    while (parent is VisualElement parentElement)
        //    {
        //        // Add the parent's coordinates
        //        x += parentElement.X;
        //        y += parentElement.Y;

        //        // If the parent is a ScrollView, we must subtract the scroll offset
        //        if (parentElement is ScrollView scrollView)
        //        {
        //            x -= scrollView.ScrollX;
        //            y -= scrollView.ScrollY;
        //        }

        //        parent = parentElement.Parent;
        //    }

        //    // If you want the coordinates of the CONTENT (inside the padding):
        //    // x += element.Padding.Left;
        //    // y += element.Padding.Top;

        //    return new Rect(x, y, element.Width, element.Height);
        //}
        //private static Rect GetAbsoluteBounds(VisualElement element)
        //{
        //    if (element == null)
        //        return Rect.Zero;

        //    double x = element.X;
        //    double y = element.Y;

        //    var parent = element.Parent as VisualElement;
        //    while (parent != null)
        //    {
        //        // accumulate parent offsets
        //        x += parent.X;
        //        y += parent.Y;

        //        // add padding if parent is Layout
        //        if (parent is Layout layout)
        //        {
        //            x += layout.Padding.Left;
        //            y += layout.Padding.Top;

        //            // if StackLayout, add spacing for preceding siblings
        //            if (layout is StackLayout stack)
        //            {
        //                int index = stack.Children.IndexOf(element);
        //                if (stack.Orientation == StackOrientation.Vertical)
        //                    y += index * stack.Spacing;
        //                else
        //                    x += index * stack.Spacing;
        //            }
        //        }

        //        element = parent;
        //        parent = element.Parent as VisualElement;
        //    }

        //    return new Rect(x, y, element.Width, element.Height);
        //}
    }
}
