using System.Collections.Generic;
using System.Linq;

namespace MGGameLibrary
{
    public class SparseGraph<TNode, TEdge>
    {
        private Dictionary<TNode, List<(TEdge edge, TNode node)>> _adjacency;

        public SparseGraph()
        {
            _adjacency = new Dictionary<TNode, List<(TEdge edge, TNode node)>>();
        }

        public void AddNode(TNode node)
        {
            if (!_adjacency.ContainsKey(node))
            {
                _adjacency[node] = new List<(TEdge edge, TNode node)>();
            }
        }

        public void AddEdge(TNode from, TEdge edge, TNode to)
        {
            AddNode(from);
            AddNode(to);

            _adjacency[from].Add((edge, to));
        }

        public IEnumerable<(TEdge edge, TNode node)> GetEdges(TNode node)
        {
            if (_adjacency.ContainsKey(node))
            {
                return _adjacency[node];
            }

            return Enumerable.Empty<(TEdge edge, TNode node)>();
        }

        public ICollection<TNode> GetAllNodes()
        {
            return _adjacency.Keys;
        }
    }
}
