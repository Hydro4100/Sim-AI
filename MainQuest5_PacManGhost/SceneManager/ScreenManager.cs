using MGGameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MainQuest5_PacManGhost.SceneManager
{
    public class ScreenManager : GameComponent
    {
        private FiniteStateMachine<ScreenState, IStateTransition> _fsm;
        private SpriteBatch _spriteBatch;
        private Texture2D _lightBulbTexture;
        private Texture2D _logoTexture;
        private SpriteFont _titleFont;

        public ScreenManager(Game game) : base(game)
        {
            UpdateOrder = -1;
        }

        public void LoadContent()
        {
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _lightBulbTexture = Game.Content.Load<Texture2D>("lightBulb");
            _logoTexture = Game.Content.Load<Texture2D>("logo");
            _titleFont = Game.Content.Load<SpriteFont>("Title");

            FlashScreenState flashScreen = new FlashScreenState(3.0f, Game, _spriteBatch, _titleFont, _logoTexture);
            TitleScreenState titleScreen = new TitleScreenState(Game, _spriteBatch, _titleFont);
            CreditsScreenState creditsScreen = new CreditsScreenState(Game, _spriteBatch, _titleFont);
            GameScreenState gameScreen = new GameScreenState(Game, _spriteBatch, _titleFont, _lightBulbTexture);
            PauseScreenState pauseScreen = new PauseScreenState(Game, _spriteBatch, _titleFont);
            GameOverScreenState gameOverScreen = new GameOverScreenState(3.0f, gameScreen, Game, _spriteBatch, _titleFont);

            Game.Components.Add(flashScreen);
            Game.Components.Add(titleScreen);
            Game.Components.Add(creditsScreen);
            Game.Components.Add(pauseScreen);
            Game.Components.Add(gameScreen);
            Game.Components.Add(gameOverScreen);

            KeyPressTransition pressC = new KeyPressTransition(Keys.C);
            KeyPressTransition pressP = new KeyPressTransition(Keys.P);
            KeyPressTransition pressSpace = new KeyPressTransition(Keys.Space);

            TimedTransition timedFlashScreenTransition = new TimedTransition(flashScreen.Timer);
            TimedTransition timedGameOverScreenTransition = new TimedTransition(gameOverScreen.Timer);

            var graph = new SparseGraph<ScreenState, IStateTransition>();

            graph.AddEdge(flashScreen, timedFlashScreenTransition, titleScreen);
            graph.AddEdge(titleScreen, pressC, creditsScreen);
            graph.AddEdge(creditsScreen, pressC, titleScreen);
            graph.AddEdge(titleScreen, pressSpace, gameScreen);
            graph.AddEdge(gameScreen, pressP, pauseScreen);
            graph.AddEdge(pauseScreen, pressP, gameScreen);
            graph.AddEdge(gameScreen, pressC, gameOverScreen);
            graph.AddEdge(gameOverScreen, timedGameOverScreenTransition, titleScreen);

            _fsm = new FiniteStateMachine<ScreenState, IStateTransition>(graph, flashScreen);

            _fsm.CurrentState.OnEnter();
        }

        public override void Update(GameTime gameTime)
        {
            float seconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _fsm.Update(seconds);

            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            if (_fsm.CurrentState != null)
            {
                _fsm.CurrentState.Draw(gameTime);
            }
        }
    }
}
