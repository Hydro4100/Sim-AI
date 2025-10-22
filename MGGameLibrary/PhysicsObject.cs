using Microsoft.Xna.Framework;

namespace MGGameLibrary
{
    public class PhysicsObject
    {
        private float _mass;
        private Vector2 _position;
        private Vector2 _previousPosition;
        private Vector2 _velocity;
        private Vector2 _force;

        public PhysicsObject(float mass, Vector2 position, Game game)
        {
            _mass = mass;
            _position = position;
        }

        public void ApplyForce(Vector2 force)
        {
            _force += force;
        }

        public virtual void Update(float deltaTime)
        {
            _previousPosition = _position;
            // a = F/m
            Vector2 acceleration = _force / _mass;
            // v = v0 + at
            _velocity += acceleration * deltaTime;
            // s = s0 + vt
            _position += _velocity * deltaTime;
            // Reset force for next frame
            _force = Vector2.Zero;
        }

        public void RevertToPreviousPosition()
        {

        }

        public Vector2 Position { get { return _position; } set { _position = value; } }
        public Vector2 Velocity { get { return _velocity; } set { _velocity = value; } }
    }
}
