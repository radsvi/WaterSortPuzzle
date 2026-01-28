using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WaterSortPuzzle.Features.Fireworks.Enums;

namespace WaterSortPuzzle.Features.Fireworks.Models
{
    public class Particle
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public float Life;
        public float Size;
        public SKColor Color;
        public ParticleShape Shape;
        public float Rotation;
    }
}
