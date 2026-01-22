using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace WaterSortPuzzle.Behaviors
{
    public class CoachMarkBehavior : Behavior<VisualElement>
    {
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(
                nameof(Text),
                typeof(string),
                typeof(CoachMarkBehavior),
                string.Empty);

        public static readonly BindableProperty OverlayProperty =
            BindableProperty.Create(
                nameof(Overlay),
                typeof(CoachMarkOverlay),
                typeof(CoachMarkBehavior));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public CoachMarkOverlay Overlay
        {
            get => (CoachMarkOverlay)GetValue(OverlayProperty);
            set => SetValue(OverlayProperty, value);
        }

        VisualElement? _associatedObject;

        protected override void OnAttachedTo(VisualElement bindable)
        {
            base.OnAttachedTo(bindable);
            _associatedObject = bindable;

            // Wait until layout is ready
            bindable.Loaded += OnLoaded;
        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.Loaded -= OnLoaded;
            _associatedObject = null;
        }

        void OnLoaded(object? sender, EventArgs e)
        {
            if (_associatedObject == null || Overlay == null)
                return;

            // Ensure we run after layout pass
            _associatedObject.Dispatcher.Dispatch(() =>
            {
                Overlay.Show(_associatedObject, Text);
                Overlay.IsVisible = true;
            });
        }
    }
}
