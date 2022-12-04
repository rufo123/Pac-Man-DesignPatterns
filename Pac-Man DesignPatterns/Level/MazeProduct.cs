using Pac_Man_DesignPatterns.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Entities;

namespace Pac_Man_DesignPatterns.Level
{
    public class MazeProduct : IMazeProduct
    {
        private readonly List<Entity> aFood;

        private readonly List<Entity> aWalls;

        private readonly List<Entity> aObjects;

        private readonly List<Entity> aGhostScatterPoints;

        private readonly List<Entity> aGhostHouse;

        private readonly List<Entity> aAllEntities;

        private readonly List<Vector2> aEmptySpaces;


        public MazeProduct()
        {
            aFood = new List<Entity>();
            aWalls = new List<Entity>();
            aObjects = new List<Entity>();
            aGhostScatterPoints = new List<Entity>();
            aGhostHouse = new List<Entity>();
            aAllEntities = new List<Entity>();
            aEmptySpaces = new List<Vector2>();


        }

        public void AddFood(Entity parFood)
        {
            aFood.Add(parFood);
            aAllEntities.Add(parFood);
        }

        public void AddOtherObjects(Entity parObjects)
        {
            aObjects.Add(parObjects);
            aAllEntities.Add(parObjects);
        }

        public void AddGhostHouse(Entity parGhostHouse)
        {
            aGhostHouse.Add(parGhostHouse);
            aAllEntities.Add(parGhostHouse);
        }

        public void AddGhostScatterPoint(Entity parGhostScatterPoints)
        {
            aGhostScatterPoints.Add(parGhostScatterPoints);
            aAllEntities.Add(parGhostScatterPoints);
        }


        public void AddWalls(Entity parWalls)
        {
            aWalls.Add(parWalls);
            aAllEntities.Add(parWalls);
        }

        public void AddEmptySpace(Vector2 parVectorEmptySpace)
        {
            aEmptySpaces.Add(parVectorEmptySpace);
        }

        public Entity[] GetGhostScatterPoints()
        {
            return aGhostScatterPoints.ToArray();
        }

        public Entity[] GetFinalProduct()
        {
            throw new NotImplementedException();
        }

        public Entity[] GetFood()
        {
            return aFood.ToArray();
        }

        public Entity[] GetGhostHouse()
        {
            return aGhostHouse.ToArray();
        }

        public Entity[] GetOtherObjects()
        {
            return aObjects.ToArray();
        }

        public Entity[] GetWalls()
        {
            return aWalls.ToArray();
        }

        public Entity[] GetAllEntities()
        {
            return aAllEntities.ToArray();
        }

        public Vector2[] GetEmptySpaces()
        {
            return aEmptySpaces.ToArray();
        }

    }
}
