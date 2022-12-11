namespace Pac_Man_DesignPatterns.PathFinding
{
    public interface IPathFindingAlgorithm
    {

        public int[] FindShortestPath(int[,] parAdjMatrix, int parSourcePosition);

    }
}
