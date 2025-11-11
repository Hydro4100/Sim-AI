using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MainQuest6_TreasureHunter
{
    public class Game6 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _whitePixelTexture;
        private TileMap _tileMap;

        public Game6()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _whitePixelTexture = new Texture2D(GraphicsDevice, 1, 1);
            _whitePixelTexture.SetData(new Color[] { Color.White });

            _tileMap = new TileMap(50, 30, _whitePixelTexture);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    _tileMap.ChangeType(Mouse.GetState().Position, Tile.TileType.WALL);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.F))
                {
                    _tileMap.ChangeType(Mouse.GetState().Position, Tile.TileType.FOOD);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.P))
                {
                    _tileMap.FindNewPath(Mouse.GetState().Position);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _tileMap.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
