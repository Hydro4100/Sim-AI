using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MainQuest3_AztecDeflect
{
    internal class PlayerShip : GameComponent
    {
        private Rectangle _rectangle;
        public Rectangle Rectangle;

        public PlayerShip(Game game) : base(game)
        {
            _rectangle = new Rectangle(10, Game.GraphicsDevice.Viewport.Height - 10 - 75, 75, 75);
            Rectangle = new Rectangle(0, 0, 256, 256);
        }

        public void MoveSideways(int amount)
        {
            _rectangle.X += amount;
            if (_rectangle.X < 10) _rectangle.X = 10;
            if (_rectangle.X > Game.GraphicsDevice.Viewport.Width - 10 - _rectangle.Width) _rectangle.X = Game.GraphicsDevice.Viewport.Width - 10 - _rectangle.Width;
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                MoveSideways(-5);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                MoveSideways(5);
            }

            base.Update(gameTime);
        }
    }
}
