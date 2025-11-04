using Microsoft.Xna.Framework;

namespace MGGameLibrary.Behaviours
{
    public class FleeBehaviour : SteeringBehaviour
    {
        public ITargetable TargetPosition { get; set; }

        public FleeBehaviour(ITargetable target)
        {
            TargetPosition = target;
        }

        public override Vector2 CalculateSteeringForce(ISteerable agent)
        {
            Vector2 desiredVelocity = agent.Position - TargetPosition.TargetPosition;

            desiredVelocity.Normalize();
            desiredVelocity *= agent.MaxSpeed;

            return desiredVelocity - agent.Velocity;
        }
    }
}
