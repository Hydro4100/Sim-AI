using MainQuest5_PacManGhost.SceneManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MainQuest5_PacManGhost
{
    public class Game5 : Game
    {
        private GraphicsDeviceManager _graphics;

        private ScreenManager _screenManager;

        public Game5()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _screenManager = new ScreenManager(this);
            Components.Add(_screenManager);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _screenManager.LoadContent();
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
            _screenManager.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
