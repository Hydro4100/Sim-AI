using MGGameLibrary.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MainQuest2_SuperStroop
{
    public class StroopShape : GameComponent
    {
        private Color _colour;
        protected Rectangle _rectangle;
        private Texture2D _texture;

        private Vector2 _startPosition;
        private Vector2 _endPosition;
        private float _movementDuration;
        private float _elapsedTime;

        private static Random random = new Random();

        private Shape _libraryShape;

        public StroopShape(Game game, Color colour, Texture2D texture) : base(game)
        {
            int size = random.Next(50, 100);
            _startPosition = new Vector2(random.Next(size, game.GraphicsDevice.Viewport.Width - size), random.Next(size, game.GraphicsDevice.Viewport.Height - size));
            _endPosition = new Vector2(random.Next(size, game.GraphicsDevice.Viewport.Width - size), random.Next(size, game.GraphicsDevice.Viewport.Height - size));
            _rectangle = new Rectangle((int)_startPosition.X, (int)_startPosition.Y, size, size);
            _elapsedTime = 0f;
            _movementDuration = 2f + 3 * random.NextSingle();

            _colour = colour;
            _texture = texture;

            string shapeType = texture.Name.ToLower();

            switch (shapeType)
            {
                case "circle":
                    _libraryShape = new Circle(_startPosition, size);
                    break;
                case "triangle":
                    _libraryShape = new Triangle(_startPosition, size);
                    break;
                case "square":
                default:
                    _libraryShape = new Square(_startPosition, size);
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rectangle, _colour);
        }

        public bool IsInside(Point point)
        {
            return _libraryShape.IsInside(point);
        }

        public override string ToString()
        {
            return $"{_libraryShape.GetType().Name}";
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

            _libraryShape.UpdateRectangle(_rectangle);

            if (t >= 1f)
            {
                (_startPosition, _endPosition) = (_endPosition, _startPosition);
                _elapsedTime = 0f;
            }

            base.Update(gameTime);
        }
    }
}
