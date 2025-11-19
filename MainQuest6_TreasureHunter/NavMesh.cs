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

        public List<Vector2> GetAStarPath((Vector2 from, Vector2 to) _pathEnds)
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

            var gScore = new Dictionary<NavMeshNode, float>();
            var fScore = new Dictionary<NavMeshNode, float>();
            var previous = new Dictionary<NavMeshNode, NavMeshNode>();
            var queue = new PriorityQueue<NavMeshNode, float>();

            foreach (NavMeshNode node in Graph.GetAllNodes())
            {
                gScore[node] = float.MaxValue;
                fScore[node] = float.MaxValue;
                previous[node] = null;
            }

            gScore[fromNode] = 0;
            fScore[fromNode] = Vector2.Distance(fromNode.Centre, toNode.Centre);
            queue.Enqueue(fromNode, fScore[fromNode]);

            while (queue.Count > 0)
            {
                NavMeshNode current = queue.Dequeue();

                if (current == toNode)
                {
                    break;
                }

                foreach ((NavMeshEdge edge, NavMeshNode neighbor) in Graph.GetEdges(current))
                {
                    float tentativeG = gScore[current] + edge.Cost;

                    if (tentativeG < gScore[neighbor])
                    {
                        gScore[neighbor] = tentativeG;
                        fScore[neighbor] = tentativeG + Vector2.Distance(neighbor.Centre, toNode.Centre);

                        previous[neighbor] = current;
                        queue.Enqueue(neighbor, fScore[neighbor]);
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
    }
}
