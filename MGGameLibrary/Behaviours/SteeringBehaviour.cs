using Microsoft.Xna.Framework;

namespace MGGameLibrary.Behaviours
{
    public abstract class SteeringBehaviour
    {
        public abstract Vector2 CalculateSteeringForce(ISteerable agent);
    }
}
