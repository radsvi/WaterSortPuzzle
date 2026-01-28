using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Features.Fireworks.Models
{
    public class Rocket
    {
        public Vector2 PreviousPosition;
        public Vector2 Position;
        public Vector2 Velocity;
        public float Life;
        public SKColor Color;
    }
}
