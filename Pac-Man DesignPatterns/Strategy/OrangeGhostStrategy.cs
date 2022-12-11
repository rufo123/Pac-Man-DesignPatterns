using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Entities.MovableEntity.Ghosts;
using Pac_Man_DesignPatterns.Game;

namespace Pac_Man_DesignPatterns.Strategy
{
    public class OrangeGhostStrategy : IGhostStrategy
    {

        private Ghost aGhostMe;


        public void SetGhostToStrategy(Ghost parGhost)
        {
            aGhostMe = parGhost;
        }


        public Vector2 GetChaseTilePos()
        {
            if (Vector2.Distance(GameManager.GetInstance().GetPacManPosition(), aGhostMe.Position) > 8 * aGhostMe.Size)
            {
                return GetScatterTilePos();
            }

            return GameManager.GetInstance().GetPacManPosition();
        }

        public Vector2 GetScatterTilePos()
        {
            return GameManager.GetInstance().GetScatterPointPositionByIndex(3);
        }
    }
}
