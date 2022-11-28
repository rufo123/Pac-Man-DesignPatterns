using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pac_Man_DesignPatterns.Entities;
using Pac_Man_DesignPatterns.Entities.MovableEntity;
using Pac_Man_DesignPatterns.Entities.MovableEntity.Ghosts;
using Pac_Man_DesignPatterns.GhostFactory;
using Pac_Man_DesignPatterns.Level;
using Pac_Man_DesignPatterns.PathFinding;
using Pac_Man_DesignPatterns.PathFinding.Algorithms;
using Pac_Man_DesignPatterns.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Pac_Man_DesignPatterns.Entities.TileEntity;
using static System.Net.Mime.MediaTypeNames;

namespace Pac_Man_DesignPatterns.Game
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager aGraphics;
        private SpriteBatch aSpriteBatch;
        private Entity[] aEntityArray;
        private KeyHandler aKeyHandler;

        private LevelBuilder aLevelBuilder;
        private LevelDirector aLevelDirector;

        private IMazeProduct aLevelMaze;

        private CollisionDetector aCollisionDetector;

        private Random aRandom;

        private Random aRandomSeedGenerator;

        private RedGhost aRedGhostFactory;

        private CyanGhost aCyanGhostFactory;

        private PinkGhost aPinkGhostFactory;

        private OrangeGhost aOrangeGhostFactory;

        private PathFindingManager aPathFindingManager;

        private int[] aPathEntities;

        private Texture2D aWalltexture;

        private bool aStartPathFind;

        private PacMan aPacMan;

        private TileEntity[] aScatterPoints;

        private GhostHouse aGhostHouse;

        public PacMan PacMan => aPacMan;

        public TileEntity[] ScatterPoints => aScatterPoints;

        public GhostHouse GhostHouse => aGhostHouse;

        public Game()
        {
            aGraphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            aEntityArray = new Entity[2];
            aKeyHandler = new KeyHandler();

            aLevelBuilder = new LevelBuilder();
            aLevelDirector = new LevelDirector(aLevelBuilder, 24);

            aRandomSeedGenerator = new Random();
            aRandom = new Random(aRandomSeedGenerator.Next());

            aRedGhostFactory = new RedGhost();
            aPinkGhostFactory = new PinkGhost();
            aCyanGhostFactory = new CyanGhost();
            aOrangeGhostFactory = new OrangeGhost();

            

           
        }

        public GhostFactory.GhostFactory GhostFactory
        {
            get => default;
            set
            {
            }
        }

        public GhostFactory.GhostFactory GhostFactory1
        {
            get => default;
            set
            {
            }
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            aGraphics.PreferredBackBufferWidth = 800;
            aGraphics.PreferredBackBufferHeight = 800;
            aGraphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {

            Vector2 tmpSpawnPointTemp = new Vector2(aLevelDirector.TilesScale, aLevelDirector.TilesScale);

            aSpriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D tmPacmanTexture = Content.Load<Texture2D>("assets\\entitites\\pacman\\pacman");
            
            Texture2D tmpGhostTexture = Content.Load<Texture2D>("assets\\entitites\\ghost");

            

            Texture2D tmpWallTexture = Content.Load<Texture2D>("assets\\entitites\\wall");
            Texture2D tmpGhostHouseTexture = Content.Load<Texture2D>("assets\\entitites\\ghost_house");
            Texture2D tmpFoodTexture = Content.Load<Texture2D>("assets\\entitites\\pacman\\pacman");

            aWalltexture = tmpWallTexture;

      

            Texture2D[] tmpWallTextureArray = new Texture2D[2];

            for (int i = 1; i <= 2; i++)
            {
                
                Rectangle sourceRectangle = new Rectangle(0, (tmpWallTexture.Height / 2) * (i - 1), (tmpWallTexture.Width), (tmpWallTexture.Height / 2));
                
                Texture2D cropTexture = new Texture2D(GraphicsDevice, sourceRectangle.Width, sourceRectangle.Height);
                Color[] data = new Color[(sourceRectangle.Width) * (sourceRectangle.Height)];
                tmpWallTexture.GetData(0, sourceRectangle, data, 0, data.Length);
                cropTexture.SetData(data);

                tmpWallTextureArray[i - 1] = cropTexture;
            }

            aLevelBuilder.LoadTextures(tmpFoodTexture, tmpGhostHouseTexture, tmpWallTextureArray);
            aLevelDirector.ConvertLevelFromPathToBlueprint("Content\\assets\\levels\\level1.txt", aLevelDirector.TilesScale);
            aLevelMaze = aLevelDirector.CreateLevel();

            aScatterPoints = aLevelMaze.GetGhostScatterPoints();

            aGhostHouse = (GhostHouse)aLevelMaze.GetGhostHouse()[0];

           

            aPathFindingManager = new PathFindingManager(28, 31, aLevelMaze.GetWalls(), aLevelDirector.TilesScale);


            aCollisionDetector = new CollisionDetector(aLevelMaze.GetAllEntities());

            aEntityArray[1] = aRedGhostFactory.CreateGhost(tmpGhostTexture, (int)tmpSpawnPointTemp.X, (int)tmpSpawnPointTemp.Y, aLevelDirector.TilesScale, aGhostHouse.Position, aCollisionDetector);
            aPacMan = new PacMan(tmPacmanTexture, (int)tmpSpawnPointTemp.X, (int)tmpSpawnPointTemp.Y, aLevelDirector.TilesScale, aCollisionDetector);
            aEntityArray[0] = aPacMan;

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.P)) {
                aStartPathFind = true;
            }

            aSpriteBatch.Begin();

            int[] tmpPathFromPacman = null;


            foreach (Entity entity in aEntityArray)
            {
                if (entity is MovableEntity)
                {
                    if (entity is Ghost)

                    {
                       int[] tmpTargetPath = aPathFindingManager.GetShortestPath(((Ghost)entity).GetTargetPosition(), new DjkistraPathFind());

                        var test = aPathFindingManager.ConstructPath(entity.Position, tmpTargetPath);

                        aPathEntities = test;


                        if (test.Length > 0 && aStartPathFind)
                        {
                            int tmpChasedBlock = test.Length > 1 ? test[0] : test[0];

                            var pos = aPathFindingManager.ConvertTargetIdToVector(tmpChasedBlock);
                            ((Ghost)entity).PursuitTarget(pos);
                        }


                    }
                }
            }



            

            ControlEntityMovement(parGameTime: gameTime);

            aSpriteBatch.End();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            aSpriteBatch.Begin();

            foreach (var itemMaze in aLevelMaze.GetWalls())
            {
                itemMaze.Draw(aSpriteBatch);
            }
            foreach (var itemMaze in aLevelMaze.GetFood())
            {
                itemMaze.Draw(aSpriteBatch);
            }
            foreach (var itemMaze in aLevelMaze.GetOtherObjects())
            {
                itemMaze.Draw(aSpriteBatch);
            }

            foreach (var itemMaze in aLevelMaze.GetGhostHouse())
            {
                itemMaze.Draw(aSpriteBatch);
            }

            foreach (Entity entity in aEntityArray)
            {
                entity.Draw(aSpriteBatch);
            }

         /*     List<Rectangle> tmpList = new List<Rectangle>();

            for (int i = 0; i < aPathEntities.Length; i++)
            {
                Rectangle testeq = new Rectangle((int)aPathFindingManager.ConvertTargetIdToVector(aPathEntities[i]).X, (int)aPathFindingManager.ConvertTargetIdToVector(aPathEntities[i]).Y, aLevelDirector.TilesScale, aLevelDirector.TilesScale);

                tmpList.Add(testeq);
            }

            for (int i = 0; i < tmpList.Count; i++)
            {
                if (i == 0)
                {
                    aSpriteBatch.Draw(aWalltexture, tmpList[i], Color.Cyan);
                }
                else {
                    aSpriteBatch.Draw(aWalltexture, tmpList[i], Color.Red);
                }
                
            } */

            

            aSpriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void ControlEntityMovement(GameTime parGameTime)
        {

            foreach (Entity entity in aEntityArray)
            {
                if (entity is MovableEntity)
                {
                    if (entity is PacMan)
                    {
                        PacMan tmpPacmanEntity = (PacMan)entity;

                        Direction tmpInputDirection = aKeyHandler.GetKeyInput();
                        tmpPacmanEntity.ChangeDirection(tmpInputDirection);
                    }
                    else
                    {
                    }

                    entity.Update(parGameTime);
                    

                }
            }

        }
    }
}