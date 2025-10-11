using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MainQuest3_AztecDeflect
{
    internal class PlayerShip : GameComponent
    {
        private Rectangle _rectangle;
        public Rectangle Rectangle;

        private float _fireRate = 0.1f;

        public PlayerShip(Game game) : base(game)
        {
            _rectangle = new Rectangle(10, Game.GraphicsDevice.Viewport.Height - 10 - 75, 75, 75);
        }

        public void MoveSideways(int amount)
        {
            _rectangle.X += amount;
            if (_rectangle.X < 10) _rectangle.X = 10;
            if (_rectangle.X > Game.GraphicsDevice.Viewport.Width - 10 - _rectangle.Width) _rectangle.X = Game.GraphicsDevice.Viewport.Width - 10 - _rectangle.Width;
        }

        public override void Update(GameTime gameTime)
        {
            _fireRate -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                MoveSideways(-5);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                MoveSideways(5);
            }

            Rectangle = _rectangle;

            base.Update(gameTime);
        }

        public EnergyOrb FireEnergyOrb()
        {
            if (_fireRate <= 0)
            {
                _fireRate = 0.1f;
                EnergyOrb orb = new EnergyOrb(new Vector2(_rectangle.Center.X, _rectangle.Center.Y - _rectangle.Height / 2), new Vector2(0, -200), Game);
                return orb;
            }
            return null;
        }
    }
}
