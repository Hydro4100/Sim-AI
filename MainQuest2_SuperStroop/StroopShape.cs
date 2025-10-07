using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MainQuest2_SuperStroop
{
    public abstract class StroopShape : GameComponent
    {
        private Color _colour;
        protected Rectangle _rectangle;
        private Texture2D _texture;

        public StroopShape(Game game, Rectangle rectangle, Color colour, Texture2D texture):base(game)
        {
            _rectangle = rectangle;
            _colour = colour;
            _texture = texture;
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
    }
}
