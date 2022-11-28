using Microsoft.Xna.Framework;
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
            throw new NotImplementedException();
        }

        public Vector2 GetScatterTilePos()
        {
            throw new NotImplementedException();
        }
    }
}
