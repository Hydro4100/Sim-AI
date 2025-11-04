using MGGameLibrary.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MainQuest4_DragonDrop
{
    public class Game4 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Agent _agent;
        private Coin _coin;

        private Texture2D _dragonsTexture;
        private Texture2D _coinsTexture;

        private bool _dragged = false;
        private Vector2 _dragOffset;

        public Game4()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Circle coinCircle = new Circle(new Vector2(100, 100), 50);

            _coin = new Coin(coinCircle, Rectangle.Empty);

            _agent = new Agent(new Vector2(100, 350), 0f, this, new SeekBehaviour(_coin));
            Components.Add(_agent);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _dragonsTexture = Content.Load<Texture2D>("dragons");
            _coinsTexture = Content.Load<Texture2D>("coins");
            int coinSourceWidth = _coinsTexture.Width / 4;
            int coinSourceHeight = _coinsTexture.Height / 4;
            _coin.TextureSource = new Rectangle(0, 0, coinSourceWidth, coinSourceHeight);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState mouseState = Mouse.GetState();
            Vector2 mousePosition = mouseState.Position.ToVector2();

            if (_dragged)
            {
                _coin.MoveCoin(mousePosition + _dragOffset);

                if (mouseState.LeftButton == ButtonState.Released)
                {
                    _dragged = false;
                }
            }
            else
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (_coin.Circle.IsInside(mouseState.Position))
                    {
                        _dragged = true;
                        _dragOffset = _coin.Circle.Centre - mousePosition;
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _agent.Draw(_spriteBatch, _dragonsTexture);
            _spriteBatch.Draw(_coinsTexture, _coin.TextureRectangle, _coin.TextureSource, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
