using MGGameLibrary;
using MGGameLibrary.Shapes;
using Microsoft.Xna.Framework;

namespace MainQuest4_DragonDrop
{
    public class Rock : ICollidable
    {
        public Circle Circle { get; set; }

        public Shape Shape { get { return Circle; } }

        public Rectangle TextureRectangle
        {
            get
            {
                return new Rectangle(
                    (int)Circle.Position.X,
                    (int)Circle.Position.Y,
                    (int)(Circle.Radius * 2),
                    (int)(Circle.Radius * 2)
                );
            }
        }

        public Rock(Circle circle)
        {
            Circle = circle;
        }

        public bool CollidesWith(ICollidable other, ref Vector2 collisionNormal)
        {
            return Circle.Intersects(other.Shape, ref collisionNormal);
        }
    }
}