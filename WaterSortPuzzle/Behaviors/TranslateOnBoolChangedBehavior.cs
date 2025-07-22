using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Behaviors
{
    public class TranslateOnBoolChangedBehavior : Behavior<View>
    {
        public static readonly BindableProperty TriggerProperty =
            BindableProperty.Create(nameof(Trigger), typeof(bool), typeof(TranslateOnBoolChangedBehavior), false, propertyChanged: OnTriggerChanged);

        public bool Trigger
        {
            get => (bool)GetValue(TriggerProperty);
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
            if (bindable is TranslateOnBoolChangedBehavior behavior && behavior.associatedView != null)
            {
                if (behavior.AnimationType == Enums.AnimationType.RaiseNLower)
                {
                    if ((bool)newValue)
                    {
                        await behavior.associatedView.TranslateTo(0, behavior.YTo, behavior.Duration);
                    }
                    else
                    {
                        await behavior.associatedView.TranslateTo(0, 0, behavior.Duration);
                    }
                }
                else if (behavior.AnimationType == Enums.AnimationType.RippleEffect)
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
