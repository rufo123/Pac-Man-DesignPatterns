namespace Pac_Man_DesignPatterns.PathFinding.Algorithms
{



    public class DjkistraPathFind : IPathFindingAlgorithm
    {
        public int[] FindShortestPath(int[,] parAdjMatrix, int parSource)
        {

            int tmpCountOfVertices = parAdjMatrix.GetLength(0);
            int[] tmpDist = new int[tmpCountOfVertices];
            int[] tmpPrev = new int[tmpCountOfVertices];

            bool[] tmpSpSet = new bool[tmpCountOfVertices];

            for (int i = 0; i < tmpCountOfVertices; i++)
            {
                tmpPrev[i] = -1;
                tmpDist[i] = int.MaxValue;
            }

            tmpDist[parSource] = 0;


            for (int i = 0; i < tmpCountOfVertices; i++)
            {

                int u = MinDistance(tmpDist, tmpSpSet, tmpCountOfVertices);

                tmpSpSet[u] = true;

                for (int v = 0; v < tmpCountOfVertices; v++)
                {
                    if (!tmpSpSet[v] && parAdjMatrix[u, v] != 0 && tmpDist[u] != int.MaxValue && tmpDist[u] + parAdjMatrix[u, v] < tmpDist[v])
                    {
                        tmpDist[v] = tmpDist[u] + parAdjMatrix[u, v];
                        tmpPrev[v] = u;
                    }


                }
            }



            return tmpPrev;

        }


        private int MinDistance(int[] parDist, bool[] parSpSet, int parLength)
        {
            int tmpMin = int.MaxValue;
            int tmpMinIndex = 0;

            for (int i = 0; i < parLength; i++)
            {
                if (parSpSet[i] == false && parDist[i] <= tmpMin)
                {
                    tmpMin = parDist[i];
                    tmpMinIndex = i;
                }
            }
            return tmpMinIndex;
        }
    }
}
