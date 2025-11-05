using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainQuest5_PacManGhost.SceneManager
{
    public abstract class ScreenState : GameComponent, IState
    {
        public ScreenState(Game game) : base(game)
        {
            Enabled = false;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public abstract void Draw(GameTime gameTime);
        
        public virtual void OnEnter()
        {
            Enabled = true;
        }

        public virtual void OnExit()
        {
            Enabled = false;
        }

        public abstract void OnUpdate(float seconds);
    }
}
