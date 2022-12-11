using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Game;
using Pac_Man_DesignPatterns.Utils;
using Pac_Man_DesignPatterns.Utils.Multiton;

namespace Pac_Man_DesignPatterns.Strategy
{
    internal class CyanGhostStrategy : IGhostStrategy
    {

        public Vector2 GetTwoTilesAhead(Direction parEntityDirection, Vector2 parEntityPosition, int parEntitySize)
        {

            return DirectionMultiton.GetInstance(
                parEntityDirection).
                    GetPositionAhead(
                        parEntityPosition,
                        parEntitySize,
                        2
                );
        }

        public Vector2 GetChaseTilePos()
        {
            GameManager tmpGameManager = GameManager.GetInstance();

            Vector2 tmpPacPos = GetTwoTilesAhead(tmpGameManager.GetPacManDirection(), tmpGameManager.GetPacManPosition(), tmpGameManager.GetPacManSize());
            Vector2 tmpGhostPos = GameManager.GetInstance().GetOtherGhostPositionForCyan();

            var tmpDistance = tmpPacPos - tmpGhostPos;

            return tmpPacPos + tmpDistance;
        }

        public Vector2 GetScatterTilePos()
        {
            return GameManager.GetInstance().GetScatterPointPositionByIndex(2);
        }


    }
}
