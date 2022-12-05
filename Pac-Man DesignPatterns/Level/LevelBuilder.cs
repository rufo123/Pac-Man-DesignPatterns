using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pac_Man_DesignPatterns.Entities.TileEntity;
using Pac_Man_DesignPatterns.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace Pac_Man_DesignPatterns.Level
{
    public class LevelBuilder : ILevelBuilder
    {
        // ReSharper disable once InconsistentNaming
        protected IMazeProduct aMazeProduct;

        private string aGhostHouseTexturePath;
        private Texture2D[] aWallTexture;
        private string aCookieTexturePath;
        private string aPowerCookieTexturePath;

        public LevelDirector Agregation { get; set; }

        public ILevelBuilder Realization
        {
            get => default;
            set
            {
            }
        }

        public void InitTextures(ContentManager parContentManager, GraphicsDevice parGraphicsDevice, string parGhostHouseTexturePath, string parCookieTexturePath, string parPowerCookieTexturePath, string parWallTexture)
        {
            Texture2D tmpWallTexture = parContentManager.Load<Texture2D>(parWallTexture);
            aWallTexture = LoadWallArrayTexture(tmpWallTexture, parGraphicsDevice);

            aGhostHouseTexturePath = parGhostHouseTexturePath;
            aCookieTexturePath = parCookieTexturePath;
            aPowerCookieTexturePath = parPowerCookieTexturePath;
            aMazeProduct = new MazeProduct();
        }

        private Texture2D[] LoadWallArrayTexture(Texture2D parWallTexture, GraphicsDevice parGraphicsDevice)
        {
            Texture2D[] tmpWallTextureArray = new Texture2D[2];

            for (int i = 1; i <= 2; i++)
            {
                Rectangle sourceRectangle = new Rectangle(0, (parWallTexture.Height / 2) * (i - 1), (parWallTexture.Width), (parWallTexture.Height / 2));

                Texture2D cropTexture = new Texture2D(parGraphicsDevice, sourceRectangle.Width, sourceRectangle.Height);
                Color[] data = new Color[(sourceRectangle.Width) * (sourceRectangle.Height)];
                parWallTexture.GetData(0, sourceRectangle, data, 0, data.Length);
                cropTexture.SetData(data);

                tmpWallTextureArray[i - 1] = cropTexture;
            }

            return tmpWallTextureArray;
        }

        public void BuildFood(List<BluePrint> parFoodBlueprint, int parScale)
        {
            int tmpIndex = 0;

            foreach (var itemFoodBp in parFoodBlueprint)
            {
                aMazeProduct.AddFood(new Cookie(aCookieTexturePath, (int)itemFoodBp.Position.X, (int)itemFoodBp.Position.Y, parScale, Color.White));
                tmpIndex++;
            }
        }

        public void BuildGhostHouse(List<BluePrint> parGhostHouseBlueprint, int parScale)
        {
            int tmpIndex = 0;

            foreach (var itemGhostHouseBp in parGhostHouseBlueprint)
            {
                aMazeProduct.AddGhostHouse(new GhostHouse(aGhostHouseTexturePath, (int)itemGhostHouseBp.Position.X, (int)itemGhostHouseBp.Position.Y, parScale, Color.White, 90));
                tmpIndex++;
            }
        }

        public void BuildScatterPoints(List<BluePrint> parScatterPointBlueprint, int parScale)
        {
            int tmpIndex = 0;

            foreach (var itemScatterPointBp in parScatterPointBlueprint)
            {
                aMazeProduct.AddGhostScatterPoint(new GhostScatterPoint(null, (int)itemScatterPointBp.Position.X, (int)itemScatterPointBp.Position.Y, parScale, Color.White, 90));
                aMazeProduct.AddFood(new PowerCookie(aPowerCookieTexturePath, (int)itemScatterPointBp.Position.X, (int)itemScatterPointBp.Position.Y, parScale, Color.White));
                aMazeProduct.AddEmptySpace(new Vector2((int)itemScatterPointBp.Position.X * parScale, (int)itemScatterPointBp.Position.Y * parScale));
                tmpIndex++;
            }
        }


        public void BuildWalls(List<BluePrint> parWallsBlueprint, int parScale)
        {

            Dictionary<Vector2, BluePrint> tmpDictWallsConnectedToTileWithPos = new Dictionary<Vector2, BluePrint>();

            for (int i = 0; i < parWallsBlueprint.Count; i++)
            {
                tmpDictWallsConnectedToTileWithPos.Add(parWallsBlueprint[i].Position, parWallsBlueprint[i]);
            }


            List<BluePrint> tmpPostponedInitBlueprint4Adj = new List<BluePrint>();
            List<BluePrint> tmpPostponedInitBlueprint3Adj = new List<BluePrint>();


            foreach (var itemWallsbp in parWallsBlueprint)
            {

                Direction[] tmpNeighbours = itemWallsbp.GetSameTypeNeighbours;



                if (tmpNeighbours != null && tmpNeighbours.Length == 2)
                {

                    Direction tmpDirectionFirstNeighbour = tmpNeighbours[0];
                    Direction tmpDirectionSecondNeighbour = tmpNeighbours[1];

                    // 

                    int tmpRotation = 0;

                    bool tmpIsCorner = false;


                    if (tmpDirectionFirstNeighbour == Direction.UP || tmpDirectionSecondNeighbour == Direction.UP)
                    {
                        if (tmpDirectionFirstNeighbour == Direction.LEFT || tmpDirectionSecondNeighbour == Direction.LEFT)
                        {
                            tmpRotation = 180;
                            tmpIsCorner = true;
                            itemWallsbp.AddConnectedToTileId(Direction.UP, GenerateVectorPosFromDirection(Direction.UP, parScale, itemWallsbp.Position));
                            itemWallsbp.AddConnectedToTileId(Direction.LEFT, GenerateVectorPosFromDirection(Direction.LEFT, parScale, itemWallsbp.Position));


                        }
                        else if (tmpDirectionFirstNeighbour == Direction.RIGHT || tmpDirectionSecondNeighbour == Direction.RIGHT)
                        {
                            tmpRotation = 270;
                            tmpIsCorner = true;
                            itemWallsbp.AddConnectedToTileId(Direction.UP, GenerateVectorPosFromDirection(Direction.UP, parScale, itemWallsbp.Position));
                            itemWallsbp.AddConnectedToTileId(Direction.RIGHT, GenerateVectorPosFromDirection(Direction.RIGHT, parScale, itemWallsbp.Position));
                        }
                        else if (tmpDirectionFirstNeighbour == Direction.DOWN || tmpDirectionSecondNeighbour == Direction.DOWN)
                        {
                            tmpRotation = 0;
                            tmpIsCorner = false;
                            itemWallsbp.AddConnectedToTileId(Direction.UP, GenerateVectorPosFromDirection(Direction.UP, parScale, itemWallsbp.Position));
                            itemWallsbp.AddConnectedToTileId(Direction.DOWN, GenerateVectorPosFromDirection(Direction.DOWN, parScale, itemWallsbp.Position));
                        }


                    }
                    else if (tmpDirectionFirstNeighbour == Direction.DOWN || tmpDirectionSecondNeighbour == Direction.DOWN)
                    {

                        if (tmpDirectionFirstNeighbour == Direction.LEFT || tmpDirectionSecondNeighbour == Direction.LEFT)
                        {
                            tmpRotation = 90;
                            tmpIsCorner = true;
                            itemWallsbp.AddConnectedToTileId(Direction.DOWN, GenerateVectorPosFromDirection(Direction.DOWN, parScale, itemWallsbp.Position));
                            itemWallsbp.AddConnectedToTileId(Direction.LEFT, GenerateVectorPosFromDirection(Direction.LEFT, parScale, itemWallsbp.Position));
                        }
                        else if (tmpDirectionFirstNeighbour == Direction.RIGHT || tmpDirectionSecondNeighbour == Direction.RIGHT)
                        {
                            tmpRotation = 0;
                            tmpIsCorner = true;
                            itemWallsbp.AddConnectedToTileId(Direction.DOWN, GenerateVectorPosFromDirection(Direction.DOWN, parScale, itemWallsbp.Position));
                            itemWallsbp.AddConnectedToTileId(Direction.RIGHT, GenerateVectorPosFromDirection(Direction.RIGHT, parScale, itemWallsbp.Position));
                        }


                    }
                    else if (tmpDirectionFirstNeighbour == Direction.LEFT || tmpDirectionSecondNeighbour == Direction.LEFT)
                    {
                        if (tmpDirectionFirstNeighbour == Direction.RIGHT || tmpDirectionSecondNeighbour == Direction.RIGHT)
                        {
                            tmpRotation = 90;
                            tmpIsCorner = false;
                            itemWallsbp.AddConnectedToTileId(Direction.LEFT, GenerateVectorPosFromDirection(Direction.LEFT, parScale, itemWallsbp.Position));
                            itemWallsbp.AddConnectedToTileId(Direction.RIGHT, GenerateVectorPosFromDirection(Direction.RIGHT, parScale, itemWallsbp.Position));
                        }
                    }



                    if (tmpIsCorner)
                    {

                        aMazeProduct.AddWalls(new Wall(aWallTexture[1], (int)itemWallsbp.Position.X, (int)itemWallsbp.Position.Y, parScale, Color.White, tmpRotation));
                    }
                    else
                    {
                        aMazeProduct.AddWalls(new Wall(aWallTexture[0], (int)itemWallsbp.Position.X, (int)itemWallsbp.Position.Y, parScale, Color.White, tmpRotation));
                    }


                }
                else if (tmpNeighbours != null && tmpNeighbours.Length == 1)
                {

                    int tmpRotation = 0;

                    switch (tmpNeighbours[0])
                    {
                        case Direction.UP:
                            tmpRotation = 0;
                            itemWallsbp.AddConnectedToTileId(Direction.UP, GenerateVectorPosFromDirection(Direction.UP, parScale, itemWallsbp.Position));
                            break;
                        case Direction.DOWN:
                            tmpRotation = 180;
                            itemWallsbp.AddConnectedToTileId(Direction.DOWN, GenerateVectorPosFromDirection(Direction.DOWN, parScale, itemWallsbp.Position));
                            break;
                        case Direction.LEFT:
                            tmpRotation = 270;
                            itemWallsbp.AddConnectedToTileId(Direction.LEFT, GenerateVectorPosFromDirection(Direction.LEFT, parScale, itemWallsbp.Position));
                            break;
                        case Direction.RIGHT:
                            tmpRotation = 90;
                            itemWallsbp.AddConnectedToTileId(Direction.RIGHT, GenerateVectorPosFromDirection(Direction.RIGHT, parScale, itemWallsbp.Position));
                            break;
                        default:
                            break;
                    }

                    aMazeProduct.AddWalls(new Wall(aWallTexture[0], (int)itemWallsbp.Position.X, (int)itemWallsbp.Position.Y, parScale, Color.White, tmpRotation));

                }
                else if (tmpNeighbours != null && tmpNeighbours.Length == 3)
                {

                    tmpPostponedInitBlueprint3Adj.Add(itemWallsbp);



                    int tmpVerticalMatches = 0;
                    int tmpHorizontalMatches = 0;

                    for (int i = 0; i < tmpNeighbours.Length; i++)
                    {

                        if (tmpNeighbours[i] == Direction.LEFT || tmpNeighbours[i] == Direction.RIGHT)
                        {
                            tmpHorizontalMatches++;
                        }

                        if (tmpNeighbours[i] == Direction.UP || tmpNeighbours[i] == Direction.DOWN)
                        {
                            tmpVerticalMatches++;
                        }


                    }

                    if (tmpHorizontalMatches > 1)
                    {

                        aMazeProduct.AddWalls(new Wall(aWallTexture[0], (int)itemWallsbp.Position.X, (int)itemWallsbp.Position.Y, parScale, Color.White, 90));
                        itemWallsbp.AddConnectedToTileId(Direction.LEFT, GenerateVectorPosFromDirection(Direction.LEFT, parScale, itemWallsbp.Position));
                        itemWallsbp.AddConnectedToTileId(Direction.RIGHT, GenerateVectorPosFromDirection(Direction.RIGHT, parScale, itemWallsbp.Position));
                    }

                    else if (tmpVerticalMatches > 1)
                    {
                        aMazeProduct.AddWalls(new Wall(aWallTexture[0], (int)itemWallsbp.Position.X, (int)itemWallsbp.Position.Y, parScale, Color.White, 0));
                        itemWallsbp.AddConnectedToTileId(Direction.UP, GenerateVectorPosFromDirection(Direction.UP, parScale, itemWallsbp.Position));
                        itemWallsbp.AddConnectedToTileId(Direction.DOWN, GenerateVectorPosFromDirection(Direction.DOWN, parScale, itemWallsbp.Position));
                    }


                }
                else if (tmpNeighbours != null && tmpNeighbours.Length == 4)
                {
                    // aMazeProduct.AddWalls(new Wall(null, (int)itemWallsbp.Position.X, (int)itemWallsbp.Position.Y, parScale, 0));

                    tmpPostponedInitBlueprint4Adj.Add(itemWallsbp);

                }


                else
                {

                    // aMazeProduct.AddWalls(new Wall(aWallTexture[0], (int)itemWallsbp.Position.X, (int)itemWallsbp.Position.Y, parScale, 0));
                }




                for (int i = 0; i < tmpPostponedInitBlueprint3Adj.Count; i++)
                {


                    Vector2 tmpPosition = tmpPostponedInitBlueprint3Adj[i].Position;


                    Vector2 tmpUpPos = GenerateVectorPosFromDirection(Direction.UP, parScale, tmpPosition);
                    Vector2 tmpRightPos = GenerateVectorPosFromDirection(Direction.RIGHT, parScale, tmpPosition);
                    Vector2 tmpDownPos = GenerateVectorPosFromDirection(Direction.DOWN, parScale, tmpPosition);
                    Vector2 tmpLeftPos = GenerateVectorPosFromDirection(Direction.LEFT, parScale, tmpPosition);

                    BluePrint tmpUpTile = null;
                    BluePrint tmpRightTile = null;
                    BluePrint tmpDownTile = null;
                    BluePrint tmpLeftTile = null;

                    tmpDictWallsConnectedToTileWithPos.TryGetValue(tmpUpPos, out tmpUpTile);
                    tmpDictWallsConnectedToTileWithPos.TryGetValue(tmpRightPos, out tmpRightTile);
                    tmpDictWallsConnectedToTileWithPos.TryGetValue(tmpDownPos, out tmpDownTile);
                    tmpDictWallsConnectedToTileWithPos.TryGetValue(tmpLeftPos, out tmpLeftTile);

                    // RIGHT - DOWN CORNER - 0

                    if (tmpRightTile != null && tmpDownTile != null && tmpRightTile.GetConnectedToTileId(Direction.LEFT) == tmpPosition && tmpDownTile.GetConnectedToTileId(Direction.UP) == tmpPosition)
                    {
                        aMazeProduct.AddWalls(new Wall(aWallTexture[1], (int)tmpPostponedInitBlueprint3Adj[i].Position.X, (int)tmpPostponedInitBlueprint3Adj[i].Position.Y, parScale, Color.White, 0));

                    }

                    // LEFT - DOWN CORNER - 90

                    else if (tmpLeftTile != null && tmpDownTile != null && tmpLeftTile.GetConnectedToTileId(Direction.RIGHT) == tmpPosition && tmpDownTile.GetConnectedToTileId(Direction.UP) == tmpPosition)
                    {
                        aMazeProduct.AddWalls(new Wall(aWallTexture[1], (int)tmpPostponedInitBlueprint3Adj[i].Position.X, (int)tmpPostponedInitBlueprint3Adj[i].Position.Y, parScale, Color.White, 90));
                    }

                    // LEFT - UP CORNER - 180


                    else if (tmpLeftTile != null && tmpUpTile != null && tmpLeftTile.GetConnectedToTileId(Direction.RIGHT) == tmpPosition && tmpUpTile.GetConnectedToTileId(Direction.DOWN) == tmpPosition)
                    {
                        aMazeProduct.AddWalls(new Wall(aWallTexture[1], (int)tmpPostponedInitBlueprint3Adj[i].Position.X, (int)tmpPostponedInitBlueprint3Adj[i].Position.Y, parScale, Color.White, 180));

                    }

                    // RIGHT - UP CORNER - 270

                    else if (tmpRightTile != null && tmpUpTile != null && tmpRightTile.GetConnectedToTileId(Direction.LEFT) == tmpPosition && tmpUpTile.GetConnectedToTileId(Direction.DOWN) == tmpPosition)
                    {
                        aMazeProduct.AddWalls(new Wall(aWallTexture[1], (int)tmpPostponedInitBlueprint3Adj[i].Position.X, (int)tmpPostponedInitBlueprint3Adj[i].Position.Y, parScale, Color.White, 270));

                    }

  


                }


                for (int i = 0; i < tmpPostponedInitBlueprint4Adj.Count; i++)
                {
                    Vector2 tmpPosition = tmpPostponedInitBlueprint4Adj[i].Position;


                    Vector2 tmpUpPos = GenerateVectorPosFromDirection(Direction.UP, parScale, tmpPosition);
                    Vector2 tmpRightPos = GenerateVectorPosFromDirection(Direction.RIGHT, parScale, tmpPosition);
                    Vector2 tmpDownPos = GenerateVectorPosFromDirection(Direction.DOWN, parScale, tmpPosition);
                    Vector2 tmpLeftPos = GenerateVectorPosFromDirection(Direction.LEFT, parScale, tmpPosition);

                    BluePrint tmpUpTile = null;
                    BluePrint tmpRightTile = null;
                    BluePrint tmpDownTile = null;
                    BluePrint tmpLeftTile = null;

                    tmpDictWallsConnectedToTileWithPos.TryGetValue(tmpUpPos, out tmpUpTile);
                    tmpDictWallsConnectedToTileWithPos.TryGetValue(tmpRightPos, out tmpRightTile);
                    tmpDictWallsConnectedToTileWithPos.TryGetValue(tmpDownPos, out tmpDownTile);
                    tmpDictWallsConnectedToTileWithPos.TryGetValue(tmpLeftPos, out tmpLeftTile);

                    // RIGHT - DOWN CORNER - 0

                    if (tmpRightTile != null && tmpDownTile != null && tmpRightTile.GetConnectedToTileId(Direction.LEFT) == tmpPosition && tmpDownTile.GetConnectedToTileId(Direction.UP) == tmpPosition)
                    {
                        aMazeProduct.AddWalls(new Wall(aWallTexture[1], (int)tmpPostponedInitBlueprint4Adj[i].Position.X, (int)tmpPostponedInitBlueprint4Adj[i].Position.Y, parScale, Color.White, 0));
                    }

                    // LEFT - DOWN CORNER - 90

                    else if (tmpLeftTile != null && tmpDownTile != null && tmpLeftTile.GetConnectedToTileId(Direction.RIGHT) == tmpPosition && tmpDownTile.GetConnectedToTileId(Direction.UP) == tmpPosition)
                    {
                        aMazeProduct.AddWalls(new Wall(aWallTexture[1], (int)tmpPostponedInitBlueprint4Adj[i].Position.X, (int)tmpPostponedInitBlueprint4Adj[i].Position.Y, parScale, Color.White, 90));
                    }

                    // LEFT - UP CORNER - 180


                    else if (tmpLeftTile != null && tmpUpTile != null && tmpLeftTile.GetConnectedToTileId(Direction.RIGHT) == tmpPosition && tmpUpTile.GetConnectedToTileId(Direction.DOWN) == tmpPosition)
                    {
                        aMazeProduct.AddWalls(new Wall(aWallTexture[1], (int)tmpPostponedInitBlueprint4Adj[i].Position.X, (int)tmpPostponedInitBlueprint4Adj[i].Position.Y, parScale, Color.White, 180));
                    }

                    // RIGHT - UP CORNER - 270

                    else if (tmpRightTile != null && tmpUpTile != null && tmpRightTile.GetConnectedToTileId(Direction.LEFT) == tmpPosition && tmpUpTile.GetConnectedToTileId(Direction.DOWN) == tmpPosition)
                    {
                        aMazeProduct.AddWalls(new Wall(aWallTexture[1], (int)tmpPostponedInitBlueprint4Adj[i].Position.X, (int)tmpPostponedInitBlueprint4Adj[i].Position.Y, parScale, Color.White, 270));
                    }
                }
            }
        }

        public IMazeProduct GetProduct()
        {
            return aMazeProduct;
        }

        public Vector2 GenerateVectorPosFromDirection(Direction parDirection, int parScale, Vector2 parPosition)
        {

            switch (parDirection)
            {
                case Direction.NOTHING:
                    return parPosition;
                case Direction.UP:
                    return new Vector2(parPosition.X, parPosition.Y - parScale);
                case Direction.DOWN:
                    return new Vector2(parPosition.X, parPosition.Y + parScale);
                case Direction.LEFT:
                    return new Vector2(parPosition.X - parScale, parPosition.Y);
                case Direction.RIGHT:
                    return new Vector2(parPosition.X + parScale, parPosition.Y);
                default:
                    return parPosition;
            }
        }

        public void BuildEmptySpaces(List<BluePrint> parEmptySpacesBlueprint, int parScale)
        {
            foreach (var itemEmptySpace in parEmptySpacesBlueprint)
            {
                aMazeProduct.AddEmptySpace(itemEmptySpace.Position);
            }
        }

        /* public bool CheckTwoAdjancentTiles(List<BluePrint> parWallsBlueprint, int parSourceX, int parSourceY, int parScale) {

             for (int i = 0; i < parWallsBlueprint.Count; i++)
             {
                 if ()
             }
         } */


    }
}
