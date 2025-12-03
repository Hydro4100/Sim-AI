using Microsoft.Xna.Framework;

namespace MGGameLibrary.Shapes
{
    public class Square : Shape
    {
        public float Size;

        public Square(Vector2 position, int size) : base(position, size)
        {
            Size = size;
        }

        public override bool IsInside(Point point)
        {
            return _rectangle.Contains(point);
        }

        public override bool Intersects(Shape other, ref Vector2 collisionNormal)
        {
            return other.IntersectsSquare(this, ref collisionNormal);
        }

        public override bool IntersectsCircle(Circle other, ref Vector2 collisionNormal)
        {
            return Shape.Intersects(other, this, ref collisionNormal);
        }

        public override bool IntersectsSquare(Square other, ref Vector2 collisionNormal)
        {
            return false;
        }
    }
}
