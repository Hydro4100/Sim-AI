using Microsoft.Xna.Framework;

namespace MGGameLibrary.Shapes
{
    public abstract class Shape
    {
        protected Rectangle _rectangle;
        public Vector2 Position
        {
            get { return new Vector2(_rectangle.X, _rectangle.Y); }
            set
            {
                _rectangle.X = (int)value.X;
                _rectangle.Y = (int)value.Y;
            }
        }

        public Shape(Vector2 position, int size)
        {
            _rectangle = new Rectangle((int)position.X, (int)position.Y, size, size);
        }

        public void UpdateRectangle(Rectangle newRect)
        {
            _rectangle = newRect;
        }

        public abstract bool IsInside(Point point);
    }
}