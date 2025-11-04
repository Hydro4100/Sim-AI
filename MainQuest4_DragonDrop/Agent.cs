using MGGameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MainQuest4_DragonDrop
{
    public class Agent : GameComponent
    {
        private SteeringBehaviour _behaviour;
        private PhysicsObject _physicsObject;
        public float MaxSpeed { get; set; }
        public float Heading { get; private set; }
        public Vector2 Position
        {
            get { return _physicsObject.Position; }
            set { _physicsObject.Position = value; }
        }
        public Vector2 Velocity
        {
            get { return _physicsObject.Velocity; }
            set { _physicsObject.Velocity = value; }
        }

        public Agent(Vector2 position, float heading, Game game, SteeringBehaviour behaviour) : base(game)
        {
            _physicsObject = new PhysicsObject(1.0f, position, game);
            Heading = heading;
            _behaviour = behaviour;
            MaxSpeed = 200f;
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_behaviour != null)
            {
                Vector2 steeringForce = _behaviour.CalculateSteeringForce(this);
                _physicsObject.ApplyForce(steeringForce);
            }

            _physicsObject.Update(deltaTime);

            Vector2 velocity = _physicsObject.Velocity;

            if (velocity.LengthSquared() > 0)
            {
                velocity.Normalize();
                Heading = MathF.Atan2(velocity.Y, velocity.X) + MathF.PI / 2;
            }

            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D dragonsTexture)
        {
            int textureColumns = 3;
            int textureRows = 2;

            int sourceWidth = dragonsTexture.Width / textureColumns;
            int sourceHeight = dragonsTexture.Height / textureRows;

            int dragonIndexX = 0;
            int dragonIndexY = 0;

            int sourceX = dragonIndexX * sourceWidth;
            int sourceY = dragonIndexY * sourceHeight;

            Rectangle sourceRectangle = new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight);
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, sourceWidth, sourceHeight);

            Vector2 origin = new Vector2(sourceWidth / 2f, sourceHeight / 2f);

            spriteBatch.Draw(
                dragonsTexture,
                Position,
                sourceRectangle,
                Color.White,
                Heading,
                origin,
                1.0f,
                SpriteEffects.None,
                0f
            );
        }
    }
}
