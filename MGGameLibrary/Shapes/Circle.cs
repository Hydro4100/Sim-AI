using Microsoft.Xna.Framework;
using System;

namespace MGGameLibrary.Shapes
{
    public class Circle : Shape
    {
        public Vector2 Centre
        {
            get { return new Vector2(Position.X + Radius, Position.Y + Radius); }
        }
        public float Radius;

        public Circle(Vector2 position, int size) : base(position, size)
        {
            Radius = size / 2;
        }

        public override bool IsInside(Point point)
        {
            float radius = _rectangle.Width / 2;
            return MathF.Pow(point.X - (_rectangle.X + radius), 2) + MathF.Pow(point.Y - (_rectangle.Y + radius), 2) <= MathF.Pow(radius, 2);
        }

        public override bool Intersects(Shape other, ref Vector2 collisionNormal)
        {
            return other.IntersectsCircle(this, ref collisionNormal);
        }

        public override bool IntersectsCircle(Circle other, ref Vector2 collisionNormal)
        {
            return Intersects(this, other, ref collisionNormal);
        }

        public override bool IntersectsSquare(Square other, ref Vector2 collisionNormal)
        {
            return other.IntersectsCircle(this, ref collisionNormal);
        }
    }
}
