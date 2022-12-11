using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Game;
using Pac_Man_DesignPatterns.Utils;
using Pac_Man_DesignPatterns.Utils.Multiton;

namespace Pac_Man_DesignPatterns.Strategy
{
    internal class PinkGhostStrategy : IGhostStrategy
    {
        public Vector2 GetChaseTilePos()
        {
            GameManager tmpGameManager = GameManager.GetInstance();

            return GetFourTilesAhead(tmpGameManager.GetPacManDirection(), tmpGameManager.GetPacManPosition(), tmpGameManager.GetPacManSize());
        }

        public Vector2 GetScatterTilePos()
        {
            return GameManager.GetInstance().GetScatterPointPositionByIndex(1);
        }

        public Vector2 GetFourTilesAhead(Direction parEntityDirection, Vector2 parEntityPosition, int parEntitySize)
        {
            return DirectionMultiton.GetInstance(parEntityDirection).GetPositionAhead(parEntityPosition, parEntitySize, 4);
        }
    }
}
