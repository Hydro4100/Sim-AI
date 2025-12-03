using MGGameLibrary.Shapes;
using Microsoft.Xna.Framework;
using System;

namespace MGGameLibrary
{
    public class LineSegment : Shape
    {
        public Vector2 Start
        {
            get { return Position; }
            set { Position = value; }
        }
        public Vector2 End { get; set; }

        public LineSegment(Vector2 start, Vector2 end) : base(start, 0)
        {
            End = end;
        }

        public override bool IsInside(Point point)
        {
            throw new NotImplementedException();
        }

        public override bool Intersects(Shape other, ref Vector2 collisionNormal)
        {
            throw new NotImplementedException();
        }

        public override bool IntersectsCircle(Circle other, ref Vector2 collisionNormal)
        {
            throw new NotImplementedException();
        }

        public override bool IntersectsSquare(Square other, ref Vector2 collisionNormal)
        {
            throw new NotImplementedException();
        }
    }
}
