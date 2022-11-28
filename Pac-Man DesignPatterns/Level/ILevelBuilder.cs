using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_DesignPatterns.Level
{
    public interface ILevelBuilder
    {
        LevelDirector Agregation { get; set; }

        public void BuildWalls(List<BluePrint> parWallsBlueprint, int parScale);
        public void BuildFood(List<BluePrint> parFoodBlueprint, int parScale);
        public void BuildGhostHouse(List<BluePrint> parGhostHouseBlueprint, int parScale);
        public void BuildScatterPoints(List<BluePrint> parScatterPointsBlueprint, int parScale);

        public IMazeProduct GetProduct();
    }
}
