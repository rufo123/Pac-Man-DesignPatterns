using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pac_Man_DesignPatterns.Entities.TileEntity
{
    public class GhostHouse : Wall
    {
        public GhostHouse(string parTexture, int parPositionX, int parPositionY, int parSize, Color parColor, int parRotation = 0) : base(parTexture, parPositionX, parPositionY, parSize, parColor, parRotation)
        {
        }
    }
}
