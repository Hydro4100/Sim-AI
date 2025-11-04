using MGGameLibrary;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MainQuest4_DragonDrop
{
    internal class PathFollowingBehaviour : SteeringBehaviour
    {
        private List<ITargetable> _pathPoints;
        private int _currentTargetIndex;
        private float _arrivalThreshold;
        private SeekBehaviour _seekBehaviour;

        public PathFollowingBehaviour(List<ITargetable> pathPoints, float arrivalThreshold)
        {
            _pathPoints = pathPoints;
            _arrivalThreshold = arrivalThreshold;
            _currentTargetIndex = 0;

            if (_pathPoints != null && _pathPoints.Count > 0)
            {
                _seekBehaviour = new SeekBehaviour(_pathPoints[_currentTargetIndex]);
            }
        }

        public override Vector2 CalculateSteeringForce(Agent agent)
        {
            if (_pathPoints == null || _pathPoints.Count == 0)
            {
                return Vector2.Zero;
            }

            float distanceToTarget = Vector2.DistanceSquared(agent.Position, _pathPoints[_currentTargetIndex].TargetPosition);

            if (distanceToTarget < _arrivalThreshold * _arrivalThreshold)
            {
                _currentTargetIndex++;

                if (_currentTargetIndex >= _pathPoints.Count)
                {
                    _currentTargetIndex = 0;
                }

                _seekBehaviour.TargetPosition = _pathPoints[_currentTargetIndex];
            }

            return _seekBehaviour.CalculateSteeringForce(agent);
        }
    }
}
