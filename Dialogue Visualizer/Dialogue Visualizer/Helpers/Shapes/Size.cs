using System;

namespace Dialogue_Visualizer.Helpers.Shapes
{
    public class Size2DInt
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Size2DInt(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Size2DInt Rise(int change)
        {
            Y += change;
            return this;
        }

        public Size2DInt Run(int change)
        {
            X += change;
            return this;
        }
    }

    public static class Size2DIntExtensions
    {
        public static int Difference(this Size2DInt a, Size2DInt b) { return Math.Abs(a.X - b.X); }
        public static Size2DInt Clamp(this Size2DInt a, Size2DInt min, Size2DInt max)
        {
            var x = Math.Clamp(a.X, min.X, max.X);
            var y = Math.Clamp(a.Y, min.Y, max.Y);
            return new Size2DInt(x, y);
        }
        public static Size2DInt CrossDivide(this Size2DInt a, Size2DInt b)
        {
            var x = (a.X / b.X) + (a.Y / b.Y);
            var y = (a.Y / b.X) + (a.X / b.Y);
            return new Size2DInt(x, y);
        }
        public static Size2DInt ScalarDivide(this Size2DInt a,  int scalar)
        {
            return new Size2DInt((int)Math.Ceiling((decimal) a.X / scalar), (int)Math.Ceiling((decimal)a.Y / scalar));
        }
    }

    public class Size3DInt : Size2DInt
    {
        public new int X { get; set; }
        public new int Y { get; set; }
        public int Z { get; set; }

        public Size3DInt(int x, int y, int z)
            : base(x, y)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
