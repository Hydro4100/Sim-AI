using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MGGameLibrary;

namespace MainQuest5_PacManGhost
{
    public class LightBulb : GameComponent
    {
        private FiniteStateMachine<LightBulbState, IStateTransition> _fsm;

        public Rectangle SourceRectangle
        {
            get { return _fsm.CurrentState.SourceRectangle; }
        }

        public LightBulb(Game game) : base(game)
        {
            LightBulbOffState offState = new LightBulbOffState(new Rectangle(0, 0, 112, 150));
            LightBulbOnState onState = new LightBulbOnState(new Rectangle(112, 0, 112, 150));

            KeyPressTransition turnOnTransition = new KeyPressTransition(Keys.O);
            KeyReleaseTransition turnOffTransition = new KeyReleaseTransition(Keys.O);

            SparseGraph<LightBulbState, IStateTransition> graph = new SparseGraph<LightBulbState, IStateTransition>();

            graph.AddNode(offState);
            graph.AddNode(onState);

            graph.AddEdge(offState, turnOnTransition, onState);
            graph.AddEdge(onState, turnOffTransition, offState);

            _fsm = new FiniteStateMachine<LightBulbState, IStateTransition>(graph, offState);
            _fsm.CurrentState.OnEnter();
        }

        public override void Update(GameTime gameTime)
        {
            float seconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _fsm.Update(seconds);

            base.Update(gameTime);
        }
    }
}
