using MGGameLibrary;
using MGGameLibrary.Shapes;
using Microsoft.Xna.Framework;

namespace MainQuest3_AztecDeflect
{
    internal class Disc : PhysicsObject, ICollidable
    {
        public const int DISC_RADIUS = 50;
        public const int DISC_MASS = 10;
        private Circle _circle;

        public Disc(float mass, Vector2 position, Game game) : base(mass, position, game)
        {
            _circle = new Circle(position, DISC_RADIUS * 2);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            _circle.Position = Position;
            _circle.Centre = new Vector2(Position.X + _circle.Radius, Position.Y + _circle.Radius);
        }

        public bool CollidesWith(ICollidable other, ref Vector2 collisionNormal)
        {
            return _circle.Intersects(other.Shape, ref collisionNormal);
        }

        public bool CollidesWith(ICollidable other)
        {
            Vector2 dummyNormal = Vector2.Zero;
            return CollidesWith(other, ref dummyNormal);
        }

        public Shape Shape { get { return _circle; } }
    }
}
