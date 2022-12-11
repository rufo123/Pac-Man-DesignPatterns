using System.Collections.Generic;

namespace Pac_Man_DesignPatterns.Level
{
    public interface ILevelBuilder
    {
        public void BuildWalls(List<BluePrint> parWallsBlueprint, int parScale);
        public void BuildFood(List<BluePrint> parFoodBlueprint, int parScale);
        public void BuildGhostHouse(List<BluePrint> parGhostHouseBlueprint, int parScale);
        public void BuildScatterPoints(List<BluePrint> parScatterPointsBlueprint, int parScale);
        public void BuildEmptySpaces(List<BluePrint> parEmptySpacesBlueprint, int parScale);

        public IMazeProduct GetProduct();
    }
}
