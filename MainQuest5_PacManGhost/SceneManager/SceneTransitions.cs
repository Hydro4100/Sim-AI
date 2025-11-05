using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainQuest5_PacManGhost.SceneManager
{
    public class TimedTransition : IStateTransition
    {
        private Timer _timer;

        public TimedTransition(Timer timer)
        {
            _timer = timer;
        }

        public bool ToTransition()
        {
            return _timer.IsFinished;
        }
    }

    public class GameOverTransition : IStateTransition
    {
        private GameScreenState _gameScreen;

        public GameOverTransition(GameScreenState gameScreen)
        {
            _gameScreen = gameScreen;
        }

        public bool ToTransition()
        {
            return _gameScreen.IsPlaying;
        }
    }
}
