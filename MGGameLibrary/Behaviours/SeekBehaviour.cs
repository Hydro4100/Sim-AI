using Microsoft.Xna.Framework;

namespace MGGameLibrary.Behaviours
{
    public class SeekBehaviour : SteeringBehaviour
    {
        public ITargetable TargetPosition { get; set; }

        public SeekBehaviour(ITargetable target)
        {
            TargetPosition = target;
        }

        public override Vector2 CalculateSteeringForce(ISteerable agent)
        {
            Vector2 desiredVelocity = TargetPosition.TargetPosition - agent.Position;

            desiredVelocity.Normalize();
            desiredVelocity *= agent.MaxSpeed;

            return desiredVelocity - agent.Velocity;
        }
    }
}
