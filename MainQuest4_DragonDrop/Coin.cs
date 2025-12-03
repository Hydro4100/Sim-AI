using MGGameLibrary;
using MGGameLibrary.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MainQuest4_DragonDrop
{
    public class Coin : ITargetable
    {
        public Circle Circle { get; set; }
        public Rectangle TextureSource { get; set; }
        public Rectangle TextureRectangle { get; private set; }

        public Vector2 TargetPosition
        {
            get
            {
                return Circle.Position + new Vector2(Circle.Radius);
            }
        }

        public Coin(Circle circle, Rectangle textureSource)
        {
            Circle = circle;
            TextureSource = textureSource;

            TextureRectangle = new Rectangle(
                (int)Circle.Position.X,
                (int)Circle.Position.Y,
                (int)(Circle.Radius * 2),
                (int)(Circle.Radius * 2)
            );
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D coinTexture)
        {
            spriteBatch.Draw(
                texture: coinTexture,
                destinationRectangle: TextureRectangle,
                sourceRectangle: TextureSource,
                color: Color.White,
                rotation: 0f,
                origin: Vector2.Zero,
                effects: SpriteEffects.None,
                layerDepth: 0f
            );
        }

        public void MoveCoin(Vector2 newCenterPosition)
        {
            Vector2 newTopLeft = newCenterPosition - new Vector2(Circle.Radius);

            Circle.Position = newTopLeft;

            TextureRectangle = new Rectangle(
                (int)Circle.Position.X,
                (int)Circle.Position.Y,
                (int)(Circle.Radius * 2),
                (int)(Circle.Radius * 2)
            );
        }
    }
}
