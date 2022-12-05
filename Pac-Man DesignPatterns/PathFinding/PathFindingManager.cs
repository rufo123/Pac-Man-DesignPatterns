using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Entities.TileEntity;
using Pac_Man_DesignPatterns.PathFinding.Algorithms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pac_Man_DesignPatterns.Entities;

namespace Pac_Man_DesignPatterns.PathFinding
{

    public class PathFindingManager
    {

        private readonly int aMazeWidth;

        private readonly int aMazeHeight;

        private readonly int[,] aMazeGraph;

        private readonly int aTilesScale;

        private int[,] aAdjMatrix;



        public PathFindingManager(int parMazeWidth, int parMazeHeight, Entity[] parListWallEntities, int parTilesScale)
        {
            this.aMazeWidth = parMazeWidth;
            this.aMazeHeight = parMazeHeight;
            this.aMazeGraph = new int[parMazeWidth, parMazeHeight];
            this.aTilesScale = parTilesScale;

            for (int i = 0; i < aMazeGraph.GetLength(0); i++)
            {
                for (int j = 0; j < aMazeGraph.GetLength(1); j++)
                {
                    aMazeGraph[i, j] = 1;
                }
            }

            foreach (var entity in parListWallEntities)
            {
                int tmpX = (int)(entity.Position.X / aTilesScale);
                int tmpY = (int)(entity.Position.Y / aTilesScale);

                this.aMazeGraph[tmpX, tmpY] = -1;
            }

            ConstructAdjMatrix(aMazeGraph);
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



        public int[] GetShortestPath(Vector2 parSource, IPathFindingAlgorithm pathFindingAlgorithm)
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
                    return pathFindingAlgorithm.FindShortestPath(aAdjMatrix, (tmpConvY) + (tmpConvX * aMazeHeight));
                }
            }

            return null;
        }

        public int[] ConstructPath(Vector2 parTarget, int[] parPath)
        {

            int tmpConvX = ((int)parTarget.X / aTilesScale) % aMazeWidth;
            int tmpConvY = ((int)parTarget.Y / aTilesScale) % aMazeHeight;

            int parTargetNode = (tmpConvY) + (tmpConvX * aMazeHeight);

            List<int> tmpConstructedPath = new List<int>();

            int tmpHelperTarget = parTargetNode;

           // tmpConstructedPath.Add(parTargetNode);

            while (parPath[tmpHelperTarget] != -1)
            {

                int tmpPrevious = parPath[tmpHelperTarget];
                tmpHelperTarget = tmpPrevious;
                tmpConstructedPath.Add(tmpHelperTarget);
                

            }

            return tmpConstructedPath.ToArray();
        }

        public Vector2 ConvertTargetIdToVector(int parTarget) {

            int parPositionX = ((parTarget / aMazeHeight)) * aTilesScale;
            int parPositionY = (parTarget % aMazeHeight) * aTilesScale;

            Debug.WriteLine(parTarget);

            return new Vector2(parPositionX, parPositionY);
        }

        public Vector2[] ConvertTargetIdsToVectorArray(int[] parTarget)
        {
            Vector2[] tmpVectorArray = new Vector2[parTarget.Length];

            for (int i = 0; i < parTarget.Length; i++)
            {
                int parPositionX = ((parTarget[i] / aMazeHeight)) * aTilesScale;
                int parPositionY = (parTarget[i] % aMazeHeight) * aTilesScale;

                tmpVectorArray[i] = new Vector2(parPositionX, parPositionY);
            }

            return tmpVectorArray;

        }

        private int[,] ReturnAdjMatrix(int[,] originalMatrix, int parWidth, int parHeight)
        {



            parWidth = originalMatrix.GetLength(0);
            parHeight = originalMatrix.GetLength(1);

            int n = parWidth;
            int m = parHeight;

            int[,] tmpAdj = new int[parWidth * parHeight, parWidth * parHeight];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (originalMatrix[i, j] > -1)
                    {

                        int x = (i - 1 < 0) ? n - 1 : i - 1;
                        int y = (i + 1 > n - 1) ? 0 : i + 1;
                        int z = (j - 1 < 0) ? m - 1 : j - 1;
                        int w = (j + 1 > m - 1) ? 0 : j + 1;

                        if (originalMatrix[x, j] > 0) tmpAdj[i * m + j, x * m + j] = 1;
                        if (originalMatrix[y, j] > 0) tmpAdj[i * m + j, y * m + j] = 1;
                        if (originalMatrix[i, z] > 0) tmpAdj[i * m + j, i * m + z] = 1;
                        if (originalMatrix[i, w] > 0) tmpAdj[i * m + j, i * m + w] = 1;

                    }
                }
            }
            //printMatrix(tmpAdj);

            return tmpAdj;
        }



    }
}

