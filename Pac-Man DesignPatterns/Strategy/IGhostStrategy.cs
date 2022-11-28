using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_DesignPatterns.Strategy
{
    public interface IGhostStrategy
    {
        public Vector2 GetChaseTilePos();

        public Vector2 GetScatterTilePos();
    }
}
