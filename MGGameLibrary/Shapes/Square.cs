using Microsoft.Xna.Framework;

namespace MGGameLibrary.Shapes
{
    public class Square : Shape
    {
        public float Size;
        public Square(Vector2 position, int size) : base(position, size)
        {
        }

        public override bool IsInside(Point point)
        {
            return _rectangle.Contains(point);
        }

        public override bool Intersects(Shape other)
        {
            throw new System.NotImplementedException();
        }

        public override bool IntersectsCircle(Circle circle)
        {
            throw new System.NotImplementedException();
        }
    }
}
