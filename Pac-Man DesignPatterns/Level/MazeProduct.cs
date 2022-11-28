using Pac_Man_DesignPatterns.Entities.TileEntity;
using Pac_Man_DesignPatterns.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_DesignPatterns.Level
{
    internal class MazeProduct : IMazeProduct
    {
        private List<TileEntity> aFood;

        private List<TileEntity> aWalls;

        private List<TileEntity> aObjects;

        private List<TileEntity> aGhostScatterPoints;

        private List<TileEntity> aGhostHouse;

        private List<TileEntity> aAllEntities;


        public MazeProduct()
        {
            aFood = new List<TileEntity>();
            aWalls = new List<TileEntity>();
            aObjects = new List<TileEntity>();
            aGhostScatterPoints = new List<TileEntity>();
            aGhostHouse = new List<TileEntity>();
            aAllEntities = new List<TileEntity>();
        }

        public void AddFood(TileEntity parFood)
        {
            aFood.Add(parFood);
            aAllEntities.Add(parFood);
        }

        public void AddOtherObjects(TileEntity parObjects)
        {
            aObjects.Add(parObjects);
            aAllEntities.Add(parObjects);
        }

        public void AddGhostHouse(TileEntity parGhostHouse)
        {
            aGhostHouse.Add(parGhostHouse);
            aAllEntities.Add(parGhostHouse);
        }

        public void AddGhostScatterPoint(TileEntity parGhostScatterPoints)
        {
            aGhostScatterPoints.Add(parGhostScatterPoints);
            aAllEntities.Add(parGhostScatterPoints);
        }

        public void AddWalls(TileEntity parWalls)
        {
            aWalls.Add(parWalls);
            aAllEntities.Add(parWalls);
        }

        public TileEntity[] GetGhostScatterPoints()
        {
            return aGhostScatterPoints.ToArray();
        }

        public TileEntity[] GetFinalProduct()
        {
            throw new NotImplementedException();
        }

        public TileEntity[] GetFood()
        {
            return aFood.ToArray();
        }

        public TileEntity[] GetGhostHouse()
        {
            return aGhostHouse.ToArray();
        }

        public TileEntity[] GetOtherObjects()
        {
            return aObjects.ToArray();
        }

        public TileEntity[] GetWalls()
        {
            return aWalls.ToArray();
        }

        public TileEntity[] GetAllEntities()
        {
            return aAllEntities.ToArray();
        }

    }
}
