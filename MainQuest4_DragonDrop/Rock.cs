using MGGameLibrary.Shapes;
using Microsoft.Xna.Framework;

namespace MainQuest4_DragonDrop
{
    public class Rock
    {
        public Circle Circle { get; set; }

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
    }
}
