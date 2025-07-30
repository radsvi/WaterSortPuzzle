using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Behaviors
{
    public class TranslateOnBoolChangedBehavior : Behavior<View>
    {
        public bool Trigger
        {
            get => (bool)GetValue(TriggerProperty);
            set => SetValue(TriggerProperty, value);
        }
        public static readonly BindableProperty TriggerProperty =
            BindableProperty.Create(nameof(Trigger), typeof(bool), typeof(TranslateOnBoolChangedBehavior), false, propertyChanged: OnTriggerChanged);


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


        public bool AdditionalAnimation
        {
            get { return (bool)GetValue(AdditionalAnimationProperty); }
            set { SetValue(AdditionalAnimationProperty, value); }
        }
        public static readonly BindableProperty AdditionalAnimationProperty =
            BindableProperty.Create(nameof(AdditionalAnimation), typeof(bool), typeof(TranslateOnBoolChangedBehavior), false);


        public bool DelayedAction
        {
            get { return (bool)GetValue(DelayedActionProperty); }
            set { SetValue(DelayedActionProperty, value); }
        }
        public static readonly BindableProperty DelayedActionProperty =
            BindableProperty.Create(nameof(DelayedAction), typeof(bool), typeof(TranslateOnBoolChangedBehavior), false);


        public TubeData? TubeData
        {
            get { return (TubeData)GetValue(TubeDataProperty); }
            set { SetValue(TubeDataProperty, value); }
        }
        public static readonly BindableProperty TubeDataProperty =
            BindableProperty.Create(nameof(TubeData), typeof(TubeData), typeof(TranslateOnBoolChangedBehavior), null);








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
                    else
                    {
                        if (behavior.AdditionalAnimation == false)
                        {
                            await behavior.associatedView.TranslateTo(0, 0, behavior.Duration);
                        }
                    }
                }
                else if (behavior.AnimationType == Enums.AnimationType.RippleEffect && behavior.Trigger == true)
                {
                    behavior.associatedView.IsVisible = true;
                    //behavior.DelayedAction = true;
                    double cellHeight = 39;
                    double yOffset = -cellHeight;

                    //if (behavior.TubeData.RippleGridRowSpan > 1)
                    //{
                    //    if (behavior.associatedView is StackLayout stackLayout)
                    //    {
                    //        if (stackLayout.Children.Count > 1)
                    //            throw new InvalidOperationException();

                    //        if (stackLayout.Children[0] is Image image)
                    //        {
                    //            //await image.TranslateTo(0, -1000, behavior.Duration * 4);
                    //            image.TranslationY = cellHeight * -behavior.TubeData.RippleGridRowSpan;
                    //        }
                    //    }
                    //}
                    var image = GetImageElement(behavior);
                    if (image is not null)
                    {
                        yOffset = -cellHeight * behavior.TubeData!.RippleGridRowSpan;
                        image.TranslationY = Constants.RippleEffectOffset - cellHeight - yOffset;
                    }

                    await behavior.associatedView.TranslateTo(0, yOffset, behavior.Duration *2);

                    behavior.associatedView.IsVisible = false;
                    behavior.associatedView.TranslationY = 0;
                    //behavior.AnimationType = AnimationType.None;
                    behavior.Trigger = false;

                    if (image is not null)
                        image.TranslationY = Constants.RippleEffectOffset;
                }
                else if (behavior.AnimationType == Enums.AnimationType.RepositionTube && behavior.Trigger == true)
                {
                    behavior.TubeData!.IsBusy = true;
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
                        innerElement.RotateTo(-rotateDegree, movementDuration),
                        innerElement.ScaleYTo(0.5, movementDuration),
                        innerElement.TranslateTo(0, 13, movementDuration /2)
                    );
                    
                    behavior.DelayedAction = true;
                    await Task.Delay((int)behavior.Duration);
                    behavior.DelayedAction = false;

                    //behavior.associatedView.IsVisible = false;

                    //behavior.associatedView.TranslationX = 0;
                    //behavior.associatedView.TranslationY = 0;
                    //behavior.associatedView.Rotation = 0;
                    //behavior.associatedView.ZIndex = 0;
                    //innerElement.Rotation = 0;
                    //innerElement.ScaleY = 1;
                    //innerElement.TranslationY = 0;

                    uint moveBackDuration = movementDuration / 2;
                    await Task.WhenAll(
                        behavior.associatedView.TranslateTo(0, 0, moveBackDuration),
                        behavior.associatedView.RotateTo(0, moveBackDuration),
                        innerElement.RotateTo(0, moveBackDuration),
                        innerElement.ScaleYTo(1, moveBackDuration),
                        innerElement.TranslateTo(0, 0, moveBackDuration / 2)
                    );

                    behavior.Trigger = false;
                    behavior.TubeData!.IsBusy = false;
                    //behavior.AnimationType = AnimationType.None;
                }
            }
        }
        private static Grid? GetInnerElement(Grid grid)
        {
            foreach (View child in grid.Children.Cast<View>())
            {
                if (child is Border border && child.StyleId == Constants.InnerBorderElementName)
                {
                    if (border.Content is Grid middleGrid)
                    {
                        foreach (View middleChild in middleGrid.Cast<View>())
                        {
                            if (middleChild is Grid innerGrid && middleChild.StyleId == Constants.RippleElementName)
                            {
                                return innerGrid;
                            }
                        }
                    }
                }
            }
            throw new NullReferenceException();
        }
        private static Image? GetImageElement(TranslateOnBoolChangedBehavior behavior)
        {
            if (behavior.TubeData?.RippleGridRowSpan > 1)
            {
                if (behavior.associatedView is StackLayout stackLayout)
                {
                    if (stackLayout.Children.Count > 1)
                        throw new InvalidOperationException();

                    if (stackLayout.Children[0] is Image image)
                        return image;
                }
            }
            return null;
        }
    }
}
