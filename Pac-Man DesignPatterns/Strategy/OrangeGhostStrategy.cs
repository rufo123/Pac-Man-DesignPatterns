using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pac_Man_DesignPatterns.Game;
using Pac_Man_DesignPatterns.Entities.MovableEntity.Ghosts;

namespace Pac_Man_DesignPatterns.Strategy
{
    internal class OrangeGhostStrategy : IGhostStrategy
    {

        private Ghost aGhostMe;


        internal IGhostStrategy IGhostStrategy
        {
            get => default;
            set
            {
            }
        }

        public void SetGhostToStrategy(Ghost parGhost)
        {
            aGhostMe = parGhost;
        }


        public Vector2 GetChaseTilePos()
        {
            if (Vector2.Distance(GameManager.GetInstance().Game.PacMan.Position, aGhostMe.Position) > 8 * aGhostMe.Size)
            {
                return GetScatterTilePos();
            }

            return GameManager.GetInstance().Game.PacMan.Position;
        }

        public Vector2 GetScatterTilePos()
        {
            return GameManager.GetInstance().Game.ScatterPoints[3].Position;
        }
    }
}
