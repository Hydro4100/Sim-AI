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
        private Texture2D _dragonsTexture;

        private Coin _coin;
        private Texture2D _coinsTexture;
        private const int CoinSize = 50;

        public Game4()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Vector2 agentStartPosition = new Vector2(GraphicsDevice.Viewport.Width / 2f, GraphicsDevice.Viewport.Height / 2f);
            _agent = new Agent(agentStartPosition, 0f, this);
            Components.Add(_agent);

            Vector2 coinCenterPosition = new Vector2(GraphicsDevice.Viewport.Width - CoinSize, GraphicsDevice.Viewport.Height - CoinSize);

            Circle coinCircle = new Circle(new Vector2(coinCenterPosition.X - CoinSize / 2f, coinCenterPosition.Y - CoinSize / 2f), CoinSize);

            int spriteWidth = 100;
            int spriteHeight = 100;

            Rectangle dummySource = new Rectangle(0, 0, 1, 1);
            _coin = new Coin(coinCircle, dummySource);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _dragonsTexture = Content.Load<Texture2D>("dragons");
            _coinsTexture = Content.Load<Texture2D>("coins");
            int coinSourceWidth = _coinsTexture.Width / 4;
            int coinSourceHeight = _coinsTexture.Height / 4;
            int sourceX = 2 * coinSourceWidth;
            int sourceY = 0 * coinSourceHeight;
            _coin.TextureSource = new Rectangle(sourceX, sourceY, coinSourceWidth, coinSourceHeight);
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
