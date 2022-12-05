using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_DesignPatterns.PathFinding
{
    public interface IPathFindingAlgorithm
    {

        public int[] FindShortestPath(int[,] parAdjMatrix, int parSourcePosition);

    }
}
