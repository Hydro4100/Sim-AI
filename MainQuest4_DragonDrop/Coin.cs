using MGGameLibrary.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MainQuest4_DragonDrop
{
    public class Coin
    {
        public Circle Circle { get; set; }
        public Rectangle TextureSource { get; set; }
        public Rectangle TextureRectangle { get; private set; }

        public Coin(Circle circle, Rectangle textureSource)
        {
            Circle = circle;
            TextureSource = textureSource;
            TextureRectangle = new Rectangle((int)(Circle.Position.X - Circle.Radius), (int)(Circle.Position.Y - Circle.Radius), (int)(Circle.Radius * 2), (int)(Circle.Radius * 2));
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
    }
}
