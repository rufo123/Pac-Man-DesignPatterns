using Pac_Man_DesignPatterns.Entities.TileEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_DesignPatterns.Level
{
    public interface IMazeProduct
    {
        public void AddWalls(TileEntity parWalls);

        public void AddFood(TileEntity parFood);

        public void AddOtherObjects(TileEntity parObjects);

        public void AddGhostHouse(TileEntity parGhostHouse);

        public void AddGhostScatterPoint(TileEntity parGhostScatterPoints);

        public TileEntity[] GetWalls();
        public TileEntity[] GetFood();

        public TileEntity[] GetGhostHouse();

        public TileEntity[] GetOtherObjects();

        public TileEntity[] GetGhostScatterPoints();

        public TileEntity[] GetFinalProduct();

        public TileEntity[] GetAllEntities();



    }
}
