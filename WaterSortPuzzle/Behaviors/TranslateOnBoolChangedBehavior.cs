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
        //public uint Duration { get; set; } = 750;

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
                        await behavior.associatedView.TranslateTo(0, behavior.YTo, Constants.RaiseTubeDuration);
                    }
                    else
                    {
                        if (behavior.AdditionalAnimation == false)
                        {
                            await behavior.associatedView.TranslateTo(0, 0, Constants.RaiseTubeDuration);
                        }
                    }
                }
                else if (behavior.AnimationType == Enums.AnimationType.RippleEffect && behavior.Trigger == true)
                {
                    behavior.associatedView.IsVisible = true;

                    uint realDuration = Constants.PouringDuration * (uint)behavior.TubeData!.RippleGridRowSpan;

                    //var innerGrid = GetOutsideImageElement(behavior);
                    var innerGrid = GetChildElement<AbsoluteLayout>(behavior.associatedView, Constants.InnerGridElementName);
                    //if (innerGrid is not null)
                    //{
                    //    //yOffset = -Constants.CellHeight * behavior.TubeData!.RippleGridRowSpan;
                    //    //realDuration = Constants.PouringDuration * (uint)behavior.TubeData!.RippleGridRowSpan;

                    //    innerGrid.TranslationY = Constants.RippleEffectOffset - Constants.CellHeight - yOffset;
                    //}
                    //innerGrid.TranslationY = - Constants.CellHeight * behavior.TubeData!.RippleGridRowSpan;

                    //await innerGrid.TranslateTo(0, -Constants.CellHeight * behavior.TubeData!.RippleGridRowSpan, realDuration * 2);




                    //var animation = new Animation(v => innerGrid.HeightRequest = v, 10, Constants.CellHeight * behavior.TubeData!.RippleGridRowSpan);
                    //animation.Commit(innerGrid, "ExpandHeight", realDuration);
                    //var animation = new Animation(v => innerGrid.TranslationY = v, 0, -60);
                    //animation.Commit(innerGrid, "TranslateY", 16, 1000);
                    //await AnimateHeight(innerGrid, 60, 1000);
                    //await AnimateHeight(innerGrid, 35, 10);

                    //Rect origSize = new Rect(innerGrid.X, innerGrid.Y, innerGrid.Width, innerGrid.Height);
                    //Rect newSize = new Rect(innerGrid.X, innerGrid.Y, innerGrid.Width, 90);
                    //await innerGrid.LayoutTo(newSize, 1000, Easing.SinInOut);
                    //await innerGrid.ScaleYTo(2, 1000, Easing.SinInOut);
                    //await innerGrid.LayoutTo();
                    //innerGrid.HeightRequest = 35;


                    await AnimateHeight(innerGrid, 60, 5, 1000);
                    //await Task.Delay(1000);



                    //var animation = new Animation(v => innerGrid.Scale = v, 1, 2);
                    //animation.Commit(behavior.associatedView, "SimpleAnimation", 16, 2000, Easing.Linear, (v, c) => innerGrid.Scale = 1, () => true);




                    //innerGrid.TranslationY = 0;

                    //if (innerGrid is not null)
                    //    innerGrid.TranslationY = Constants.RippleEffectOffset;
                    //innerGrid.TranslationY = Constants.RippleEffectOffset;

                    behavior.associatedView.IsVisible = false;
                    behavior.Trigger = false;

                    behavior.TubeData.RippleGridRow = 0;
                    behavior.TubeData.RippleGridRowSpan = 1;
                }
                else if (behavior.AnimationType == Enums.AnimationType.RepositionTube && behavior.Trigger == true)
                {
                    if (behavior.associatedView is not Grid grid)
                        return;

                    Grid innerElement = GetInnerElement(grid);

                    behavior.TubeData!.IsBusy = true;
                    behavior.associatedView.InputTransparent = true;

                    //behavior.associatedView.IsVisible = true;
                    //behavior.associatedView.IsVisible = false;
                    behavior.associatedView.ZIndex = 10;
                    double XOffset = behavior.XTo - (behavior.ActualHeight / 2) + (behavior.ActualWidth / 2); // Using Height, because we are rotating it almost 90 degrees. The second part of the equasion is so that it is in the middle of the target tube instead of at the edge
                    double YOffset = behavior.YTo - (behavior.ActualHeight / 2) - (behavior.ActualWidth / 2);

                    double rotateDegree = 66.0;


                    await Task.WhenAll(
                        behavior.associatedView.TranslateTo(XOffset, YOffset, Constants.RepositionDuration),
                        behavior.associatedView.RotateTo(rotateDegree, Constants.RepositionDuration),
                        innerElement.RotateTo(-rotateDegree, Constants.RepositionDuration),
                        innerElement.ScaleYTo(0.5, Constants.RepositionDuration),
                        innerElement.TranslateTo(0, 13, Constants.RepositionDuration / 2)
                    );
                    
                    behavior.DelayedAction = true;
                    await Task.Delay((int)Constants.PouringDuration * behavior.TubeData!.NumberOfRepeatingLiquids);
                    behavior.DelayedAction = false;

                    uint moveBackDuration = Constants.RepositionDuration / 2;
                    await Task.WhenAll(
                        behavior.associatedView.TranslateTo(0, 0, moveBackDuration),
                        behavior.associatedView.RotateTo(0, moveBackDuration),
                        innerElement.RotateTo(0, moveBackDuration),
                        innerElement.ScaleYTo(1, moveBackDuration),
                        innerElement.TranslateTo(0, 0, moveBackDuration / 2)
                    );

                    behavior.associatedView.ZIndex = 0;
                    behavior.Trigger = false;
                    behavior.TubeData!.IsBusy = false;
                    behavior.associatedView.InputTransparent = false;
                }
            }
        }
        private static Grid GetInnerElement(Grid grid)
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
        private static Grid GetOutsideImageElement(TranslateOnBoolChangedBehavior behavior)
        {
            if (behavior.TubeData?.RippleGridRowSpan > 0)
            {
                if (behavior.associatedView is AbsoluteLayout stackLayout)
                {
                    if (stackLayout.Children.Count > 1)
                        throw new InvalidOperationException();

                    if (stackLayout.Children[0] is Grid element && element.StyleId == Constants.InnerGridElementName)
                        return element;
                }
            }
            throw new NullReferenceException();
        }
        private static T GetChildElement<T>(View view, string styleId) where T : View
        {
            if (view is T target && view.StyleId == styleId)
            {
                return target;
            }
            
            if (view is Layout layout)
            {
                foreach (View child in layout.Children.Cast<View>())
                {
                    GetChildElement<T>(child, styleId);
                }
            }
            
            throw new NullReferenceException();
        }
        private static Task<bool> AnimateHeight(View view, double fromHeight, double toHeight, uint duration)
        {
            var tcs = new TaskCompletionSource<bool>();

            var animation = new Animation(v => view.HeightRequest = v, fromHeight, toHeight);
            animation.Commit(view, "HeightAnim", length: duration, easing: Easing.SinInOut,
                finished: (v, c) => tcs.SetResult(true));

            return tcs.Task;
        }
    }
}
