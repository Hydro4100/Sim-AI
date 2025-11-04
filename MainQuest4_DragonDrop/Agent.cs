using MGGameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MainQuest4_DragonDrop
{
    public class Agent : GameComponent
    {
        private PhysicsObject _physicsObject;
        private float _heading;

        public Agent(Vector2 position, float heading, Game game) : base(game)
        {
            _physicsObject = new PhysicsObject(1.0f, position, game);
            _heading = heading;
        }

        public Vector2 Position
        {
            get { return _physicsObject.Position; }
            set { _physicsObject.Position = value; }
        }

        public float Heading
        {
            get { return _heading; }
            set { _heading = value; }
        }

        public void ApplyMovementForce(Vector2 force)
        {
            _physicsObject.ApplyForce(force);
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _heading += 1.0f * deltaTime;

            if (_heading > MathHelper.TwoPi)
            {
                _heading -= MathHelper.TwoPi;
            }
            if (_heading < MathHelper.TwoPi)
            {
                _heading += MathHelper.TwoPi;
            }

            _physicsObject.Update(deltaTime);

            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            int textureColumns = 3;
            int textureRows = 2;

            int sourceWidth = texture.Width / textureColumns;
            int sourceHeight = texture.Height / textureRows;

            int dragonIndexX = 0;
            int dragonIndexY = 0;

            int sourceX = dragonIndexX * sourceWidth;
            int sourceY = dragonIndexY * sourceHeight;

            Rectangle sourceRectangle = new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight);
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, sourceWidth, sourceHeight);

            Vector2 origin = new Vector2(sourceWidth / 2f, sourceHeight / 2f);

            spriteBatch.Draw(
                texture: texture,
                destinationRectangle: destinationRectangle,
                sourceRectangle: sourceRectangle,
                color: Color.White,
                rotation: _heading,
                origin: origin,
                effects: SpriteEffects.None,
                layerDepth: 0f
            );
        }
    }
}
