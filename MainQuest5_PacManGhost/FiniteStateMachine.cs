using MGGameLibrary;
namespace MainQuest5_PacManGhost
{
    public class FiniteStateMachine<TNode, TEdge>
        where TNode : IState
        where TEdge : IStateTransition
    {
        private SparseGraph<TNode, TEdge> _fsm;
        public TNode CurrentState { get; private set; }

        public FiniteStateMachine(SparseGraph<TNode, TEdge> fsm, TNode currentState)
        {
            _fsm = fsm;
            CurrentState = currentState;
        }

        public void Update(float seconds)
        {
            var edges = _fsm.GetEdges(CurrentState);

            foreach ((TEdge, TNode) edge in edges)
            {
                if (edge.Item1.ToTransition())
                {
                    CurrentState.OnExit();
                    CurrentState = edge.Item2;
                    CurrentState.OnEnter();
                }
            }

            if (CurrentState != null)
            {
                CurrentState.OnUpdate(seconds);
            }
        }
    }
}
