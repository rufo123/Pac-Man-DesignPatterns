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
using System.Linq;
using Microsoft.Xna.Framework.Content;
using Pac_Man_DesignPatterns.Entities.TileEntity;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;
using Pac_Man_DesignPatterns.Menu;

namespace Pac_Man_DesignPatterns.Game
{
    public class Game : Microsoft.Xna.Framework.Game, IObserver
    {
        private readonly GraphicsDeviceManager aGraphics;
        private SpriteBatch aSpriteBatch;
        private readonly Entity[] aEntityArray;
        private readonly KeyHandler aKeyHandler;

        private readonly LevelBuilder aLevelBuilder;
        private readonly LevelDirector aLevelDirector;

        private IMazeProduct aLevelMaze;

        private CollisionDetector aCollisionDetector;

        private CollisionDetector aCollisionDetectorWithGhosts;

        private readonly Random aRandom;

        private readonly Random aRandomSeedGenerator;

        private readonly RedGhost aRedGhostFactory;

        private readonly CyanGhost aCyanGhostFactory;

        private readonly PinkGhost aPinkGhostFactory;

        private readonly OrangeGhost aOrangeGhostFactory;

        private PathFindingManager aPathFindingManager;

        private PacMan aPacMan;

        private Entity[] aScatterPoints;

        private GhostHouse aGhostHouse;

        private UIManager aUIManager;

        private Vector2 aMazeSizeVector;

        private float aXOffset;
        private float aYOffset;

        private readonly Vector2[] aArrayRandomTiles;

        public PacMan PacMan => aPacMan;

        public Entity[] ScatterPoints => aScatterPoints;

        private float aTimer = 0;

        public GhostHouse GhostHouse => aGhostHouse;

        private Texture2D aDebugWallTexture;

        public Vector2[] ArrayRandomTiles => aArrayRandomTiles;

        public Dictionary<TexturesEnum, string> aDictionaryTexturePaths;

        private Menu.Menu aMenuDelete;

