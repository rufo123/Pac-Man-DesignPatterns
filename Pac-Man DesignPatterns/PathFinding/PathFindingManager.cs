using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Entities.TileEntity;
using Pac_Man_DesignPatterns.PathFinding.Algorithms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_DesignPatterns.PathFinding
{

    public class PathFindingManager
    {

        private int aMazeWidth;

        private int aMazeHeight;

        private int[,] aMazeGraph;

        private int aTilesScale;



        public PathFindingManager(int parMazeWidth, int parMazeHeight, TileEntity[] parListWallEntities, int parTilesScale)
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

        }

        public int[] GetShortestPath(Vector2 parSource, IPathFindingAlgorithm pathFindingAlgorithm)
        {
            int tmpConvX = ((int)parSource.X / aTilesScale) % aMazeWidth;
            int tmpConvY = ((int)parSource.Y / aTilesScale) % aMazeHeight;

            return pathFindingAlgorithm.FindShortestPath(aMazeGraph, (tmpConvY) + (tmpConvX * aMazeHeight));
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



    }
}

