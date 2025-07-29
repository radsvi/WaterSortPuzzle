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



        public double XTo
        {
            get { return (double)GetValue(XToProperty); }
            set { SetValue(XToProperty, value); }
        }
        public static readonly BindableProperty XToProperty =
            BindableProperty.Create(nameof(XTo), typeof(double), typeof(TranslateOnBoolChangedBehavior), 0.0);



        public double YTo
        {
            get { return (double)GetValue(YToProperty); }
            set { SetValue(YToProperty, value); }
        }
        public static readonly BindableProperty YToProperty =
            BindableProperty.Create(nameof(YTo), typeof(double), typeof(TranslateOnBoolChangedBehavior), 0.0);




        public double ActualWidth
        {
            get { return (double)GetValue(ActualWidthProperty); }
            set { SetValue(ActualWidthProperty, value); }
        }
        public static readonly BindableProperty ActualWidthProperty =
            BindableProperty.Create(nameof(ActualWidth), typeof(double), typeof(TranslateOnBoolChangedBehavior), 0.0);


        public double ActualHeight
        {
            get { return (double)GetValue(ActualHeightProperty); }
            set { SetValue(ActualHeightProperty, value); }
        }
        public static readonly BindableProperty ActualHeightProperty =
            BindableProperty.Create(nameof(ActualHeight), typeof(double), typeof(TranslateOnBoolChangedBehavior), 0.0);






        //public double XTo { get; set; } = 0;
        //public double YTo { get; set; } = 10;
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
                    //else
                    //{
                    //    await behavior.associatedView.TranslateTo(0, 0, behavior.Duration);
                    //}
                }
                else if (behavior.AnimationType == Enums.AnimationType.RippleEffect)
                {
                    behavior.associatedView.IsVisible = true;

                    await behavior.associatedView.TranslateTo(0, behavior.YTo, behavior.Duration);

                    behavior.associatedView.IsVisible = false;
                    behavior.associatedView.TranslationY = 0;
                    //behavior.AnimationType = AnimationType.None;
                }
                else if (behavior.AnimationType == Enums.AnimationType.RepositionTube)
                {
                    //behavior.associatedView.IsVisible = true;
                    //behavior.associatedView.IsVisible = false;
                    uint movementDuration = 250;
                    behavior.associatedView.ZIndex = 10;
                    double XOffset = behavior.XTo - (behavior.ActualHeight / 2) + (behavior.ActualWidth / 2); // Using Height, because we are rotating it almost 90 degrees. The second part of the equasion is so that it is in the middle of the target tube instead of at the edge
                    double YOffset = behavior.YTo - (behavior.ActualHeight / 2) - (behavior.ActualWidth / 2);

                    if (behavior.associatedView is not Grid grid)
                        return;

                    Grid? innerElement = GetInnerElement(grid);
                    if (innerElement is null)
                        return;

                    double rotateDegree = 66.0;

                    await Task.WhenAll(
                        behavior.associatedView.TranslateTo(XOffset, YOffset, movementDuration),
                        behavior.associatedView.RotateTo(rotateDegree, movementDuration),
                        innerElement.RotateTo(-rotateDegree, movementDuration)
                    );
                    await Task.Delay((int)behavior.Duration);


                    //behavior.associatedView.IsVisible = false;
                    behavior.associatedView.TranslationX = 0;
                    behavior.associatedView.TranslationY = 0;
                    behavior.associatedView.Rotation = 0;
                    behavior.associatedView.ZIndex = 0;
                    innerElement.Rotation = 0;
                    //behavior.AnimationType = AnimationType.None;
                }
            }
        }
        private static Grid? GetInnerElement(Grid grid)
        {
            foreach (View child in grid.Children.Cast<View>())
            {
                if (child is Border border && child.StyleId == "InnerBorder")
                {
                    if (border.Content?.GetType() == typeof(Grid) && border.Content.StyleId == "RippleEffectElement")
                    {
                        return (Grid)border.Content;
                    }
                }
            }
            //foreach (View child in grid.Children.Cast<View>())
            //{
            //    if (child.GetType() == typeof(Grid) && child.StyleId == "RippleEffectElement")
            //    {
            //        return (Grid)child;
            //    }
            //}
            throw new NullReferenceException();
        }
    }
}
