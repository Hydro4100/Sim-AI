using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Globalization;

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
        private Color[] _colours;
        private string[] _colourNames;

        private SpriteFont _displayFont;
        private string _displayText = "Hello, Super Stroop!";
        private Color _displayColour = Color.White;
        private string _livesText;
        private Color _livesColour = Color.White;

        private ShapeRequester _shapeRequester;

        private bool _mouseClicked = false;
        private MouseState ms = new MouseState(), oms;

        private int _lives;

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

            _colours = new Color[]
            {
                Color.Aqua, Color.Beige, Color.Black, Color.Blue, Color.Brown, Color.Crimson, Color.DarkGray, Color.Gray, Color.Green, Color.LightBlue, Color.LightGray, Color.LimeGreen, Color.Magenta, Color.Orange, Color.Pink, Color.Purple, Color.Red, Color.White, Color.Yellow
            };

            _colourNames = new string[]
            {
                "Aqua", "Beige", "Black", "Blue", "Brown", "Crimson", "Dark Gray", "Gray", "Green", "Light Blue", "Light Gray", "Lime", "Magenta", "Orange", "Pink", "Purple", "Red", "White", "Yellow"
            };

            _shapeRequester = new ShapeRequester(_shapes, _colours, _colourNames);
            _shapeRequester.GetNewRequest();
        }

        protected override void Update(GameTime gameTime)
        {
            oms = ms;
            ms = Mouse.GetState();
            _mouseClicked = ms.LeftButton != ButtonState.Pressed && oms.LeftButton == ButtonState.Pressed;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //_displayColour = Color.White;
            _displayColour = _shapeRequester.Colour;
            _displayText = $"{_shapeRequester.ColourName} {_shapeRequester.StroopShape}";
            foreach (StroopShape shape in _shapes)
            {
                if (shape.IsInside(Mouse.GetState().Position))
                {
                    _displayColour = shape.Colour;
                    _displayText = $"Mouse over the {shape.ToString()}";

                    if (_mouseClicked)
                    {
                        Click();
                    }
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

        private void Click()
        {
            Debug.WriteLine("clicked!");
        }
    }
}
