using MGGameLibrary;
using MGGameLibrary.Shapes;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace MainQuest4_DragonDrop
{
    public class AvoidCollidableWithWhiskersBehaviour : SteeringBehaviour
    {
        private List<ICollidable> _collidableList;
        private List<Vector2> _whiskers;

        public AvoidCollidableWithWhiskersBehaviour(List<ICollidable> collidableList, List<Vector2> whiskers)
        {
            _collidableList = collidableList;
            _whiskers = whiskers;
        }

        public override Vector2 CalculateSteeringForce(Agent agent)
        {
            Vector2 totalForce = Vector2.Zero;

            Matrix rotation = Matrix.CreateRotationZ(agent.Heading);

            foreach (ICollidable collidable in _collidableList)
            {
                if (collidable.Shape is Circle)
                {
                    Circle circle = (Circle)collidable.Shape;

                    foreach (Vector2 whisker in _whiskers)
                    {
                        Vector2 rotatedWhisker = Vector2.Transform(whisker, rotation);

                        LineSegment whiskerLine = new LineSegment(agent.Position, agent.Position + rotatedWhisker);

                        if (Shape.Intersects(whiskerLine, circle))
                        {
                            Vector2 circleCenter = circle.Position + new Vector2(circle.Radius);

                            Vector2 desiredVelocity = agent.Position - circleCenter;
                            desiredVelocity.Normalize();
                            desiredVelocity *= agent.MaxSpeed;

                            totalForce += desiredVelocity - agent.Velocity;
                        }
                    }
                }
            }

            return totalForce;
        }
    }
}
