using Microsoft.Xna.Framework.Input;

namespace MainQuest5_PacManGhost
{
    public class KeyReleaseTransition : IStateTransition
    {
        private bool _oldKeyState;
        public Keys Key { get; private set; }

        public KeyReleaseTransition(Keys key)
        {
            Key = key;
            _oldKeyState = Keyboard.GetState().IsKeyDown(Key);
        }

        public bool ToTransition()
        {
            bool currentKeyState = Keyboard.GetState().IsKeyDown(Key);
            if (_oldKeyState && !currentKeyState)
            {
                _oldKeyState = false;
                return true;
            }

            _oldKeyState = currentKeyState;
            return false;
        }
    }
}
