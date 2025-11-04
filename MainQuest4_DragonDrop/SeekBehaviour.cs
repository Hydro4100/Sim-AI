using MGGameLibrary;
using Microsoft.Xna.Framework;

namespace MainQuest4_DragonDrop
{
    public class SeekBehaviour : SteeringBehaviour
    {
        private ITargetable _target;

        public SeekBehaviour(ITargetable target)
        {
            _target = target;
        }

        public ITargetable Target { get { return _target; } }
        public Vector2 TargetPosition { get { return _target.TargetPosition; } }


        public override Vector2 CalculateSteeringForce(Agent agent)
        {
            Vector2 desiredVelocity = TargetPosition - agent.Position;

            desiredVelocity.Normalize();
            desiredVelocity *= agent.MaxSpeed;

            return desiredVelocity - agent.Velocity;
        }
    }
}
