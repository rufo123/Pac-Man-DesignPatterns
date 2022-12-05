using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_DesignPatterns.Entities.TileEntity
{
    public class Wall : TileEntity
    {
        public Wall(Texture2D parTexture, int parPositionX, int parPositionY, int parSize, Color parColor, int parRotation = 0) : base(parTexture, parPositionX, parPositionY, parSize, parColor, parRotation)
        {
        }

        public Wall(string parTexturePath, int parPositionX, int parPositionY, int parSize, Color parColor, int parRotation = 0) : base(parTexturePath, parPositionX, parPositionY, parSize, parColor, parRotation)
        {
        }
    }
}
