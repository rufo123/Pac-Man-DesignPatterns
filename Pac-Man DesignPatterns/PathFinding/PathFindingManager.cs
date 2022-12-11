using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Entities;

namespace Pac_Man_DesignPatterns.PathFinding
{

    public class PathFindingManager
    {

        private readonly int aMazeWidth;

        private readonly int aMazeHeight;

        private readonly int aTilesScale;

        private int[,] aAdjMatrix;



        public PathFindingManager(int parMazeWidth, int parMazeHeight, Entity[] parListWallEntities, int parTilesScale)
        {
            aMazeWidth = parMazeWidth;
            aMazeHeight = parMazeHeight;
            int[,] tmpMazeGraph = new int[parMazeWidth, parMazeHeight];
            aTilesScale = parTilesScale;

            for (int i = 0; i < tmpMazeGraph.GetLength(0); i++)
            {
                for (int j = 0; j < tmpMazeGraph.GetLength(1); j++)
                {
                    tmpMazeGraph[i, j] = 1;
                }
            }

            foreach (var entity in parListWallEntities)
            {
                int tmpX = (int)(entity.Position.X / aTilesScale);
                int tmpY = (int)(entity.Position.Y / aTilesScale);

                tmpMazeGraph[tmpX, tmpY] = -1;
            }

            ConstructAdjMatrix(tmpMazeGraph);
        }

        private void ConstructAdjMatrix(int[,] parOriginalMatrix)
        {

            int tmpWidth = parOriginalMatrix.GetLength(0);
            int tmpHeight = parOriginalMatrix.GetLength(1);

            int n = tmpWidth;
            int m = tmpHeight;

            int[,] tmpAdj = new int[tmpWidth * tmpHeight, tmpWidth * tmpHeight];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (parOriginalMatrix[i, j] > -1)
                    {

                        int x = (i - 1 < 0) ? n - 1 : i - 1;
                        int y = (i + 1 > n - 1) ? 0 : i + 1;
                        int z = (j - 1 < 0) ? m - 1 : j - 1;
                        int w = (j + 1 > m - 1) ? 0 : j + 1;

                        if (parOriginalMatrix[x, j] > 0) tmpAdj[i * m + j, x * m + j] = 1;
                        if (parOriginalMatrix[y, j] > 0) tmpAdj[i * m + j, y * m + j] = 1;
                        if (parOriginalMatrix[i, z] > 0) tmpAdj[i * m + j, i * m + z] = 1;
                        if (parOriginalMatrix[i, w] > 0) tmpAdj[i * m + j, i * m + w] = 1;

                    }
                }
            }
            //printMatrix(tmpAdj);

            aAdjMatrix = tmpAdj;
        }



        public int[] GetShortestPath(Vector2 parSource, IPathFindingAlgorithm parPathFindingAlgorithm)
        {
            if (aAdjMatrix is null)
            {
                throw new NullReferenceException("Adjacency Matrix Is Not Constructed!");
            }

            int tmpConvX = ((int)parSource.X / aTilesScale) % aMazeWidth;
            int tmpConvY = ((int)parSource.Y / aTilesScale) % aMazeHeight;

            if (tmpConvX > 0 && tmpConvX < aMazeWidth)
            {
                if (tmpConvY > 0 && tmpConvY < aMazeHeight)
                {
                    return parPathFindingAlgorithm.FindShortestPath(aAdjMatrix, (tmpConvY) + (tmpConvX * aMazeHeight));
                }
            }

            return null;
        }

        public int[] ConstructPath(Vector2 parTarget, int[] parPath)
        {

            int tmpConvX = ((int)parTarget.X / aTilesScale) % aMazeWidth;
            int tmpConvY = ((int)parTarget.Y / aTilesScale) % aMazeHeight;

            int tmpTargetNode = (tmpConvY) + (tmpConvX * aMazeHeight);

            List<int> tmpConstructedPath = new List<int>();

            int tmpHelperTarget = tmpTargetNode;

           // tmpConstructedPath.Add(parTargetNode);

            while (parPath[tmpHelperTarget] != -1)
            {

                int tmpPrevious = parPath[tmpHelperTarget];
                tmpHelperTarget = tmpPrevious;
                tmpConstructedPath.Add(tmpHelperTarget);
                

            }

            return tmpConstructedPath.ToArray();
        }

        public Vector2[] ConvertTargetIdsToVectorArray(int[] parTarget)
        {
            Vector2[] tmpVectorArray = new Vector2[parTarget.Length];

            for (int i = 0; i < parTarget.Length; i++)
            {
                int tmpPositionX = ((parTarget[i] / aMazeHeight)) * aTilesScale;
                int tmpPositionY = (parTarget[i] % aMazeHeight) * aTilesScale;

                tmpVectorArray[i] = new Vector2(tmpPositionX, tmpPositionY);
            }

            return tmpVectorArray;

        }
    }
}

