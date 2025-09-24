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
        private Rectangle _blackRectangle;
        private Texture2D _whitePixelTexture;

        private Texture2D _monogameLogoTexture;
        private Texture2D _studioLogo;

        private float _timeRemaining = 1f;
        private float _score = 0;
        private SpriteFont _timerFont;

        enum Screen { FlashScreen, TitleScreen, CreditsScreen, GameScreen, PauseScreen, GameOverScreen };
        private Screen _screen;

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
            _studioLogo = Content.Load<Texture2D>("logo");

            int redRectangleWidth = 200;
            int redRectangleHeight = 100;
            int blackRectangleWidth = 220;
            int blackRectangleHeight = 120;
            int logoWidth = _monogameLogoTexture.Width;
            int logoHeight = _monogameLogoTexture.Height;
            int redX = (GraphicsDevice.Viewport.Width - redRectangleWidth) / 2;
            int redY = (GraphicsDevice.Viewport.Height - redRectangleHeight) / 2;
            int blueX = (GraphicsDevice.Viewport.Width - blackRectangleWidth) / 2;
            int blueY = (GraphicsDevice.Viewport.Height - blackRectangleHeight) / 2;

            int x = (GraphicsDevice.Viewport.Width - logoWidth) / 2;
            int y = (GraphicsDevice.Viewport.Height - logoHeight) / 2;

            _rectangle = new Rectangle(x, y, logoWidth, logoHeight);

            _redRectangle = new Rectangle(redX, redY, redRectangleWidth, redRectangleHeight);
            _blackRectangle = new Rectangle(blueX, blueY, blackRectangleWidth, blackRectangleHeight);

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

            switch (_screen)
            {
                case Screen.FlashScreen:
                    _timeRemaining -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (_timeRemaining < 0)
                    {
                        _screen = Screen.TitleScreen;
                    }
                    break;
                case Screen.TitleScreen:
                    if (Keyboard.GetState().IsKeyDown(Keys.C))
                    {
                        _screen = Screen.CreditsScreen;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        _screen = Screen.GameScreen;
                    }
                    break;
                case Screen.CreditsScreen:
                    if (Keyboard.GetState().IsKeyDown(Keys.T))
                    {
                        _screen = Screen.TitleScreen;
                    }
                    break;
                case Screen.GameScreen:
                    _timeRemaining += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (Keyboard.GetState().IsKeyDown(Keys.P))
                    {
                        _screen = Screen.PauseScreen;
                    }
                    else if (_redRectangle.Contains(Mouse.GetState().Position) && Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        _score = _timeRemaining * 100f;
                        _screen = Screen.GameOverScreen;
                        _timeRemaining = 2f; // YUCK!
                    }
                    else if (_timeRemaining >= 10f)
                    {
                        _score = 0f;
                        _screen = Screen.GameOverScreen;
                        _timeRemaining = 2f; // YUCK!
                    }
                    break;
                case Screen.PauseScreen:
                    if (Keyboard.GetState().IsKeyDown(Keys.U))
                    {
                        _screen = Screen.GameScreen;
                    }
                    break;
                case Screen.GameOverScreen:
                    _timeRemaining -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (_timeRemaining < 0)
                    {
                        _screen = Screen.TitleScreen;
                    }
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            Vector2 textPosition;

            switch (_screen)
            {
                case Screen.FlashScreen:
                    _spriteBatch.Begin();

                    _spriteBatch.Draw(_studioLogo, _rectangle, Color.White);

                    _spriteBatch.End();
                    break;
                case Screen.TitleScreen:
                    _spriteBatch.Begin();
                    textPosition = new Vector2(10, 10);
                    _spriteBatch.DrawString(_timerFont, "Title", textPosition, new Color(252f / 255, 234f / 255, 51f / 255, 1f));
                    _spriteBatch.End();
                    break;
                case Screen.CreditsScreen:
                    _spriteBatch.Begin();
                    textPosition = new Vector2(10, 10);
                    _spriteBatch.DrawString(_timerFont, "Credits", textPosition, new Color(252f / 255, 234f / 255, 51f / 255, 1f));
                    _spriteBatch.End();
                    break;
                case Screen.GameScreen:
                    _spriteBatch.Begin();

                    _spriteBatch.Draw(_whitePixelTexture, _blackRectangle, Color.Black);
                    _spriteBatch.Draw(_whitePixelTexture, _redRectangle, Color.Red);

                    //Vector2 timerSize = _timerFont.MeasureString(_timeRemaining.ToString());

                    Vector2 timerPosition = new Vector2((_graphics.GraphicsDevice.Viewport.Width - _timerFont.MeasureString(_timeRemaining.ToString("0.0")).X) / 2, (_graphics.GraphicsDevice.Viewport.Height - _timerFont.MeasureString(_timeRemaining.ToString("0.0")).Y) / 2);

                    _spriteBatch.DrawString(_timerFont, _timeRemaining.ToString("0.0"), timerPosition + new Vector2(2, 2), new Color(242f / 255, 70f / 255, 80f / 255, 1f));
                    _spriteBatch.DrawString(_timerFont, _timeRemaining.ToString("0.0"), timerPosition, new Color(252f / 255, 234f / 255, 51f / 255, 1f));

                    _spriteBatch.End();
                    break;
                case Screen.PauseScreen:
                    _spriteBatch.Begin();
                    textPosition = new Vector2(10, 10);
                    _spriteBatch.DrawString(_timerFont, "Pause", textPosition, new Color(252f / 255, 234f / 255, 51f / 255, 1f));
                    _spriteBatch.End();
                    break;
                case Screen.GameOverScreen:
                    _spriteBatch.Begin();
                    string gameOverText = $"Game Over!\nYour score: {_score}";
                    textPosition = new Vector2((_graphics.GraphicsDevice.Viewport.Width - _timerFont.MeasureString(gameOverText).X) / 2, (_graphics.GraphicsDevice.Viewport.Height - _timerFont.MeasureString(gameOverText).Y) / 2);
                    _spriteBatch.DrawString(_timerFont, gameOverText, textPosition, new Color(252f / 255, 234f / 255, 51f / 255, 1f));
                    _spriteBatch.End();
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
