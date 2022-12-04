using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Entities;

namespace Pac_Man_DesignPatterns.Level
{
    public interface IMazeProduct
    {
        public void AddWalls(Entity parWalls);

        public void AddFood(Entity parFood);

        public void AddOtherObjects(Entity parObjects);

        public void AddGhostHouse(Entity parGhostHouse);

        public void AddGhostScatterPoint(Entity parGhostScatterPoints);

        public void AddEmptySpace(Vector2 parVectorEmptySpace);


        public Entity[] GetWalls();
        public Entity[] GetFood();

        public Entity[] GetGhostHouse();

        public Entity[] GetOtherObjects();

        public Entity[] GetGhostScatterPoints();

        public Entity[] GetFinalProduct();

        public Entity[] GetAllEntities();

        public Vector2[] GetEmptySpaces();



    }
}
