using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_DesignPatterns.Entities.TileEntity
{
    public abstract class TileEntity : Entity
    {

        protected bool aIsHidden;

        protected TileEntity(string parTexturePath, int parPositionX, int parPositionY, int parSize, int parRotation = 0) : base(parTexturePath, parPositionX, parPositionY, parSize, parRotation)
        {
            aIsHidden = false;
        }

        protected TileEntity(Texture2D parTexture, int parPositionX, int parPositionY, int parSize, int parRotation = 0) : base(parTexture, parPositionX, parPositionY, parSize, parRotation)
        {
            aIsHidden = false;
        }

        public bool IsHidden => aIsHidden;
    }
}
