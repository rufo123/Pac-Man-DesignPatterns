using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pac_Man_DesignPatterns.Entities.TileEntity;
using Pac_Man_DesignPatterns.Utils;

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
            foreach (var itemFoodBp in parFoodBlueprint)
            {
                aMazeProduct.AddFood(new Cookie(aCookieTexturePath, (int)itemFoodBp.Position.X, (int)itemFoodBp.Position.Y, parScale, Color.White));
            }
        }

        public void BuildGhostHouse(List<BluePrint> parGhostHouseBlueprint, int parScale)
        {
            foreach (var itemGhostHouseBp in parGhostHouseBlueprint)
            {
                aMazeProduct.AddGhostHouse(new GhostHouse(aGhostHouseTexturePath, (int)itemGhostHouseBp.Position.X, (int)itemGhostHouseBp.Position.Y, parScale, Color.White, 90));
            }
        }

        public void BuildScatterPoints(List<BluePrint> parScatterPointBlueprint, int parScale)
        {
            foreach (var itemScatterPointBp in parScatterPointBlueprint)
            {
                aMazeProduct.AddGhostScatterPoint(new GhostScatterPoint(null, (int)itemScatterPointBp.Position.X, (int)itemScatterPointBp.Position.Y, parScale, Color.White, 90));
                aMazeProduct.AddFood(new PowerCookie(aPowerCookieTexturePath, (int)itemScatterPointBp.Position.X, (int)itemScatterPointBp.Position.Y, parScale, Color.White));
                aMazeProduct.AddEmptySpace(new Vector2((int)itemScatterPointBp.Position.X * parScale, (int)itemScatterPointBp.Position.Y * parScale));
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


            foreach (var itemWallBluePrint in parWallsBlueprint)
            {
                Direction[] tmpNeighbours = itemWallBluePrint.GetSameTypeNeighbours;

                if (tmpNeighbours is { Length: 2 })
                {
                    itemWallBluePrint.TwoNeighbourLogic(tmpNeighbours, parScale, out bool tmpIsCorner, out float tmpRotation);

                    aMazeProduct.AddWalls(new Wall(tmpIsCorner ? aWallTexture[1] : aWallTexture[0], (int)itemWallBluePrint.Position.X, (int)itemWallBluePrint.Position.Y, parScale, Color.White, (int)tmpRotation));

                }
                else if (tmpNeighbours is { Length: 1 })
                {
                    itemWallBluePrint.OneNeighbourLogic(tmpNeighbours, parScale, out bool tmpIsCorner, out float tmpRotation);

                    aMazeProduct.AddWalls(new Wall(tmpIsCorner ? aWallTexture[1] : aWallTexture[0], (int)itemWallBluePrint.Position.X, (int)itemWallBluePrint.Position.Y, parScale, Color.White, (int)tmpRotation));

                }
                else if (tmpNeighbours is { Length: 3 })
                {
                    tmpPostponedInitBlueprint3Adj.Add(itemWallBluePrint);

                    itemWallBluePrint.ThreeNeighbourLogic(tmpNeighbours, parScale, out bool tmpIsCorner, out float tmpRotation);

                    aMazeProduct.AddWalls(new Wall(tmpIsCorner ? aWallTexture[1] : aWallTexture[0], (int)itemWallBluePrint.Position.X, (int)itemWallBluePrint.Position.Y, parScale, Color.White, (int)tmpRotation));

                }
                else if (tmpNeighbours is { Length: 4 })
                {
                    tmpPostponedInitBlueprint4Adj.Add(itemWallBluePrint);
                }

                for (int i = 0; i < tmpPostponedInitBlueprint3Adj.Count; i++)
                {
                    tmpPostponedInitBlueprint3Adj[i].FourNeighbourLogic(null, parScale, out bool tmpIsCorner, out float tmpRotation, tmpDictWallsConnectedToTileWithPos);

                    if ((int)tmpRotation != -1)
                    {
                        aMazeProduct.AddWalls(new Wall(tmpIsCorner ? aWallTexture[1] : aWallTexture[0], (int)tmpPostponedInitBlueprint3Adj[i].Position.X, (int)tmpPostponedInitBlueprint3Adj[i].Position.Y, parScale, Color.White, (int)tmpRotation));
                    }
                }

                for (int i = 0; i < tmpPostponedInitBlueprint4Adj.Count; i++)
                {
                    tmpPostponedInitBlueprint4Adj[i].FourNeighbourLogic(null, parScale, out bool tmpIsCorner, out float tmpRotation, tmpDictWallsConnectedToTileWithPos);

                    if ((int)tmpRotation != -1)
                    {
                        aMazeProduct.AddWalls(new Wall(tmpIsCorner ? aWallTexture[1] : aWallTexture[0], (int)tmpPostponedInitBlueprint4Adj[i].Position.X, (int)tmpPostponedInitBlueprint4Adj[i].Position.Y, parScale, Color.White, (int)tmpRotation));
                    }
                }
            }
        }

        public IMazeProduct GetProduct()
        {
            return aMazeProduct;
        }

        public void BuildEmptySpaces(List<BluePrint> parEmptySpacesBlueprint, int parScale)
        {
            foreach (var itemEmptySpace in parEmptySpacesBlueprint)
            {
                aMazeProduct.AddEmptySpace(itemEmptySpace.Position);
            }
        }

    }
}
