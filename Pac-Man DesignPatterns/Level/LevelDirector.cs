using Pac_Man_DesignPatterns.Entities.TileEntity;
using Pac_Man_DesignPatterns.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_DesignPatterns.Level
{
    public class LevelDirector
    {
        private ILevelBuilder aBuilder;

        private List<BluePrint> aWallsBlueprint;
        private List<BluePrint> aGhostHouseBlueprint;
        private List<BluePrint> aFoodBlueprint;
        private List<BluePrint> aScatterPointsBlueprint;

        private int aTilesScale;

        public void ConvertLevelFromPathToBlueprint(string parLevelPath, int parScale)
        {

            string[] tmpLines = File.ReadAllLines(parLevelPath);


            string[,] tmpCharMatrix = new string[31, 28];


            for (int tmpRow = 0; tmpRow < tmpLines.Length; tmpRow++)
            {
                string[] tmpParsedLine = tmpLines[tmpRow].Split(' ');

                for (int tmpCol = 0; tmpCol < tmpParsedLine.Length; tmpCol++)
                {

                    tmpCharMatrix[tmpRow, tmpCol] = tmpParsedLine[tmpCol];

                    
                }
            }

            for (int tmpRow = 0; tmpRow < tmpCharMatrix.GetLength(0); tmpRow++)
            {

                for (int tmpCol = 0; tmpCol < tmpCharMatrix.GetLength(1); tmpCol++)
                {

                    string tmpValue = tmpCharMatrix.GetValue(tmpRow, tmpCol)!.ToString();
                    int tmpValueAsInt = Int32.Parse(tmpValue!);

                    switch (tmpValueAsInt)
                    {
                        case (int)LevelMappings.Wall:

                            List<Direction> tmpListNeighbours = new List<Direction>(4);


                            //Neighbours UP
                            if ((tmpRow - 1 > -1 && tmpRow - 1 < tmpCharMatrix.GetLength(0)) && tmpCharMatrix[tmpRow - 1, tmpCol] != null && Int32.Parse((string)tmpCharMatrix.GetValue(tmpRow - 1, tmpCol)!).Equals((int)LevelMappings.Wall)) {
                                tmpListNeighbours.Add(Direction.UP);
                            }
                            //Neighbours DOWN
                            if ((tmpRow + 1 > -1 && tmpRow + 1 < tmpCharMatrix.GetLength(0)) && tmpCharMatrix[tmpRow + 1, tmpCol] != null && Int32.Parse((string)tmpCharMatrix.GetValue(tmpRow + 1, tmpCol)!).Equals((int)LevelMappings.Wall))
                            {
                                tmpListNeighbours.Add(Direction.DOWN);
                            }
                            //Neighbours LEFT
                            if ((tmpCol - 1 > -1 && tmpCol - 1 < tmpCharMatrix.GetLength(1)) && tmpCharMatrix[tmpRow ,tmpCol - 1] != null && Int32.Parse((string)tmpCharMatrix.GetValue(tmpRow, tmpCol - 1)!).Equals((int)LevelMappings.Wall))
                            {
                                tmpListNeighbours.Add(Direction.LEFT);
                            }
                            //Neighbours RIGHT
                            if ((tmpCol + 1 > -1 && tmpCol + 1 < tmpCharMatrix.GetLength(1)) && tmpCharMatrix[tmpRow, tmpCol + 1] != null && Int32.Parse((string)tmpCharMatrix.GetValue(tmpRow, tmpCol + 1)!).Equals((int)LevelMappings.Wall))
                            {
                                tmpListNeighbours.Add(Direction.RIGHT);
                            }


                            aWallsBlueprint.Add(new BluePrint(new System.Numerics.Vector2(tmpCol * parScale, tmpRow * parScale), 0, tmpListNeighbours.ToArray()));
                            break;
                        case (int)LevelMappings.Empty:

                            break;
                        case (int)LevelMappings.GhostHouse:
                            aGhostHouseBlueprint.Add(new BluePrint(new System.Numerics.Vector2(tmpCol * parScale, tmpRow * parScale), 0));
                            break;
                        case (int)LevelMappings.ScatterPoint:
                            aScatterPointsBlueprint.Add(new BluePrint(new System.Numerics.Vector2(tmpCol * parScale, tmpRow * parScale), 0));
                            break;
                    }


                }
            }



           




        }

        public LevelDirector(ILevelBuilder parLevelBuilder, int parTilesScale)
        {
            aWallsBlueprint = new List<BluePrint>();
            aFoodBlueprint = new List<BluePrint>();
            aGhostHouseBlueprint = new List<BluePrint>();
            aScatterPointsBlueprint = new List<BluePrint>();
            aBuilder = parLevelBuilder;
            aTilesScale = parTilesScale;
        }

        public IMazeProduct CreateLevel()
        {
            aBuilder.BuildFood(aFoodBlueprint, aTilesScale);
            aBuilder.BuildWalls(aWallsBlueprint, aTilesScale);
            aBuilder.BuildGhostHouse(aGhostHouseBlueprint, aTilesScale);
            aBuilder.BuildScatterPoints(aScatterPointsBlueprint, aTilesScale);
            return aBuilder.GetProduct();
        }

        public int TilesScale
        {
            get => aTilesScale;
        }




    }
}

public enum LevelMappings
{
    Empty = 0,
    Wall = 1,
    GhostHouse = 2,
    ScatterPoint = 3
}
