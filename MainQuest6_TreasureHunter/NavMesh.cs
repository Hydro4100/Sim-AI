using MGGameLibrary;

namespace MainQuest6_TreasureHunter
{
    public class NavMesh
    {
        public SparseGraph<NavMeshNode, NavMeshEdge> Graph;

        public NavMesh()
        {
            Graph = new SparseGraph<NavMeshNode, NavMeshEdge>();
        }
    }
}
