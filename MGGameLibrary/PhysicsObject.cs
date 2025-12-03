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
            _previousPosition = position;
            _velocity = Vector2.Zero;
            _force = Vector2.Zero;
        }

        public void ApplyForce(Vector2 force)
        {
            _force += force;
        }

        public virtual void Update(float deltaTime)
        {
            _previousPosition = _position;
            Vector2 acceleration = _force / _mass;
            _velocity += acceleration * deltaTime;
            _position += _velocity * deltaTime;
            _force = Vector2.Zero;
        }

        public void RevertToPreviousPosition()
        {
            _position = _previousPosition;
        }

        public void ApplyGravity()
        {
            const float GRAVITY = 250f;
            _force += new Vector2(0, GRAVITY * _mass);
        }

        public Vector2 Position { get { return _position; } set { _position = value; } }
        public Vector2 Velocity { get { return _velocity; } set { _velocity = value; } }
    }
}
