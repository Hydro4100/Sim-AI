using MGGameLibrary;
using Microsoft.Xna.Framework;

namespace MainQuest4_DragonDrop
{
    public class SimpleTargetable : ITargetable
    {
        public Vector2 TargetPosition { get; set; }

        public SimpleTargetable(Vector2 pos)
        {
            TargetPosition = pos;
        }
    }
}
