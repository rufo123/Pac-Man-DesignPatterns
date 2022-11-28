﻿using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_DesignPatterns.PathFinding.Algorithms
{



    public class DjkistraPathFind : IPathFindingAlgorithm
    {

        private const int INFINITY = -1;
        private const int UNDEFINED = -1;

        public int[] FindShortestPath(int[,] parMazeGraph, int parSource)
        {

            int[,] tmpAdjGraph = ReturnAdjMatrix(parMazeGraph, parMazeGraph.GetLength(0), parMazeGraph.GetLength(1));

            int tmpCountOfVertices = tmpAdjGraph.GetLength(0);
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
                    if (!tmpSpSet[v] && tmpAdjGraph[u, v] != 0 && tmpDist[u] != int.MaxValue && tmpDist[u] + tmpAdjGraph[u, v] < tmpDist[v])
                    {
                        tmpDist[v] = tmpDist[u] + tmpAdjGraph[u,v];
                        tmpPrev[v] = u; 
                    }


                }
            }



            return tmpPrev;

        }

        private int[,] ReturnAdjMatrix(int[,] originalMatrix, int parWidth, int parHeight) {



            parWidth = originalMatrix.GetLength(0);
            parHeight = originalMatrix.GetLength(1);

            int n = parWidth;
            int m = parHeight;

            int[,] tmpAdj = new int[parWidth * parHeight, parWidth * parHeight];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (originalMatrix[i, j] > -1) {

                        int x = (i - 1 < 0) ? n - 1 : i - 1;
                        int y = (i + 1 > n - 1) ? 0 : i + 1;
                        int z = (j - 1 < 0) ? m - 1 : j - 1;
                        int w = (j + 1 > m - 1) ? 0 : j + 1;

                        if (originalMatrix[x,j] > 0) tmpAdj[i * m + j,x * m + j] = 1;
                        if (originalMatrix[y,j] > 0) tmpAdj[i * m + j,y * m + j] = 1;
                        if (originalMatrix[i,z] > 0) tmpAdj[i * m + j,i * m + z] = 1;
                        if (originalMatrix[i,w] > 0) tmpAdj[i * m + j,i * m + w] = 1;

                    }
                }
            }
            //printMatrix(tmpAdj);

            return tmpAdj;
        }

        void printMatrix(int[,] parAdj)
        {

            for (int i = 0; i < parAdj.GetLength(0); i++)
            {
                for (int j = 0; j < parAdj.GetLength(1); j++)
                {
                    Debug.Write(parAdj[i,j]);
                }
                Debug.WriteLine("");
            }
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

        public int[] GetNeighbours(int tmpNode, int[,] parMazeGraph)
        {
            int[] tmpHelperArray = new int[] { tmpNode - 1, tmpNode + 1, tmpNode + parMazeGraph.GetLength(0), tmpNode - parMazeGraph.GetLength(1) };

            List<int> tmpHelperList = new List<int>(4);

            for (int i = 0; i < tmpHelperArray.Length; i++)
            {
                if (tmpHelperArray[i] > -1 && tmpHelperArray[i] < parMazeGraph.Length && (int)parMazeGraph.GetValue(tmpHelperArray[i] % parMazeGraph.GetLength(0), tmpHelperArray[i] / parMazeGraph.GetLength(0)) != -1)
                {
                    tmpHelperList.Add(tmpHelperArray[i]);
                }
            }

            return tmpHelperList.ToArray();
        }

        public int GetDistanceNeighbour(int parSource, int parNeighbour, int[,] parMazeGraph)
        {

            int sourceValue = (int)parMazeGraph.GetValue(parSource % parMazeGraph.GetLength(0), parSource / parMazeGraph.GetLength(0));
            int neighbourValue = (int)parMazeGraph.GetValue(parNeighbour % parMazeGraph.GetLength(0), parNeighbour / parMazeGraph.GetLength(0));

            if (sourceValue == -1 || neighbourValue == -1)
            {
                return -1;
            }

            return 1;

        }




    }
}