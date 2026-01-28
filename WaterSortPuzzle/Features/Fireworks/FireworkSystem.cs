using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WaterSortPuzzle.Features.Fireworks.Enums;
using WaterSortPuzzle.Features.Fireworks.Models;

namespace WaterSortPuzzle.Features.Fireworks
{
    public class FireworkSystem
    {
        private readonly List<Rocket> _rockets = new();
        private readonly List<Particle> _particles = new();
        private readonly Random _rand = new();
        private readonly FireworksViewModel _vm;

        public IReadOnlyList<Rocket> Rockets => _rockets;
        public IReadOnlyList<Particle> Particles => _particles;

        public FireworkSystem(FireworksViewModel vm)
        {
            this._vm = vm;
        }

        public void LaunchRocket(Vector2 origin)
        {
            float velocity = 1200;
            // Start near bottom of screen
            var start = new Vector2(
                origin.X + _rand.Next(-60, 60),
                velocity); // adjust to canvas height

            var rand = new Vector2(_rand.Next(-200, 200), _rand.Next(-60, 60));

            var direction = Vector2.Normalize(origin - rand - start);

            _rockets.Add(new Rocket
            {
                Position = start,
                PreviousPosition = start,
                Velocity = direction * velocity, // FAST
                Life = 0.6f,
                Color = SKColors.White
            });
        }
        public void Update(float delta)
        {
            const float gravity = 300f;

            // rockets
            for (int i = _rockets.Count - 1; i >= 0; i--)
            {
                var r = _rockets[i];

                r.Velocity.Y += gravity * delta * 0.2f; // slight arc
                r.PreviousPosition = r.Position;
                r.Position += r.Velocity * delta;
                r.Life -= delta;

                // reached explosion point
                if (r.Life <= 0)
                {
                    Explode(r.Position);
                    _rockets.RemoveAt(i);
                }
            }

            UpdateParticles(delta);
        }
        public void Explode(Vector2 origin)
        {
            int count = 30;

            for (int i = 0; i < count; i++)
            {
                var angle = _rand.NextDouble() * Math.PI * 2;
                var speed = _rand.Next(200, 600);

                _particles.Add(new Particle
                {
                    Position = origin,
                    Velocity = new Vector2(
                        (float)Math.Cos(angle) * speed,
                        (float)Math.Sin(angle) * speed),
                    Life = 1.6f,
                    Size = _rand.Next(8, 15),
                    Color = SKColor.FromHsv(_rand.Next(360), 100, 100),
                    Shape = (ParticleShape)_rand.Next(0, 4),
                    Rotation = (float)(_rand.NextDouble() * 360)
                });
            }
        }
        //public void Launch(SKPoint origin)
        //{
        //    int count = 80;

        //    for (int i = 0; i < count; i++)
        //    {
        //        var angle = _rand.NextDouble() * Math.PI * 2;
        //        var speed = _rand.Next(200, 500);

        //        _particles.Add(new Particle
        //        {
        //            Position = origin,
        //            Velocity = new SKPoint(
        //                (float)(Math.Cos(angle) * speed),
        //                (float)(Math.Sin(angle) * speed)),
        //            Life = 1.5f,
        //            Size = _rand.Next(5, 10),
        //            Color = SKColor.FromHsv(
        //                _rand.Next(360),
        //                100,
        //                100),
        //            Shape = (ParticleShape)_rand.Next(0, 3)
        //        });
        //    }
        //}
        public void UpdateParticles(float deltaTime)
        {
            const float gravity = 600f;

            for (int i = _particles.Count - 1; i >= 0; i--)
            {
                var p = _particles[i];

                p.Velocity.Y += gravity * deltaTime;
                p.Position = new SKPoint(
                    p.Position.X + p.Velocity.X * deltaTime,
                    p.Position.Y + p.Velocity.Y * deltaTime);

                p.Life -= deltaTime;

                if (p.Life <= 0)
                    _particles.RemoveAt(i);
            }
        }
        public async Task LaunchFireworks(float pageWidth, float pageHeight)
        {
            var point = new SKPoint(pageWidth / 2, pageHeight * 2 / 3);

            for (int i = 0; i < 5; i++)
            {
                LaunchRocket(point);
                await Task.Delay(300);
            }
        }
        public async Task ExplodeConfetti(float pageWidth, float pageHeight)
        {
            var point = new SKPoint(pageWidth / 2, pageHeight / 2);
            int xShift = (int)(pageWidth * 0.4);
            int yShift = (int)(pageHeight * 0.3);

            for (int i = 0; i < 5; i++)
            {
                var randomPoint = new SKPoint(
                    point.X - _rand.Next(-xShift, xShift),
                    point.Y - _rand.Next(-yShift, yShift));

                Explode(randomPoint);

                await Task.Delay(100 + _rand.Next(0, 200) * i * 5);
            }
        }

        internal void Render(SKCanvas canvas, int width, int height)
        {
            canvas.Clear(SKColors.Transparent);

            using var paint = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                BlendMode = SKBlendMode.Plus // glow / additive blending
            };

            paint.StrokeWidth = 3;
            //paint.Style = SKPaintStyle.Stroke;

            foreach (var r in _vm.System.Rockets)
            {
                paint.Color = r.Color;
                //canvas.DrawCircle(r.Position.X, r.Position.Y, 3, paint);
                canvas.DrawLine(
                    r.PreviousPosition.X,
                    r.PreviousPosition.Y,
                    r.Position.X,
                    r.Position.Y,
                    paint);
            }

            foreach (var p in _vm.System.Particles)
            {
                paint.Color = p.Color.WithAlpha(
                    (byte)(255 * Math.Clamp(p.Life / 1.5f, 0, 1)));

                //canvas.DrawCircle(p.Position, p.Size, paint);
                //canvas.DrawRect(p.Position.X, p.Position.Y, p.Size * 3, p.Size * 6, paint);
                switch (p.Shape)
                {
                    case ParticleShape.Circle:
                        canvas.DrawCircle(p.Position.X, p.Position.Y, p.Size, paint);
                        break;

                    case ParticleShape.Square:
                    case ParticleShape.RoundedRectangle:
                        //var rect = SKRect.Create(
                        //    p.Position.X - p.Size,
                        //    p.Position.Y - p.Size / 2,
                        //    p.Size * 2,
                        //    p.Size);
                        var rect = SKRect.Create(
                            -p.Size,
                            -p.Size / 2,
                            p.Size * 2,
                            p.Size); // center at 0,0

                        float cornerRadius = (p.Shape == ParticleShape.RoundedRectangle) ? p.Size / 2 : 0; // adjust for more/less rounding

                        canvas.Save();
                        canvas.Translate(p.Position.X, p.Position.Y);
                        canvas.RotateDegrees(p.Rotation);
                        canvas.DrawRoundRect(rect, cornerRadius, cornerRadius, paint);
                        canvas.Restore();
                        break;

                    case ParticleShape.Triangle:
                        using (var path = new SKPath())
                        {
                            path.MoveTo(0, -p.Size);
                            path.LineTo(-p.Size, p.Size);
                            path.LineTo(p.Size, p.Size);
                            path.Close();
                            canvas.Save();
                            canvas.Translate(p.Position.X, p.Position.Y);
                            canvas.RotateDegrees(p.Rotation);
                            canvas.DrawPath(path, paint);
                            canvas.Restore();
                            break;
                        }
                }
            }
        }
    }
}
