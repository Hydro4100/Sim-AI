using Microsoft.Xna.Framework;

namespace MainQuest6_TreasureHunter
{
    public class NavMeshNode
    {
        public Rectangle Rectangle { get; private set; }
        public Color Colour { get; private set; }

        public Vector2 Centre { get { return new Vector2(Rectangle.X + Rectangle.Width / 2f, Rectangle.Y + Rectangle.Height / 2f); } }

        public NavMeshNode(Rectangle rectangle, Color colour)
        {
            Rectangle = rectangle;
            Colour = colour;
        }
    }
}
