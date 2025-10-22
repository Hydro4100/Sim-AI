
using MGGameLibrary;
using MGGameLibrary.Shapes;
using Microsoft.Xna.Framework;

namespace MainQuest3_AztecDeflect
{
    internal class EnergyOrb : GameComponent, ICollidable
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

        private Vector2 _previousPosition;

        const int ORB_RADIUS = 5;

        public EnergyOrb(Vector2 position, Vector2 velocity, Game game) : base(game)
        {
            Position = position;
            Velocity = velocity;
        }

        public override void Update(GameTime gameTime)
        {
            _previousPosition = Position;
            Position = Position + (float)gameTime.ElapsedGameTime.TotalSeconds * Velocity; // Euler Integration
            base.Update(gameTime);
        }

        public Shape Shape { get { return new Circle(Position, ORB_RADIUS); } } // yuck, inefficient

        public bool CollidesWith(ICollidable other, ref Vector2 collisionNormal)
        {
            return Shape.Intersects(other.Shape, ref collisionNormal);
        }

        public void RevertToPreviousPosition()
        {
            Position = _previousPosition;
        }
    }
}
