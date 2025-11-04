using Microsoft.Xna.Framework;

namespace MainQuest5_PacManGhost
{
    public class LightBulbState : IState
    {
        public Rectangle SourceRectangle { get; protected set; }

        public LightBulbState(Rectangle sourceRectangle)
        {
            SourceRectangle = sourceRectangle;
        }
        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public virtual void OnUpdate(float seconds) { }
    }
}
