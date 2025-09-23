using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MainQuest1_ClosestToTen
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Rectangle _redRectangle;
        private Rectangle _blueRectangle;
        private Texture2D _whitePixelTexture;
        private Texture2D _redPixelTexture;
        private Texture2D _bluePixelTexture;
        public Game1()
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

            // TODO: use this.Content to load your game content here 
            int rectangleWidth = 200;
            int rectangleHeight = 100;
            int redX = 0;
            int redY = GraphicsDevice.Viewport.Height - rectangleHeight;
            int blueX = GraphicsDevice.Viewport.Width - rectangleWidth;
            int blueY = GraphicsDevice.Viewport.Height - rectangleHeight;

            _redRectangle = new Rectangle(redX, redY, rectangleWidth, rectangleHeight);
            _blueRectangle = new Rectangle(blueX, blueY, rectangleWidth, rectangleHeight);

            _whitePixelTexture = new Texture2D(GraphicsDevice, 1, 1);
            _whitePixelTexture.SetData(new Color[] { Color.White });
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(_whitePixelTexture, _redRectangle, Color.Red);
            _spriteBatch.Draw(_whitePixelTexture, _blueRectangle, Color.Blue);

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
