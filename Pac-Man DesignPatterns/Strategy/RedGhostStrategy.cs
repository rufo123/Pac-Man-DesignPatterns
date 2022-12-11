using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Game;

namespace Pac_Man_DesignPatterns.Strategy
{
    internal class RedGhostStrategy : IGhostStrategy
    {

        public Vector2 GetChaseTilePos()
        {
           return GameManager.GetInstance().GetPacManPosition();
        }

        public Vector2 GetScatterTilePos()
        {
            return GameManager.GetInstance().GetScatterPointPositionByIndex(0);
        }
    }
}
