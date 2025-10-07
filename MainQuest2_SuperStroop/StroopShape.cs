using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MainQuest2_SuperStroop
{
    public abstract class StroopShape : GameComponent
    {
        private Color _colour;
        protected Rectangle _rectangle;
        private Texture2D _texture;

        private Vector2 _startPosition;
        private Vector2 _endPosition;
        private float _movementDuration;
        private float _elapsedTime;

        public StroopShape(Game game, Rectangle rectangle, Color colour, Texture2D texture):base(game)
        {
            _rectangle = rectangle;
            _colour = colour;
            _texture = texture;

            _startPosition = new Vector2(rectangle.X, rectangle.Y);
            _endPosition = new Vector2(rectangle.X + Game.GraphicsDevice.Viewport.Width - 2 * rectangle.X, rectangle.Y);
            _elapsedTime = 0f;
            _movementDuration = 5f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rectangle, _colour);
        }

        public abstract bool IsInside(Point point);

        public override string ToString()
        {
            return $"{_texture}";
        }

        public Color Colour
        {
            get
            {
                return _colour;
            }
        }

        public override void Update(GameTime gameTime)
        {
            _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            float t = MathHelper.Clamp(_elapsedTime / _movementDuration, 0f, 1f);
            t = MathHelper.SmoothStep(0, 1, t);

            Vector2 newPosition = (1 - t) * _startPosition + t * _endPosition;
            _rectangle.X = (int)newPosition.X;
            _rectangle.Y = (int)newPosition.Y;

            if (t >= 1f)
            {
                (_startPosition, _endPosition) = (_endPosition, _startPosition);
                _elapsedTime = 0f;
            }

            base.Update(gameTime);
        }
    }
}