        public Game()
        {
            aGraphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            aEntityArray = new Entity[5];
            aKeyHandler = new KeyHandler();

            aLevelBuilder = new LevelBuilder();
            aLevelDirector = new LevelDirector(aLevelBuilder, 24);

            aRandomSeedGenerator = new Random();
            aRandom = new Random(aRandomSeedGenerator.Next());

            aRedGhostFactory = new RedGhost();
            aPinkGhostFactory = new PinkGhost();
            aCyanGhostFactory = new CyanGhost();
            aOrangeGhostFactory = new OrangeGhost();

            aArrayRandomTiles = new Vector2[4];

            aDictionaryTexturePaths = new Dictionary<TexturesEnum, string>();

           

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
            aGraphics.PreferredBackBufferHeight = 850;
            aGraphics.ApplyChanges();

            InitTexturesDictionary();

            aLevelBuilder.InitTextures(Content, GraphicsDevice, aDictionaryTexturePaths[TexturesEnum.GHOST_HOUSE], aDictionaryTexturePaths[TexturesEnum.COOKIE], aDictionaryTexturePaths[TexturesEnum.POWERCOOKIE], aDictionaryTexturePaths[TexturesEnum.WALL]);
            aLevelDirector.ConvertLevelFromPathToBlueprint("Content\\assets\\levels\\level1.txt", aLevelDirector.TilesScale);
            aLevelMaze = aLevelDirector.CreateLevel();

            aMazeSizeVector = new Vector2(28, 31);

            float tmpGameWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
            aXOffset = (tmpGameWidth - (aLevelDirector.TilesScale * aMazeSizeVector.X)) / 2;
            aYOffset = 100;

            aGhostHouse = (GhostHouse)aLevelMaze.GetGhostHouse()[0];
            aScatterPoints = aLevelMaze.GetGhostScatterPoints();

            Vector2 tmpSpawnPointTemp = new Vector2(aGhostHouse.Position.X, aGhostHouse.Position.Y);

            aPathFindingManager = new PathFindingManager(28, 31, aLevelMaze.GetWalls(), aLevelDirector.TilesScale);
            aCollisionDetector = new CollisionDetector(aLevelMaze.GetAllEntities());

            Vector2 tmpGhostHouseTargetPos = new Vector2(aGhostHouse.Position.X, aGhostHouse.Position.Y + aLevelDirector.TilesScale);

            aEntityArray[1] = aRedGhostFactory.CreateGhost(aDictionaryTexturePaths[TexturesEnum.GHOST_GENERAL], (int)tmpSpawnPointTemp.X, (int)tmpSpawnPointTemp.Y + aLevelDirector.TilesScale, aLevelDirector.TilesScale, tmpGhostHouseTargetPos, aCollisionDetector, aDictionaryTexturePaths[TexturesEnum.GHOST_FRIGHTENED], aDictionaryTexturePaths[TexturesEnum.GHOST_DEAD]);
            aEntityArray[2] = aPinkGhostFactory.CreateGhost(aDictionaryTexturePaths[TexturesEnum.GHOST_GENERAL], (int)tmpSpawnPointTemp.X, (int)tmpSpawnPointTemp.Y + aLevelDirector.TilesScale, aLevelDirector.TilesScale, tmpGhostHouseTargetPos, aCollisionDetector, aDictionaryTexturePaths[TexturesEnum.GHOST_FRIGHTENED], aDictionaryTexturePaths[TexturesEnum.GHOST_DEAD]);
            aEntityArray[3] = aCyanGhostFactory.CreateGhost(aDictionaryTexturePaths[TexturesEnum.GHOST_GENERAL], (int)tmpSpawnPointTemp.X, (int)tmpSpawnPointTemp.Y + aLevelDirector.TilesScale, aLevelDirector.TilesScale, tmpGhostHouseTargetPos, aCollisionDetector, aDictionaryTexturePaths[TexturesEnum.GHOST_FRIGHTENED], aDictionaryTexturePaths[TexturesEnum.GHOST_DEAD]);
            aEntityArray[4] = aOrangeGhostFactory.CreateGhost(aDictionaryTexturePaths[TexturesEnum.GHOST_GENERAL], (int)tmpSpawnPointTemp.X, (int)tmpSpawnPointTemp.Y + aLevelDirector.TilesScale, aLevelDirector.TilesScale, tmpGhostHouseTargetPos, aCollisionDetector, aDictionaryTexturePaths[TexturesEnum.GHOST_FRIGHTENED], aDictionaryTexturePaths[TexturesEnum.GHOST_DEAD]);

            List<Entity> tmpListAll = aLevelMaze.GetAllEntities().ToList();
            tmpListAll.Add(aEntityArray[1]);
            tmpListAll.Add(aEntityArray[2]);
            tmpListAll.Add(aEntityArray[3]);
            tmpListAll.Add(aEntityArray[4]);

            aCollisionDetectorWithGhosts = new CollisionDetector(tmpListAll);

            aPacMan = new PacMan(aDictionaryTexturePaths[TexturesEnum.PACMAN], (int)tmpSpawnPointTemp.X, (int)tmpSpawnPointTemp.Y - aLevelDirector.TilesScale, aLevelDirector.TilesScale, aCollisionDetectorWithGhosts, Color.White);

            aEntityArray[0] = aPacMan;

            aUIManager = new UIManager(new Vector2(GraphicsDevice.PresentationParameters.BackBufferWidth, aYOffset), aLevelDirector.TilesScale, GraphicsDevice);

            MenuItem test1 = new PlayButton("Play");
            MenuItem test2 = new QuitButton("Quit");
            aMenuDelete = new Menu.Menu(GraphicsDevice, Color.Purple, GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight, new MenuItem[] { test1, test2 });

            base.Initialize();
        }

        private void InitTexturesDictionary()
        {
            aDictionaryTexturePaths.Add(TexturesEnum.PACMAN, "assets\\entitites\\pacman\\pacman");
            aDictionaryTexturePaths.Add(TexturesEnum.GHOST_GENERAL, "assets\\entitites\\ghost");
            aDictionaryTexturePaths.Add(TexturesEnum.GHOST_FRIGHTENED, "assets\\entitites\\ghost_frightened");
            aDictionaryTexturePaths.Add(TexturesEnum.GHOST_DEAD, "assets\\entitites\\ghost_dead");
            aDictionaryTexturePaths.Add(TexturesEnum.WALL, "assets\\entitites\\wall");
            aDictionaryTexturePaths.Add(TexturesEnum.GHOST_HOUSE, "assets\\entitites\\ghost_house");
            aDictionaryTexturePaths.Add(TexturesEnum.COOKIE, "assets\\entitites\\cookie");
            aDictionaryTexturePaths.Add(TexturesEnum.POWERCOOKIE, "assets\\entitites\\power_cookie");
            aDictionaryTexturePaths.Add(TexturesEnum.FOOD, "assets\\entitites\\cookie");
        }

