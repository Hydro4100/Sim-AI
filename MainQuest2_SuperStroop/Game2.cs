using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MainQuest2_SuperStroop
{
    public class Game2 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _rectangleTexture;
        private Texture2D _circleTexture;
        private Texture2D _triangleTexture;
        private List<Texture2D> _shapeTextures;

        private List<StroopShape> _shapes;
        private Color[] _colours;
        private string[] _colourNames;

        private SpriteFont _displayFont;
        private string _displayText = "Hello, Super Stroop!";
        private Color _displayColour = Color.White;
        private Color _textColour = Color.White;
        private string _livesText;
        private string _scoreText;

        private ShapeRequester _shapeRequester;

        private bool _mouseClicked = false;
        private MouseState ms = new MouseState(), oms;

        private int _lives = 3;
        private int _score = 0;
        private float _time = 3.5f;
        private float _timeRemaining = 3.5f;

        private static Random _random = new Random();

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
            _rectangleTexture.Name = "square";

            _circleTexture = Content.Load<Texture2D>("circle");
            _triangleTexture = Content.Load<Texture2D>("triangle");
            _displayFont = Content.Load<SpriteFont>("displayFont");

            _shapeTextures = new List<Texture2D> { _rectangleTexture, _circleTexture, _triangleTexture };

            _colours = new Color[]
            {
                Color.Aqua, Color.Beige, Color.Black, Color.Blue, Color.Brown, Color.Crimson, Color.DarkGray, Color.Gray, Color.Green, Color.LightBlue, Color.LightGray, Color.LimeGreen, Color.Magenta, Color.Orange, Color.Pink, Color.Purple, Color.Red, Color.White, Color.Yellow
            };

            _colourNames = new string[]
            {
                "Aqua", "Beige", "Black", "Blue", "Brown", "Crimson", "Dark Gray", "Gray", "Green", "Light Blue", "Light Gray", "Lime", "Magenta", "Orange", "Pink", "Purple", "Red", "White", "Yellow"
            };

            _shapes = new List<StroopShape>();
            for (int i = 0; i < 3; i++)
            {
                StroopShape shape = CreateUniqueShape();
                _shapes.Add(shape);
                Components.Add(shape);
            }

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

            _displayColour = _shapeRequester.Colour;
            _displayText = $"{_shapeRequester.ColourName} {_shapeRequester.StroopShape} {MathF.Round(_timeRemaining, 1)}";
            _livesText = $"{_lives} Lives";
            _scoreText = $"{_score} Points";

            if (_mouseClicked)
            {
                var clickedShapes = new List<StroopShape>();
                foreach (var shape in _shapes)
                {
                    if (shape.IsInside(ms.Position))
                    {
                        clickedShapes.Add(shape);
                    }
                }

                if (clickedShapes.Count > 0)
                {
                    if (clickedShapes.Contains(_shapeRequester.StroopShape))
                    {
                        Correct();
                    }
                    else
                    {
                        Fail();
                    }
                }
            }

            _timeRemaining -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timeRemaining <= 0)
            {
                Fail();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.DrawString(_displayFont, _displayText, new Vector2((_graphics.GraphicsDevice.Viewport.Width - _displayFont.MeasureString(_displayText).X) / 2, 10), _displayColour);
            _spriteBatch.DrawString(_displayFont, _livesText, new Vector2(10, 10), _textColour);
            _spriteBatch.DrawString(_displayFont, _scoreText, new Vector2(_graphics.GraphicsDevice.Viewport.Width - _displayFont.MeasureString(_scoreText).X - 10, 10), _textColour);

            foreach (var shape in _shapes)
            {
                shape.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private StroopShape CreateUniqueShape()
        {
            int maxCombinations = _shapeTextures.Count * _colours.Length;
            if (_shapes.Count >= maxCombinations)
            {
                Console.WriteLine("Warning: All possible shape combinations are on screen!");
                return new StroopShape(this, _colours[0], _shapeTextures[0]);
            }

            while (true)
            {
                Texture2D candidateTexture = _shapeTextures[_random.Next(_shapeTextures.Count)];
                Color candidateColor = _colours[_random.Next(_colours.Length)];

                bool isDuplicate = false;
                foreach (var existingShape in _shapes)
                {
                    if (existingShape.Texture == candidateTexture && existingShape.Colour == candidateColor)
                    {
                        isDuplicate = true;
                        break;
                    }
                }

                if (!isDuplicate)
                {
                    return new StroopShape(this, candidateColor, candidateTexture);
                }
            }
        }

        private void AddTwoShapes()
        {
            for (int i = 0; i < 2; i++)
            {
                StroopShape newShape = CreateUniqueShape();
                _shapes.Add(newShape);
                Components.Add(newShape);
            }
        }

        protected void Correct()
        {
            _score += 100;
            //_time++;
            AddTwoShapes();
            Reset();
        }

        protected void Fail()
        {
            _lives--;
            Reset();
        }

        protected void Reset()
        {
            _timeRemaining = _time;
            _shapeRequester.GetNewRequest();
        }
    }
}
