using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Entities;
using Pac_Man_DesignPatterns.Entities.MovableEntity;
using Pac_Man_DesignPatterns.Entities.MovableEntity.Ghosts;
using Pac_Man_DesignPatterns.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_DesignPatterns.Strategy
{
    internal class CyanGhostStrategy : IGhostStrategy
    {
        internal IGhostStrategy IGhostStrategy
        {
            get => default;
            set
            {
            }
        }

        private Ghost aOtherGhost;

        private PacMan aPacman;

        public Vector2 GetTwoTilesAhead(MovableEntity parEntity) {

            int parPositionX = (int)(parEntity.Position.X - (parEntity.Position.X % parEntity.Size));
            int parPositionY = (int)(parEntity.Position.Y - (parEntity.Position.Y % parEntity.Size));

            switch (parEntity.Direction)
            {
                // Two to the up & Two to the left
                case Direction.UP:
                    parPositionY = parPositionY - (parEntity.Size * 2);
                    parPositionX = parPositionX - (parEntity.Size * 2);
                    break;
                case Direction.DOWN:
                    parPositionY = parPositionY + (parEntity.Size * 2);
                    break;
                case Direction.RIGHT:
                    parPositionX = parPositionX + (parEntity.Size * 2);
                    break;
                case Direction.LEFT:
                    parPositionX = parPositionX - (parEntity.Size * 2);
                    break;
                default:
                    break;

            }

            return new Vector2(parPositionX, parPositionY);
        }

        public Vector2 GetChaseTilePos()
        {
            throw new NotImplementedException();
        }

        public Vector2 GetScatterTilePos()
        {
            throw new NotImplementedException();
        }

        
    }
}
