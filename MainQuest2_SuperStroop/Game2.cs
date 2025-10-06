using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MainQuest2_SuperStroop
{
    public class Game2 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _rectangleTexture;
        private Texture2D _circleTexture;
        private Texture2D _triangleTexture;

        private StroopShape[] _shapes;

        private SpriteFont _displayFont;
        private string _displayText = "Hello, Super Stroop!";
        private Color _displayColour = Color.White;

        public Game2()
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

            _rectangleTexture = new Texture2D(GraphicsDevice, 1, 1);
            _rectangleTexture.SetData(new Color[] { Color.White });
            _rectangleTexture.Name = "rectangle";

            _circleTexture = Content.Load<Texture2D>("circle");
            _triangleTexture = Content.Load<Texture2D>("triangle");
            _displayFont = Content.Load<SpriteFont>("displayFont");

            _shapes = new StroopShape[]
            {
                new StroopCircle(new Rectangle(80, 70, 70, 70), Color.Red, _circleTexture),
                new StroopTriangle(new Rectangle(170, 70, 80, 80), Color.Green, _triangleTexture),
                new StroopSquare(new Rectangle(130, 170, 60, 60), Color.Blue, _rectangleTexture),
            };
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _displayColour = Color.White;
            _displayText = "Mouse over nothing";
            foreach (StroopShape shape in _shapes)
            {
                if (shape.IsInside(Mouse.GetState().Position))
                {
                    _displayColour = shape.Colour;
                    _displayText = $"Mouse over the {shape.ToString()}";
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.DrawString(_displayFont, _displayText, new Vector2((_graphics.GraphicsDevice.Viewport.Width - _displayFont.MeasureString(_displayText).X) / 2, 10), _displayColour);

            foreach (var shape in _shapes)
            {
                shape.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
