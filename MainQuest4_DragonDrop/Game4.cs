using MGGameLibrary;
using MGGameLibrary.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MainQuest4_DragonDrop
{
    public class Game4 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Agent _agent;
        private Agent _agent2;
        private Agent _agent3;
        private Agent _agent4;
        private Coin _coin;
        private Rock _rock;

        private List<ITargetable> _path;

        private Texture2D _dragonsTexture;
        private Texture2D _coinsTexture;
        private Texture2D _rockTexture;

        private bool _dragged = false;
        private Vector2 _dragOffset;

        private Color _backgroundColor;

        public Game4()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _backgroundColor = Color.CornflowerBlue;

            Circle coinCircle = new Circle(new Vector2(100, 100), 50);

            _coin = new Coin(coinCircle, Rectangle.Empty);

            Vector2 rockPosition = new Vector2(_graphics.GraphicsDevice.Viewport.Width / 2 - 64, _graphics.GraphicsDevice.Viewport.Height / 2 - 64);
            int rockSize = 128;
            Circle rockCircle = new Circle(rockPosition, rockSize);
            _rock = new Rock(rockCircle);

            _path = new List<ITargetable>
            {
                new SimpleTargetable(new Vector2(100, 100)),
                new SimpleTargetable(new Vector2(300, 100)),
                new SimpleTargetable(new Vector2(300, 300)),
                new SimpleTargetable(new Vector2(100, 300))
            };

            _agent = new Agent(new Vector2(100, 350), 0f, this, new SeekBehaviour(_coin), 0, 0);
            Components.Add(_agent);

            _agent2 = new Agent(new Vector2(50, 50), 0f, this, new SeekBehaviour(_agent), 1, 0);
            Components.Add(_agent2);

            _agent3 = new Agent(new Vector2(100, 100), 0f, this, new PathFollowingBehaviour(_path, 50f), 2, 0);
            Components.Add(_agent3);

            _agent4 = new Agent(new Vector2(100, 100), 0f, this, new FleeBehaviour(_coin), 0, 1);
            Components.Add(_agent4);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _dragonsTexture = Content.Load<Texture2D>("dragons");
            _coinsTexture = Content.Load<Texture2D>("coins");
            _rockTexture = Content.Load<Texture2D>("rock");

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

            Vector2 coinCenter = _coin.Circle.Position + new Vector2(_coin.Circle.Radius);

            LineSegment toCoin = new LineSegment(_agent.Position, coinCenter);

            if (Shape.Intersects(toCoin, _rock.Circle))
            {
                _backgroundColor = Color.SlateBlue;
            }
            else
            {
                _backgroundColor = Color.CornflowerBlue;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_backgroundColor);

            _spriteBatch.Begin();

            _agent.Draw(_spriteBatch, _dragonsTexture);
            _agent2.Draw(_spriteBatch, _dragonsTexture);
            _agent3.Draw(_spriteBatch, _dragonsTexture);
            _agent4.Draw(_spriteBatch, _dragonsTexture);
            _spriteBatch.Draw(_coinsTexture, _coin.TextureRectangle, _coin.TextureSource, Color.White);
            _spriteBatch.Draw(_rockTexture, _rock.TextureRectangle, null, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
