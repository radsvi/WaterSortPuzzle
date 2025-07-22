using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Behaviors
{
    public class TranslateOnChangedBehavior : Behavior<View>
    {
        public static readonly BindableProperty TriggerProperty =
            BindableProperty.Create(nameof(Trigger), typeof(int), typeof(TranslateOnChangedBehavior), 0, propertyChanged: OnTriggerChanged);

        public int Trigger
        {
            get => (int)GetValue(TriggerProperty);
            set => SetValue(TriggerProperty, value);
        }

        //public double XTo { get; set; } = 0;
        public double YTo { get; set; } = 10;
        public uint Duration { get; set; } = 750;

        public AnimationType AnimationType { get; set; }

        private View? associatedView;

        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);
            associatedView = bindable;

            // Inherit BindingContext from the view
            BindingContext = bindable.BindingContext;

            // Handle future BindingContext changes
            bindable.BindingContextChanged += (s, e) =>
            {
                BindingContext = bindable.BindingContext;
            };
        }

        protected override void OnDetachingFrom(View bindable)
        {
            base.OnDetachingFrom(bindable);
            associatedView = null;
        }

        private static async void OnTriggerChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is TranslateOnChangedBehavior behavior && behavior.associatedView != null)
            {
                if (behavior.AnimationType == AnimationType.RippleEffect)
                {
                    behavior.associatedView.IsVisible = true;

                    await behavior.associatedView.TranslateTo(0, behavior.YTo, behavior.Duration);

                    behavior.associatedView.IsVisible = false;
                    behavior.associatedView.TranslationY = 0;
                    //behavior.AnimationType = AnimationType.None;
                }
            }
        }
    }
}
