using MGGameLibrary;
using Microsoft.Xna.Framework;

namespace MainQuest4_DragonDrop
{
    public class FleeBehaviour : SteeringBehaviour
    {
        public ITargetable TargetPosition { get; set; }

        public FleeBehaviour(ITargetable target)
        {
            TargetPosition = target;
        }

        public override Vector2 CalculateSteeringForce(Agent agent)
        {
            Vector2 desiredVelocity = agent.Position - TargetPosition.TargetPosition;

            desiredVelocity.Normalize();
            desiredVelocity *= agent.MaxSpeed;

            return desiredVelocity - agent.Velocity;
        }
    }
}
