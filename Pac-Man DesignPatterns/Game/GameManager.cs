using Pac_Man_DesignPatterns.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Pac_Man_DesignPatterns.Game
{
    public sealed class GameManager : IObserver
    {
        public int aGameWidth;
        public int aGameHeight;

        private static GameManager _staticGameManager;

        public static GameManager GetInstance()
        {

            if (_staticGameManager == null)
            {
                _staticGameManager = new GameManager();
            }

            return _staticGameManager;
        }

        public Game Game { get; }

        private GameManager()
        {
            Game = new Game();
        }

        public Level.LevelDirector LevelDirector
        {
            get => default;
            set
            {
            }
        }

        public Level.ILevelBuilder ILevelBuilder
        {
            get => default;
            set
            {
            }
        }

        public Level.IMazeProduct IMazeProduct
        {
            get => default;
            set
            {
            }
        }


        public UIManager UIManager
        {
            get => default;
            set
            {
            }
        }

        public KeyHandler KeyHandler
        {
            get => default;
            set
            {
            }
        }

        public Utils.IObserver Implementation
        {
            get => default;
            set
            {
            }
        }

        internal Menu.MenuManager MenuManager
        {
            get => default;
            set
            {
            }
        }

        public void GetInput(KeyHandler parKey)
        {

        }

        public void Update(Message parMessage)
        {
            switch (parMessage.MessageCode)
            {
                case MessageCodes.CookieEaten:
                    //Cosi
                    if (parMessage.ACommand is not null)
                    {
                        parMessage.ACommand.Execute();
                    }
                    break;
                default:
                    break;
            }
        }

        public void SetGhostsFrightened()
        {
            Game.SetGhostFrightened();
        }

        public Vector2 GetRandomTile(int parIndex)
        {
           return Game.GetRandomTile(parIndex);
        }

        public void ReSpawnPacMan()
        {
            Game.PacMan.SeatDead();
        }

        public Vector2 GetOtherGhostPositionForCyan()
        {
            return Game.GetOtherGhostPositionForCyan();
        }
    }
}
