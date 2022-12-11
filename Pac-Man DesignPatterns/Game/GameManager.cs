using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pac_Man_DesignPatterns.State.PacMan;
using Pac_Man_DesignPatterns.Utils;

namespace Pac_Man_DesignPatterns.Game
{
    public sealed class GameManager : IObserver
    {

        private static GameManager _staticGameManager;

        public static GameManager GetInstance()
        {

            // ReSharper disable once ConvertIfStatementToNullCoalescingExpression
            if (_staticGameManager == null)
            {
                _staticGameManager = new GameManager();
            }

            return _staticGameManager;
        }

        private Game Game { get; }

        private GameManager()
        {
            Game = new Game();
        }

        public void Update(Message parMessage)
        {
            switch (parMessage.MessageCode)
            {
                case MessageCodes.CookieEaten:
                    if (parMessage.ACommand is not null)
                    {
                        parMessage.ACommand.Execute();
                    }
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

        public void KillPacMan()
        {
            Game.PacMan.ChangeState(PacManStateEnum.Dead);
        }

        public PacManStateEnum GetPacManState()
        {
            return Game.PacMan.GetState();
        }

        public Vector2 GetOtherGhostPositionForCyan()
        {
            return Game.GetOtherGhostPositionForCyan();
        }

        public void StartGame()
        {
            Game.GameState = GameState.Playing;
            Game.RestartGame();
        }


        public GraphicsDevice GetGraphicDevice()
        {
            return Game.GraphicsDevice;
        }

        public Vector2 GetMazeWidth()
        {
            return Game.GetMazeWidth();
        }

        public void RunGame()
        {
            Game.Run();
        }

        public Vector2 GetPacManPosition()
        {
            return Game.PacMan.Position;
        }

        public Direction GetPacManDirection()
        {
            return Game.PacMan.Direction;
        }

        public int GetPacManSize()
        {
            return Game.PacMan.Size;
        }

        public Vector2 GetScatterPointPositionByIndex(int parIndex)
        {
            int tmpIndex = parIndex;

            while (tmpIndex != -1)
            {
                if (Game.ScatterPoints.Length >= parIndex + 1)
                {
                    return Game.ScatterPoints[tmpIndex].Position;
                }
                tmpIndex--;
            }

            return Vector2.Zero;
        }

        public void ExitGame()
        {
            Game.Exit();
        }

        public void GetGraphicDeviceSamplerState(SamplerState parSamplerState)
        {
            Game.GraphicsDevice.SamplerStates[0] = parSamplerState;
        }

        public void AddScore(int parScore)
        {
            Game.UiManager.AddScore(parScore);
        }

        public void TakeLives(int parNumberOfLives)
        {
            Game.UiManager.TakeLives(parNumberOfLives);
        }
    }
}
