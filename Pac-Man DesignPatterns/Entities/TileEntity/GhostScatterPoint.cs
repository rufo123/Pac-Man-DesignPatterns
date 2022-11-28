using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Pac_Man_DesignPatterns.Entities.TileEntity
{
    internal class GhostScatterPoint : TileEntity
    {
        public GhostScatterPoint(Texture2D parTexture, int parPositionX, int parPositionY, int parSize, int parRotation = 0) : base(parTexture, parPositionX, parPositionY, parSize, parRotation)
        {
        }
    }
}
