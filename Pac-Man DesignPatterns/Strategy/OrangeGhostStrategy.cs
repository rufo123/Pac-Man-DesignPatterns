using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_DesignPatterns.Strategy
{
    internal class OrangeGhostStrategy : IGhostStrategy
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
            return IGhostStrategy.GetChaseTilePos();
        }

        public Vector2 GetScatterTilePos()
        {
            return IGhostStrategy.GetScatterTilePos();
        }
    }
}
