using Pac_Man_DesignPatterns.Entities;
using Pac_Man_DesignPatterns.Entities.MovableEntity;
using Pac_Man_DesignPatterns.Entities.MovableEntity.Ghosts;
using Pac_Man_DesignPatterns.Game;
using Pac_Man_DesignPatterns.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Design;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Vector3 = Microsoft.Xna.Framework.Vector3;

namespace Pac_Man_DesignPatterns.Strategy
{
    internal class CyanGhostStrategy : IGhostStrategy
    {

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

            double tmpSinus = Math.Sin(180);
            double tmpCosinus = Math.Cos(180);

            // Translate Point Back To Origin

            Vector2 tmpPacPos = GameManager.GetInstance().Game.PacMan.Position;

            var varPointOrX= GameManager.GetInstance().Game.GetOtherGhostPositionForCyan().X - tmpPacPos.X;
            var varPointOrY= GameManager.GetInstance().Game.GetOtherGhostPositionForCyan().Y - tmpPacPos.Y;

            // Rotate

            double xnew = varPointOrX * tmpCosinus - varPointOrY * tmpSinus;
            double ynew = varPointOrX * tmpSinus + varPointOrY * tmpCosinus;
            
            // Translate Back

            varPointOrX = (float)(xnew + tmpPacPos.X);
            varPointOrY = (float)(ynew + tmpPacPos.Y);


            return new Vector2(varPointOrX - varPointOrX % GameManager.GetInstance().Game.PacMan.Size, varPointOrY - varPointOrY % GameManager.GetInstance().Game.PacMan.Size);

        }

        public Vector2 GetScatterTilePos()
        {
            return GameManager.GetInstance().Game.ScatterPoints[2].Position;
        }

        
    }
}
