using Microsoft.Xna.Framework;
using System;

namespace MGGameLibrary.Shapes
{
    public class Circle : Shape
    {
        public Vector2 Centre;
        public float Radius;

        public Circle(Vector2 position, int size) : base(position, size)
        {
            Radius = size / 2;
            Centre = new Vector2(position.X + Radius, position.Y + Radius);
        }

        public override bool IsInside(Point point)
        {
            float radius = _rectangle.Width / 2;
            return MathF.Pow(point.X - (_rectangle.X + radius), 2) + MathF.Pow(point.Y - (_rectangle.Y + radius), 2) <= MathF.Pow(radius, 2);
        }

        public override bool Intersects(Shape other)
        {
            if (other is Circle)
            {
                return Shape.Intersects(other as Circle, this);
            }

            return false;
        }

        public override bool IntersectsCircle(Circle circle)
        {
            throw new NotImplementedException();
        }
    }
}
