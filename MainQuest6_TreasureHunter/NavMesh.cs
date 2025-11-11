using MGGameLibrary;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MainQuest6_TreasureHunter
{
    public class NavMesh
    {
        public SparseGraph<NavMeshNode, NavMeshEdge> Graph;

        public NavMesh()
        {
            Graph = new SparseGraph<NavMeshNode, NavMeshEdge>();
        }

        public List<Vector2> GetDijkstraPath((Vector2 from, Vector2 to) _pathEnds)
        {
            List<Vector2> result = new();

            NavMeshNode fromNode = PointInsideNode(_pathEnds.from);
            NavMeshNode toNode = PointInsideNode(_pathEnds.to);

            if (fromNode == null || toNode == null)
            {
                return result;
            }

            if (fromNode == toNode)
            {
                result.Add(_pathEnds.from);
                result.Add(_pathEnds.to);
                return result;
            }

            Dictionary<NavMeshNode, float> distances = new();
            Dictionary<NavMeshNode, NavMeshNode> previous = new();
            PriorityQueue<NavMeshNode, float> queue = new();

            foreach (NavMeshNode node in Graph.GetAllNodes())
            {
                distances[node] = float.MaxValue;
                previous[node] = null;
            }

            distances[fromNode] = 0;
            queue.Enqueue(fromNode, 0);

            while (queue.Count > 0)
            {
                NavMeshNode currentNode = queue.Dequeue();

                if (currentNode == toNode)
                {
                    break;
                }

                float currentDist = distances[currentNode];

                foreach ((NavMeshEdge edge, NavMeshNode neighbour) in Graph.GetEdges(currentNode))
                {
                    float alt = currentDist + edge.Cost;
                    if (alt < distances[neighbour])
                    {
                        distances[neighbour] = alt;
                        previous[neighbour] = currentNode;
                        queue.Enqueue(neighbour, alt);
                    }
                }
            }

            result.Add(_pathEnds.to);
            NavMeshNode nextNode = toNode;
            while (nextNode != null)
            {
                result.Add(nextNode.Centre);
                nextNode = previous[nextNode];
            }
            result.Add(_pathEnds.from);

            result.Reverse();

            return result;
        }

        public NavMeshNode PointInsideNode(Vector2 point)
        {
            foreach (NavMeshNode node in Graph.GetAllNodes())
            {
                if (node.Rectangle.Contains(point))
                {
                    return node;
                }
            }
            return null;
        }
    }
}
