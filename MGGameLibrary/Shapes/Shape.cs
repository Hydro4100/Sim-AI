using Microsoft.Xna.Framework;
using System;

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

        public abstract bool Intersects(Shape other, ref Vector2 collisionNormal);
        public abstract bool IntersectsCircle(Circle other, ref Vector2 collisionNormal);
        public abstract bool IntersectsSquare(Square other, ref Vector2 collisionNormal);

        public static bool Intersects(Circle c1, Circle c2, ref Vector2 collisionNormal)
        {
            float distanceSquared = Vector2.DistanceSquared(c1.Centre, c2.Centre);
            float radiusSum = c1.Radius + c2.Radius;
            bool collision = distanceSquared <= radiusSum * radiusSum;

            if (collision && c1 != c2)
            {
                collisionNormal = Vector2.Normalize(c1.Centre - c2.Centre);
            }

            return collision;
        }

        public static bool Intersects(Circle c, Square s, ref Vector2 collisionNormal)
        {
            float closestX = Math.Clamp(c.Centre.X, s.Position.X, s.Position.X + s.Size);
            float closestY = Math.Clamp(c.Centre.Y, s.Position.Y, s.Position.Y + s.Size);

            Vector2 closestPoint = new Vector2(closestX, closestY);

            Vector2 diff = c.Centre - closestPoint;
            float distancesquared = diff.LengthSquared();

            if (distancesquared <= c.Radius * c.Radius)
            {
                collisionNormal = diff != Vector2.Zero ? Vector2.Normalize(diff) : Vector2.UnitY;
                return true;
            }

            collisionNormal = Vector2.Zero;
            return false;
        }

        public static bool Intersects(LineSegment line, Circle circle)
        {
            Vector2 toCircle = circle.Centre - line.Start;
            Vector2 lineDir = line.End - line.Start;
            float t = Vector2.Dot(toCircle, lineDir) / Vector2.Dot(lineDir, lineDir);
            t = MathHelper.Clamp(t, 0.0f, 1.0f);
            Vector2 closestPoint = line.Start + t * lineDir;
            float distanceSquared = Vector2.DistanceSquared(closestPoint, circle.Centre);
            return distanceSquared <= circle.Radius * circle.Radius;
        }
    }
}