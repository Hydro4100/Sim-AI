using MGGameLibrary;
using MGGameLibrary.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MainQuest3_AztecDeflect
{
    public class Game3 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private PlayerShip _playerShip;
        private Texture2D _textures;

        private Rectangle ShipRect;

        private List<EnergyOrb> _orbsList = new List<EnergyOrb> { };

        private List<ICollidable> _obstaclesList = new List<ICollidable> { };

        public Game3()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _playerShip = new PlayerShip(this);
            Components.Add(_playerShip);

            _obstaclesList.Add(new Obstacle(new Circle(new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), 30)));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _textures = Content.Load<Texture2D>("textures");
            ShipRect = new Rectangle(0, 0, 256, 256);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                EnergyOrb orb = _playerShip.FireEnergyOrb();
                if (orb != null)
                {
                    _orbsList.Add(orb);
                    Components.Add(orb);
                }
            }

            for (int i = 0; i < _orbsList.Count; ++i)
            {
                foreach (Obstacle obstacle in _obstaclesList)
                {
                    if (obstacle.CollidesWith(_orbsList[i]))
                    {
                        _orbsList[i].Velocity = Vector2.Zero;
                    }
                }
            }

            RemoveOrbs();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_textures, _playerShip.Rectangle, ShipRect, Color.White);

            foreach (EnergyOrb energyOrb in _orbsList)
            {
                _spriteBatch.Draw(_textures, energyOrb.Rectangle, new Rectangle(512, 0, 256, 256), Color.OrangeRed);
            }

            foreach (ICollidable obstacle in _obstaclesList)
            {
                if (obstacle.Shape is Circle circle)
                {
                    Rectangle r = new Rectangle((int)(circle.Centre.X - circle.Radius), (int)(circle.Centre.Y - circle.Radius), (int)circle.Radius * 2, (int)circle.Radius * 2);

                    _spriteBatch.Draw(_textures, r, new Rectangle(256, 0, 256, 256), Color.White);
                }
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void RemoveOrbs()
        {
            for (int i = _orbsList.Count - 1; i >= 0; --i)
            {
                if (_orbsList[i].Position.X < 0 ||
                    _orbsList[i].Position.X > GraphicsDevice.Viewport.Width ||
                    _orbsList[i].Position.Y < 0 ||
                    _orbsList[i].Position.Y > GraphicsDevice.Viewport.Height)
                {
                    Components.Remove(_orbsList[i]);
                    _orbsList.Remove(_orbsList[i]);
                }
            }
        }
    }
}
