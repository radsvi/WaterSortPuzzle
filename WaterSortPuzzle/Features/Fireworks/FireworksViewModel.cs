
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Features.Fireworks
{
    public partial class FireworksViewModel : ObservableObject
    {
        public FireworkSystem System { get; }

        private readonly Stopwatch _timer = Stopwatch.StartNew();
        private double _lastTime;

        public double PageWidth { get; set; }
        public double PageHeight { get; set; }
        public FireworksViewModel()
        {
            System = new(this);
        }
        public void Update()
        {
            var now = _timer.Elapsed.TotalSeconds;
            var delta = (float)(now - _lastTime);
            _lastTime = now;

            System.Update(delta);
        }

        public void LaunchCommand(SKPoint point)
        {
            //System.Launch(point);
            System.Explode(point);
        }

        [RelayCommand]
        public async Task StartAnimation()
        {
            ////System.Launch(new SKPoint(150, 150));
            ////System.LaunchRocket(new SKPoint(250, 250));

            //var displayInfo = DeviceDisplay.Current.MainDisplayInfo;
            ////double screenWidth = (displayInfo.Width / displayInfo.Density);
            ////double screenHeight = (displayInfo.Height / displayInfo.Density);

            //float canvasWidthPx = (float)(PageWidth * displayInfo.Density);
            ////float canvasHeightPx = (float)(PageHeight * displayInfo.Density);

            ////var point = new SKPoint((float)(PageWidth / 2), (float)(PageHeight * 2 / 3));
            //var point = new SKPoint((float)(canvasWidthPx / 2), (float)(PageHeight * 2 / 3));
            //System.LaunchRocket(point);

            var displayInfo = DeviceDisplay.Current.MainDisplayInfo;
            float canvasWidthPx = (float)(PageWidth * displayInfo.Density);
            //await System.LaunchFireworks(canvasWidthPx, (float)PageHeight);
            await System.ExplodeConfetti(canvasWidthPx, (float)PageHeight);
        }
    }
}
