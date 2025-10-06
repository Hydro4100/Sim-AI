using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MainQuest2_SuperStroop
{
    internal class StroopCircle : StroopShape
    {
        public StroopCircle(Rectangle rectangle, Color colour, Texture2D texture) : base(rectangle, colour, texture)
        {

        }

        public override bool IsInside(Point point)
        {
            float radius = _rectangle.Width / 2;
            return MathF.Pow(point.X - (_rectangle.X + radius), 2) + MathF.Pow(point.Y - (_rectangle.Y + radius), 2) <= MathF.Pow(radius, 2);
        }
    }
}
