using MGGameLibrary;
using MGGameLibrary.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        private List<Disc> _discsList = new List<Disc> { };

        private const float GRAVITY_ACCELERATION = 250f;

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

            _obstaclesList.Add(new Obstacle(new Circle(new Vector2(10, GraphicsDevice.Viewport.Height / 2), 40)));
            _obstaclesList.Add(new Obstacle(new Circle(new Vector2(500, GraphicsDevice.Viewport.Height / 2), 40)));

            _discsList.Add(new Disc(Disc.DISC_MASS, new Vector2(100, GraphicsDevice.Viewport.Height / 2), this));
            _discsList.Add(new Disc(Disc.DISC_MASS, new Vector2(400, GraphicsDevice.Viewport.Height / 2), this));

            _discsList[0].ApplyImpulse(new Vector2(100, 0), (float)TargetElapsedTime.TotalSeconds);
            _discsList[1].ApplyImpulse(new Vector2(-100, 0), (float)TargetElapsedTime.TotalSeconds);

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

            Vector2 collisionNormal = Vector2.Zero;
            for (int i = 0; i < _orbsList.Count; ++i)
            {
                foreach (Obstacle obstacle in _obstaclesList)
                {
                    if (obstacle.CollidesWith(_orbsList[i], ref collisionNormal))
                    {
                        _orbsList[i].RevertToPreviousPosition();
                        _orbsList[i].Velocity = Vector2.Reflect(_orbsList[i].Velocity, collisionNormal);
                    }
                }
            }


            for (int i = 0; i < _discsList.Count; ++i)
            {
                Disc disc = _discsList[i];

                //disc.ApplyGravity();

                disc.Update((float)TargetElapsedTime.TotalSeconds);

                foreach (Obstacle obstacle in _obstaclesList)
                {
                    if (obstacle.CollidesWith(disc, ref collisionNormal))
                    {
                        disc.RevertToPreviousPosition();
                        Vector2 desiredVelocity = Vector2.Reflect(disc.Velocity, collisionNormal);
                        float deltaTime = (float)TargetElapsedTime.TotalSeconds;
                        disc.ApplyImpulse(desiredVelocity - disc.Velocity, deltaTime);
                    }
                }

                for (int j = i + 1; j < _discsList.Count; j++)
                {
                    Disc otherDisc = _discsList[j];
                    Vector2 normal = Vector2.Zero;

                    if (disc.CollidesWith(otherDisc, ref normal))
                    {
                        disc.Velocity = Vector2.Zero;
                        otherDisc.Velocity = Vector2.Zero;
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

            foreach (Disc disc in _discsList)
            {
                Circle circle = disc.Shape as Circle;
                Rectangle r = new Rectangle((int)disc.Position.X, (int)disc.Position.Y, (int)circle.Radius * 2, (int)circle.Radius * 2);

                _spriteBatch.Draw(_textures, r, new Rectangle(512, 0, 256, 256), Color.Red);
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
