using Microsoft.Maui.Graphics;

namespace WaterSortPuzzle.Views;

public partial class CoachMarkOverlay : AbsoluteLayout, IDrawable
{
    RectF _targetRect;

    public CoachMarkOverlay()
    {
        InitializeComponent();
        OverlayGraphics.Drawable = this;

        var tap = new TapGestureRecognizer();
        tap.Tapped += (_, _) => RemoveFromParent();
        GestureRecognizers.Add(tap);
    }

    public void Show(VisualElement target, string text)
    {
        HintLabel.Text = text;

        // Get target position on screen
        var bounds = target.GetBoundsOnScreen();
        _targetRect = new RectF(
            (float)bounds.X,
            (float)bounds.Y,
            (float)bounds.Width,
            (float)bounds.Height);

        // Position hint below target
        AbsoluteLayout.SetLayoutBounds(
            HintFrame,
            new Rect(bounds.X, bounds.Bottom + 12, 250, AbsoluteLayout.AutoSize));

        OverlayGraphics.Invalidate();
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        // Dim background
        canvas.FillColor = new Color(0, 0, 0, 0.6f);
        canvas.FillRectangle(dirtyRect);

        // Cut hole
        canvas.BlendMode = BlendMode.Clear;
        canvas.FillRoundedRectangle(_targetRect.Inflate(new SizeF(8, 8)), 8);
        canvas.BlendMode = BlendMode.Normal;
    }

    void RemoveFromParent()
    {
        (Parent as Layout)?.Children.Remove(this);
    }
}