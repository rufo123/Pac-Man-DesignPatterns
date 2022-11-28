using Pac_Man_DesignPatterns.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            switch (parMessage.Id)
            {
                case -1:
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
    }
}
