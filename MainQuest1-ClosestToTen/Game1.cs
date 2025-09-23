using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MainQuest1_ClosestToTen
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Rectangle _rectangle;
        private Rectangle _redRectangle;
        private Rectangle _blueRectangle;
        private Texture2D _whitePixelTexture;

        private Texture2D _monogameLogoTexture;

        private float _timeRemaining = 10f;
        private SpriteFont _timerFont;
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
            _monogameLogoTexture = Content.Load<Texture2D>("monogame");

            int rectangleWidth = 200;
            int rectangleHeight = 100;
            int logoWidth = _monogameLogoTexture.Width;
            int logoHeight = _monogameLogoTexture.Height;
            int redX = 0;
            int redY = GraphicsDevice.Viewport.Height - rectangleHeight;
            int blueX = GraphicsDevice.Viewport.Width - rectangleWidth;
            int blueY = GraphicsDevice.Viewport.Height - rectangleHeight;

            int x = (GraphicsDevice.Viewport.Width - logoWidth) / 2;
            int y = (GraphicsDevice.Viewport.Height - logoHeight) / 2;

            _rectangle = new Rectangle(x, y, logoWidth, logoHeight);

            _redRectangle = new Rectangle(redX, redY, rectangleWidth, rectangleHeight);
            _blueRectangle = new Rectangle(blueX, blueY, rectangleWidth, rectangleHeight);

            _whitePixelTexture = new Texture2D(GraphicsDevice, 1, 1);
            _whitePixelTexture.SetData(new Color[] { Color.White });

            _timerFont = Content.Load<SpriteFont>("Timer");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            float secondsPassed = gameTime.ElapsedGameTime.Milliseconds / 1000f;

            _timeRemaining -= secondsPassed;

            if (_timeRemaining < 0)
            {
                _timeRemaining = 0;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(_whitePixelTexture, _redRectangle, Color.Red);
            _spriteBatch.Draw(_whitePixelTexture, _blueRectangle, Color.Blue);

            _spriteBatch.Draw(_monogameLogoTexture, _rectangle, Color.White);

            Vector2 timerSize = _timerFont.MeasureString(_timeRemaining.ToString());

            Vector2 timerPosition = new Vector2(_graphics.GraphicsDevice.Viewport.Width - _timerFont.MeasureString(_timeRemaining.ToString("0.0")).X - 10, 10);

            _spriteBatch.DrawString(_timerFont, _timeRemaining.ToString("0.0"), timerPosition + new Vector2(2, 2), new Color(242f / 255, 70f / 255, 80f / 255, 1f));
            _spriteBatch.DrawString(_timerFont, _timeRemaining.ToString("0.0"), timerPosition, new Color(252f / 255, 234f / 255, 51f / 255, 1f));

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
