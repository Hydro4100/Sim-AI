using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MainQuest4_DragonDrop
{
    public class TruncatedSumSteeringBehaviour : SteeringBehaviour
    {
        private List<SteeringBehaviour> _behaviours;
        private float _maxForce;

        public TruncatedSumSteeringBehaviour(List<SteeringBehaviour> behaviours, float maxForce)
        {
            _behaviours = behaviours;
            _maxForce = maxForce;
        }

        public override Vector2 CalculateSteeringForce(Agent agent)
        {
            Vector2 totalForce = Vector2.Zero;

            foreach (SteeringBehaviour behaviour in _behaviours)
            {
                totalForce += behaviour.CalculateSteeringForce(agent);
            }

            if (totalForce.LengthSquared() > _maxForce * _maxForce)
            {
                totalForce = Vector2.Normalize(totalForce) * _maxForce;
            }

            return totalForce;
        }
    }
}
