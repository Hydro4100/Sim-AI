using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainQuest6_TreasureHunter
{
    public class NavMeshEdge
    {
        public float Cost { get; private set; }
        public NavMeshEdge(float cost)
        {
            Cost = cost;
        }
    }
}
