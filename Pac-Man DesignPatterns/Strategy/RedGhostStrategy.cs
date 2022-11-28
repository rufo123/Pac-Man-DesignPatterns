using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pac_Man_DesignPatterns.Game;

namespace Pac_Man_DesignPatterns.Strategy
{
    internal class RedGhostStrategy : IGhostStrategy
    {

        public Vector2 GetChaseTilePos()
        {
           return GameManager.GetInstance().Game.PacMan.Position;
        }

        public Vector2 GetScatterTilePos()
        {
            return GameManager.GetInstance().Game.ScatterPoints[0].Position;
        }
    }
}
