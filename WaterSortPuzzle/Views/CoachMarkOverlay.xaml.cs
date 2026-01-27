using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics;
using System.Windows.Input;

namespace WaterSortPuzzle.Views;

public partial class CoachMarkOverlay : AbsoluteLayout, IDrawable
{
    RectF _targetRect;

    public ICommand? TappedCommand
    {
        get => (ICommand?)GetValue(TappedCommandProperty);
        set => SetValue(TappedCommandProperty, value);
    }
    public static readonly BindableProperty TappedCommandProperty =
        BindableProperty.Create(
            nameof(TappedCommand),
            typeof(ICommand),
            typeof(CoachMarkOverlay));


    public bool ForceVisibilityTo
    {
        get { return (bool)GetValue(ForceVisibilityToProperty); }
        set { SetValue(ForceVisibilityToProperty, value); }
    }

    public static readonly BindableProperty ForceVisibilityToProperty =
        BindableProperty.Create(nameof(ForceVisibilityTo), typeof(bool), typeof(CoachMarkOverlay), false);



    public CoachMarkOverlay()
    {
        InitializeComponent();
        OverlayGraphics.Drawable = this;

        var tap = new TapGestureRecognizer();
        tap.Tapped += OnTapped;
        GestureRecognizers.Add(tap);

    }
    void OnTapped(object? sender, TappedEventArgs e)
    {
        TappedCommand?.Execute(null);
    }
    public void Show(VisualElement target, string text, bool centered)
    {
        HintLabel.WidthRequest = -1;
        HintLabel.Text = text;
        Dispatcher.Dispatch(() =>
        {

            // Get target position on screen
            int padding = (centered) ? 20 : 0; // x:Name="RootGrid" top padding
            Rect targetBounds = target.GetBoundsOnScreen();
            _targetRect = new RectF(
                (float)targetBounds.X - padding,
                (float)targetBounds.Y,
                (float)targetBounds.Width + padding * 2,
                (float)targetBounds.Height + padding);

            //HintFrame.Measure(double.PositiveInfinity, double.PositiveInfinity);
            //var hintSize = new Size(HintFrame.Width, HintFrame.Height);
            Size hintSize = HintFrame.Measure(double.PositiveInfinity, double.PositiveInfinity);

            Rect hintBounds = CalculateNewHintPosition(targetBounds, hintSize, centered);

            // Position hint below target
            AbsoluteLayout.SetLayoutBounds(
                HintFrame,
                new Rect(hintBounds.X, hintBounds.Y, hintSize.Width, hintSize.Height));

            IsVisible = ForceVisibilityTo;
            OverlayGraphics.Invalidate();
        });
    }
    //public void ShowStaticHint(string text)
    //{
    //    HintLabel.WidthRequest = -1;
    //    HintLabel.Text = text;
    //    Dispatcher.Dispatch(() =>
    //    {
    //        var displayInfo = DeviceDisplay.Current.MainDisplayInfo;
    //        double screenWidth = displayInfo.Width / displayInfo.Density;
    //        //double screenHeight = displayInfo.Height / displayInfo.Density;

    //        // tady bych mel najit kde se zobrazila prvni flaska a kde posledni a vypocitat podle toho Rect()
    //        Rect targetBounds = target.GetBoundsOnScreen();


    //        _targetRect = new RectF(150, 150, (float)(screenWidth * 0.8f), 200);

    //        //HintFrame.Measure(double.PositiveInfinity, double.PositiveInfinity);
    //        //var hintSize = new Size(HintFrame.Width, HintFrame.Height);
    //        Size hintSize = HintFrame.Measure(double.PositiveInfinity, double.PositiveInfinity);

    //        Rect hintBounds = CalculateNewHintPosition(targetBounds, hintSize);

    //        // Position hint below target
    //        AbsoluteLayout.SetLayoutBounds(
    //            HintFrame,
    //            new Rect(hintBounds.X, hintBounds.Y, hintSize.Width, hintSize.Height));

    //        IsVisible = ForceVisibilityTo;
    //        OverlayGraphics.Invalidate();
    //    });
    //}
    /// <summary>
    ///  Calculate new position for hint. Based on screen size and position of the target element
    /// </summary>
    /// <param name="targetBounds"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    private Rect CalculateNewHintPosition(Rect targetBounds, Size hintSize, bool centered)
    {
        int halfPadding = 6;

        //var page = App.Current!.Windows[0].Page;
        //if (page == null) throw new NullReferenceException($"{nameof(page)} is null");
        //var displayInfo = DeviceDisplay.Current.MainDisplayInfo;
        //double screenWidth = displayInfo.Width / displayInfo.Density;
        //double screenHeight = displayInfo.Height / displayInfo.Density;

        //Rect hintBounds = HintFrame.GetBoundsOnScreen();

        double xPos;// = this.Width / 2 - hintBounds.Width / 2;
        double yPos;
        
        if (!centered && targetBounds.X + targetBounds.Width > this.Width * 3 / 5) // zarovnat doprava
        {
            xPos = this.Width - halfPadding * 3 - hintSize.Width;
        }
        else if (!centered && targetBounds.X < this.Width * 2 / 5) // zarovnat doleva
        {
            xPos = halfPadding * 3;
        }
        else // zarovnat na stred
        {
            xPos = this.Width / 2 - hintSize.Width / 2;
        }
        if (targetBounds.Y + targetBounds.Height + (halfPadding * 2) + hintSize.Height <= this.Height)
        {
            yPos = targetBounds.Y + targetBounds.Height + halfPadding;
        }
        else
        {
            yPos = targetBounds.Y - hintSize.Height - halfPadding;
        }


        //Rect newBounds = new Rect(xPos, yPos, HintFrame.Width, HintFrame.Height);
        //Rect newBounds = new Rect(xPos, yPos, this.Width * 0.4, 100);
        Rect newBounds = new Rect(xPos, yPos, hintSize.Width, hintSize.Height);

        // nemusim updatovat i _targetRect?
        return newBounds;
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        float borderSize = 2;

        var holeRect = _targetRect.Inflate(new SizeF(borderSize, borderSize));
        float cornerRadius = 8f;

        // Overlay with hole
        var path = new PathF();
        path.AppendRectangle(dirtyRect);
        path.AppendRoundedRectangle(holeRect, cornerRadius);

        canvas.FillColor = new Color(0, 0, 0, 0.6f);
        canvas.FillPath(path, WindingMode.EvenOdd);

        // add border
        canvas.StrokeColor = Colors.White;
        canvas.StrokeSize = borderSize;

        canvas.DrawRoundedRectangle(
            holeRect.Inflate(new SizeF(-borderSize, -borderSize)),
            cornerRadius - borderSize);
    }

    //void RemoveFromParent()
    //{
    //    (Parent as Layout)?.Children.Remove(this);
    //}
}