        protected override void LoadContent()
        {

            aSpriteBatch = new SpriteBatch(GraphicsDevice);

            

            aDebugWallTexture = Content.Load<Texture2D>(aDictionaryTexturePaths[TexturesEnum.WALL]);

            foreach (var itemMazeEntity in aLevelMaze.GetAllEntities())
            {
                itemMazeEntity.LoadContent(Content);
            }

            for (int i = 0; i < aEntityArray.Length; i++)
            {
                aEntityArray[i].LoadContent(Content);
            }

            aUIManager.LoadContent(Content);

            aPacMan.RegisterObserver(aUIManager);
            aPacMan.RegisterObserver(this);

            aMenuDelete.Load(Content);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            aSpriteBatch.Begin();

            aTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (aTimer > 0.1)
            {
                foreach (Entity tmpEntity in aEntityArray)
                {
                    if (tmpEntity is Ghost tmpGhost)
                    {
                        int[] tmpTargetPath = aPathFindingManager.GetShortestPath(tmpGhost.GetTargetPosition(), new DjkistraPathFind());

                        if (tmpTargetPath is not null)
                        {
                            var tmpConstructedPath = aPathFindingManager.ConstructPath(tmpGhost.Position, tmpTargetPath);

                            tmpGhost.SetPath(aPathFindingManager.ConvertTargetIdsToVectorArray(tmpConstructedPath));
                        }
                    }
                }

                aTimer = 0;
            }

            foreach (Entity entity in aEntityArray)
            {
                if (entity is MovableEntity)
                {
                    if (entity is Ghost ghost)

                    {
                        ghost.PursuitTarget();
                    }
                }
            }

            ControlEntityMovement(parGameTime: gameTime);

            aMenuDelete.Update();

            aSpriteBatch.End();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            aSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, null, null, null);

           

            aUIManager.Draw(aSpriteBatch);

            foreach (var itemMaze in aLevelMaze.GetWalls())
            {
                itemMaze.Draw(aSpriteBatch, aXOffset, aYOffset);

            }
            foreach (var itemMaze in aLevelMaze.GetFood())
            {
                if (!((TileEntity)itemMaze).IsHidden)
                {
                    itemMaze.Draw(aSpriteBatch, aXOffset, aYOffset);
                }
            }
            foreach (var itemMaze in aLevelMaze.GetOtherObjects())
            {
                itemMaze.Draw(aSpriteBatch, aXOffset, aYOffset);
            }

            foreach (var itemMaze in aLevelMaze.GetGhostHouse())
            {
                itemMaze.Draw(aSpriteBatch, aXOffset, aYOffset);
            }

            int tmpIndex = 0;

            foreach (Entity entity in aEntityArray)
            {

                if (entity is not null)
                {
                    entity.Draw(aSpriteBatch, aXOffset, aYOffset);


                    if (entity is Ghost tmpGhost)
                    {
                        DrawDebugPath(tmpGhost, tmpIndex);
                    }

                    tmpIndex++;

                }
            }

            aMenuDelete.Draw(aSpriteBatch);

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
                    if (entity is PacMan tmpPacManEntity)
                    {
                        Direction tmpInputDirection = aKeyHandler.GetKeyInput();
                        tmpPacManEntity.ChangeDirection(tmpInputDirection);
                    }

                    entity.Update(parGameTime);


                }
            }

        }

        private void DrawDebugPath(Ghost parGhost, int parIndex)
        {
            var tmpPathArray = parGhost.GetPath().ToArray();

            for (int i = 0; i < tmpPathArray.Length; i++)
            {

                int tmpLeft = (int)tmpPathArray[i].X;
                int tmpTop = (int)tmpPathArray[i].Y;

                Wall tmpWall = new Wall(aDebugWallTexture, tmpLeft, tmpTop, aLevelDirector.TilesScale, Color.White);

                tmpWall.Draw(aSpriteBatch, aXOffset, aYOffset, parIndex);
            }
        }

        public void SetGhostFrightened()
        {

            for (int i = 0; i < ArrayRandomTiles.Length; i++)
            {
                ArrayRandomTiles[i] = GenerateRandomTile();
            }

            foreach (Entity tmpEntity in aEntityArray)
            {
                if (tmpEntity is Ghost tmpGhost)
                {
                    tmpGhost.GhostState.PowerCookieActivated();
                }
            }
        }

        private Vector2 GenerateRandomTile()
        {

            int tmpRandomIndex = aLevelMaze.GetEmptySpaces() is not null ? aRandom.Next(0, aLevelMaze.GetEmptySpaces().Length) : 0;

            return aLevelMaze.GetEmptySpaces() is not null ? aLevelMaze.GetEmptySpaces()[tmpRandomIndex] : Vector2.Zero;
        }

        public Vector2 GetRandomTile(int parIndex)
        {
            return aArrayRandomTiles[parIndex];
        }

        public void Update(Message parMessage)
        {
            if (parMessage is not null && parMessage.ACommand is not null)
            {
                parMessage.ACommand.Execute();
            }
        }

        public Vector2 GetOtherGhostPositionForCyan()
        {
            return aEntityArray[1].Position;
        }

        public Vector2 GetMazeWidth()
        {
           int tmpX = (int)(aLevelDirector.TilesScale * aMazeSizeVector.X);
           int tmpY = (int)(aLevelDirector.TilesScale * aMazeSizeVector.Y);
           return new Vector2(tmpX, tmpY);
        }
    }
}