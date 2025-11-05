using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainQuest5_PacManGhost.SceneManager
{
    public class Timer
    {
        private float _totalTime;
        public float TimeRemaining { get; private set; }
        public bool IsFinished => TimeRemaining <= 0;

        public Timer(float time)
        {
            _totalTime = time;
            TimeRemaining = time;
        }

        public void Update(float seconds)
        {
            if (!IsFinished)
            {
                TimeRemaining -= seconds;
            }
        }

        public void Reset()
        {
            TimeRemaining = _totalTime;
        }
    }
}
