
using Microsoft.Xna.Framework;

namespace MainQuest3_AztecDeflect
{
    internal class EnergyOrb : GameComponent
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)(Position.X - ORB_RADIUS), (int)(Position.Y - ORB_RADIUS), ORB_RADIUS * 2, ORB_RADIUS * 2);
            }
        }

        const int ORB_RADIUS = 5;

        public EnergyOrb(Vector2 position, Vector2 velocity, Game game) : base(game)
        {
            Position = position;
            Velocity = velocity;
        }

        public override void Update(GameTime gameTime)
        {
            Position = Position + (float)gameTime.ElapsedGameTime.TotalSeconds * Velocity; // Euler Integration
            base.Update(gameTime);
        }
    }
}
