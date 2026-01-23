using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics;
using System.Windows.Input;

namespace WaterSortPuzzle.Views;

public partial class CoachMarkOverlay : AbsoluteLayout, IDrawable
{
    RectF _targetRect;

    public static readonly BindableProperty TappedCommandProperty =
    BindableProperty.Create(
        nameof(TappedCommand),
        typeof(ICommand),
        typeof(CoachMarkOverlay));

    public ICommand? TappedCommand
    {
        get => (ICommand?)GetValue(TappedCommandProperty);
        set => SetValue(TappedCommandProperty, value);
    }

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
    public void Show(VisualElement target, string text)
    {
        HintLabel.WidthRequest = -1;
        HintLabel.Text = text;
        Dispatcher.Dispatch(() =>
        {

            // Get target position on screen
            Rect targetBounds = target.GetBoundsOnScreen();
            _targetRect = new RectF(
                (float)targetBounds.X,
                (float)targetBounds.Y,
                (float)targetBounds.Width,
                (float)targetBounds.Height);

            //HintFrame.Measure(double.PositiveInfinity, double.PositiveInfinity);
            //var hintSize = new Size(HintFrame.Width, HintFrame.Height);
            Size hintSize = HintFrame.Measure(double.PositiveInfinity, double.PositiveInfinity);

            Rect hintBounds = CalculateNewHintPosition(targetBounds);

            // Position hint below target
            AbsoluteLayout.SetLayoutBounds(
                HintFrame,
                new Rect(hintBounds.X, hintBounds.Y, hintSize.Width, hintSize.Height));

            OverlayGraphics.Invalidate();
        });
    }
    /// <summary>
    ///  Calculate new position for hint. Based on screen size and position of the target element
    /// </summary>
    /// <param name="targetBounds"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    private Rect CalculateNewHintPosition(Rect targetBounds)
    {
        int halfPadding = 6;

        //var page = App.Current!.Windows[0].Page;
        //if (page == null) throw new NullReferenceException($"{nameof(page)} is null");
        //var displayInfo = DeviceDisplay.Current.MainDisplayInfo;
        //double screenWidth = displayInfo.Width / displayInfo.Density;
        //double screenHeight = displayInfo.Height / displayInfo.Density;

        Rect hintBounds = HintFrame.GetBoundsOnScreen();

        double xPos;// = this.Width / 2 - hintBounds.Width / 2;
        double yPos;
        if (targetBounds.X + targetBounds.Width > this.Width * 3 / 5) // zarovnat doprava
        {
            xPos = this.Width - halfPadding * 3 - hintBounds.Width;
        }
        else if (targetBounds.X < this.Width * 2 / 5) // zarovnat doleva
        {
            xPos = halfPadding * 3;
        }
        else // zarovnat na stred
        {
            xPos = this.Width / 2 - hintBounds.Width / 2;
        }
        if (targetBounds.Y + targetBounds.Height + (halfPadding * 2) + hintBounds.Height <= this.Height)
        {
            yPos = targetBounds.Y + targetBounds.Height + halfPadding;
        }
        else
        {
            yPos = targetBounds.Y - hintBounds.Height - halfPadding;
        }


        //Rect newBounds = new Rect(xPos, yPos, HintFrame.Width, HintFrame.Height);
        Rect newBounds = new Rect(xPos, yPos, HintFrame.Width, HintFrame.Height);
        //Rect newBounds = new Rect(xPos, yPos, this.Width * 0.4, 100);
        // nemusim updatovat i _targetRect?
        return newBounds;
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        var holeRect = _targetRect.Inflate(new SizeF(8, 8));
        float cornerRadius = 8f;

        // Overlay with hole
        var path = new PathF();
        path.AppendRectangle(dirtyRect);
        path.AppendRoundedRectangle(holeRect, cornerRadius);

        canvas.FillColor = new Color(0, 0, 0, 0.6f);
        canvas.FillPath(path, WindingMode.EvenOdd);

        // add border
        float borderSize = 2;
        canvas.StrokeColor = Colors.White;
        canvas.StrokeSize = borderSize;

        canvas.DrawRoundedRectangle(
            holeRect.Inflate(new SizeF(-borderSize - 8, -borderSize - 8)),
            cornerRadius - borderSize);
    }

    //void RemoveFromParent()
    //{
    //    (Parent as Layout)?.Children.Remove(this);
    //}
}