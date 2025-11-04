using Microsoft.Xna.Framework;

namespace MainQuest4_DragonDrop
{
    public abstract class SteeringBehaviour
    {
        public abstract Vector2 CalculateSteeringForce(Agent agent);
    }
}
