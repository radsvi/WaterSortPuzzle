using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Features.Fireworks
{
    public class FireworksView : SKCanvasView
    {
        private readonly FireworksViewModel _vm = new();
        IDispatcherTimer? _timer;
        public FireworksView(FireworksViewModel vm)
        {
            _vm = vm;

            //
            //EnableTouchEvents = true;
            //

            Touch += OnTouch;
            PaintSurface += OnPaintSurface;
            Loaded += OnAppearing;


            _timer = Dispatcher.CreateTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(16); // ~60 FPS
            _timer.Tick += (s, e) =>
            {
                _vm.Update();
                InvalidateSurface();
            };
            _timer.Start();
        }

        private void OnAppearing(object? sender, EventArgs e)
        {
            _vm.StartAnimation();
        }

        private void OnPaintSurface(object? sender, SKPaintSurfaceEventArgs e)
        {
            _vm.System.Render(e.Surface.Canvas, e.Info.Width, e.Info.Height);
        }
        private void OnTouch(object? sender, SKTouchEventArgs e)
        {
            if (e.ActionType == SKTouchAction.Pressed)
                _vm.LaunchCommand(e.Location);

            e.Handled = true;
        }
    }
}
