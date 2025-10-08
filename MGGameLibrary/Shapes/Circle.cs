using Microsoft.Xna.Framework;
using System;

namespace MGGameLibrary.Shapes
{
    public class Circle : Shape
    {
        public Circle(Vector2 position, int size) : base(position, size)
        {
        }

        public override bool IsInside(Point point)
        {
            float radius = _rectangle.Width / 2;
            return MathF.Pow(point.X - (_rectangle.X + radius), 2) + MathF.Pow(point.Y - (_rectangle.Y + radius), 2) <= MathF.Pow(radius, 2);
        }
    }
}
