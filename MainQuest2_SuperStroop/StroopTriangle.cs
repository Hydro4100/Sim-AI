using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MainQuest2_SuperStroop
{
    internal class StroopTriangle : StroopShape
    {
        public StroopTriangle(Game game, Color colour, Texture2D texture) : base(game, colour, texture)
        {

        }

        public override bool IsInside(Point point)
        {
            Point pointA = new Point(_rectangle.X + (_rectangle.Width / 2), _rectangle.Y);
            Point pointB = new Point(_rectangle.X, _rectangle.Y + _rectangle.Height);
            Point pointC = new Point(_rectangle.X + _rectangle.Width, _rectangle.Y + _rectangle.Height);

            float w1 = ((pointA.X * (pointC.Y - pointA.Y)) + ((point.Y - pointA.Y) * (pointC.X - pointA.X)) - (point.X * (pointC.Y - pointA.Y))) / (((pointB.Y - pointA.Y) * (pointC.X - pointA.X)) - ((pointB.X - pointA.X) * (pointC.Y - pointA.Y)) * 1f);
            float w2 = (point.Y - pointA.Y - (w1 * (pointB.Y - pointA.Y))) / ((pointC.Y - pointA.Y) * 1f);

            return w1 >= 0 && w2 >= 0 && (w1 + w2) <= 1;
        }
    }
}
