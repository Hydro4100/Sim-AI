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

        public Game4()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Vector2 initialAgentPosition = new Vector2(
                _graphics.PreferredBackBufferWidth / 2,
                _graphics.PreferredBackBufferHeight / 2
            );

            Circle coinCircle = new Circle(new Vector2(100, 100), 50);

            _coin = new Coin(coinCircle, Rectangle.Empty);

            SeekBehaviour seekBehaviour = new SeekBehaviour(_coin);

            _agent = new Agent(initialAgentPosition, 0f, this, seekBehaviour);
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

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _agent.Draw(_spriteBatch, _dragonsTexture);
            _coin.Draw(_spriteBatch, _coinsTexture);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
