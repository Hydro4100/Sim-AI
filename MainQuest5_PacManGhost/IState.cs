namespace MainQuest5_PacManGhost
{
    public interface IState
    {
        public void OnEnter();
        public void OnExit();
        public void OnUpdate(float seconds);
    }
}
