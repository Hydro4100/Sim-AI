using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainQuest5_PacManGhost.SceneManager
{
    public class FlashScreenState : ScreenState
    {
        public Timer Timer {  get; private set; }
        SpriteBatch _spriteBatch;
        SpriteFont _font;
        Texture2D _logo;

        public FlashScreenState(float time, Game game, SpriteBatch sb, SpriteFont font, Texture2D logo) : base(game)
        {
            Timer = new Timer(time);
            _spriteBatch = sb;
            _font = font;
            _logo = logo;
        }

        public override void OnEnter() { Timer.Reset(); base.OnEnter(); }
        public override void OnExit() { base.OnExit(); }
        public override void OnUpdate(float seconds) { Timer.Update(seconds); }
        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.Yellow);

            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, "Flash Screen", new Vector2(100, 100), Color.Black);
            _spriteBatch.Draw(_logo, new Vector2(100, 150), Color.White);
            _spriteBatch.End();
        }
    }

    public class TitleScreenState : ScreenState
    {
        SpriteBatch _spriteBatch;
        SpriteFont _font;

        public TitleScreenState(Game game, SpriteBatch sb, SpriteFont font) : base(game)
        {
            _spriteBatch = sb;
            _font = font;
        }

        public override void OnEnter() { base.OnEnter(); }
        public override void OnExit() { base.OnExit(); }
        public override void OnUpdate(float seconds) { }
        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.Teal);

            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, "Title Screen\n(C - Credits | Space - Game)", new Vector2(100, 100), Color.White);
            _spriteBatch.End();
        }
    }

    public class CreditsScreenState : ScreenState
    {
        SpriteBatch _spriteBatch;
        SpriteFont _font;

        public CreditsScreenState(Game game, SpriteBatch sb, SpriteFont font) : base(game)
        {
            _spriteBatch = sb;
            _font = font;
        }

        public override void OnEnter() { base.OnEnter(); }
        public override void OnExit() { base.OnExit(); }
        public override void OnUpdate(float seconds) { }
        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.MediumPurple);

            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, "Credits Screen", new Vector2(100, 100), Color.White);
            _spriteBatch.End();
        }
    }

    public class GameScreenState : ScreenState
    {
        public bool IsPlaying { get; set; }
        public int Score { get; set; }
        SpriteBatch _spriteBatch;
        SpriteFont _font;
        Texture2D _lightBulbTexture;
        public GameScreenState(Game game, SpriteBatch sb, SpriteFont font, Texture2D lightBulbTexture) : base(game)
        {
            _spriteBatch = sb;
            _font = font;
            _lightBulbTexture = lightBulbTexture;
        }

        public override void OnEnter()
        {
            IsPlaying = true;
            Score = 0;
            base.OnEnter();
        }

        public override void OnExit()
        {
            IsPlaying = false;
            base.OnExit();
        }

        public override void OnUpdate(float seconds) { }
        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.DodgerBlue);

            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, "Game Screen\n(P - Pause | C - Game Over)", new Vector2(100, 100), Color.White);
            _spriteBatch.Draw(_lightBulbTexture, new Vector2(100, 150), new Rectangle(0, 0, 112, 150), Color.White);
            _spriteBatch.End();
        }
    }

    public class PauseScreenState : ScreenState
    {
        SpriteBatch _spriteBatch;
        SpriteFont _font;

        public PauseScreenState(Game game, SpriteBatch sb, SpriteFont font) : base(game)
        {
            _spriteBatch = sb;
            _font = font;
        }

        public override void OnEnter() { base.OnEnter(); }
        public override void OnExit() { base.OnExit(); }
        public override void OnUpdate(float seconds) { }
        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.Gray);

            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, "Paused\n(P - Unpause)", new Vector2(100, 100), Color.White);
            _spriteBatch.End();
        }
    }

    public class GameOverScreenState : ScreenState
    {
        public Timer Timer { get; set; }
        private GameScreenState _gameScreenState;
        SpriteBatch _spriteBatch;
        SpriteFont _font;

        public GameOverScreenState(float time, GameScreenState gameScreenState, Game game, SpriteBatch sb, SpriteFont font) : base(game)
        {
            Timer = new Timer(time);
            _gameScreenState = gameScreenState;
            _spriteBatch = sb;
            _font = font;
        }

        public override void OnEnter() { Timer.Reset(); base.OnEnter(); }
        public override void OnExit() { base.OnExit(); }
        public override void OnUpdate(float seconds) { Timer.Update(seconds); }
        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.Red);

            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, $"Game Over! Score {_gameScreenState.Score}", new Vector2(100, 100), Color.White);
            _spriteBatch.DrawString(_font, "Restarting...", new Vector2(100, 150), Color.White);
            _spriteBatch.End();
        }
    }
}
