using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Entities.MovableEntity;
using Pac_Man_DesignPatterns.Game;
using Pac_Man_DesignPatterns.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_DesignPatterns.Strategy
{
    internal class PinkGhostStrategy : IGhostStrategy
    {

        

        internal IGhostStrategy IGhostStrategy
        {
            get => default;
            set
            {
            }
        }

        public Vector2 GetChaseTilePos()
        {
            return GetFourTilesAhead(GameManager.GetInstance().Game.PacMan);
        }

        public Vector2 GetScatterTilePos()
        {
            return GameManager.GetInstance().Game.ScatterPoints[1].Position;
        }

        public Vector2 GetFourTilesAhead(MovableEntity parEntity)
        {

            int tmpPositionX = (int)(parEntity.Position.X - (parEntity.Position.X % parEntity.Size));
            int tmpPositionY = (int)(parEntity.Position.Y - (parEntity.Position.Y % parEntity.Size));

            switch (parEntity.Direction)
            {
                // Two to the up & Two to the left
                case Direction.UP:
                    tmpPositionY -= (parEntity.Size * 4);
                    tmpPositionX -= (parEntity.Size * 4);
                    break;
                case Direction.DOWN:
                    tmpPositionY += (parEntity.Size * 4);
                    break;
                case Direction.RIGHT:
                    tmpPositionX += (parEntity.Size * 4);
                    break;
                case Direction.LEFT:
                    tmpPositionX -= (parEntity.Size * 4);
                    break;
                default:
                    break;

            }

            return new Vector2(tmpPositionX, tmpPositionY);
        }
    }
}
