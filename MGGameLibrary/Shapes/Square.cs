using Microsoft.Xna.Framework;

namespace MGGameLibrary.Shapes
{
    public class Square : Shape
    {
        public Square(Vector2 position, int size) : base(position, size)
        {
        }

        public override bool IsInside(Point point)
        {
            return _rectangle.Contains(point);
        }
    }
}
