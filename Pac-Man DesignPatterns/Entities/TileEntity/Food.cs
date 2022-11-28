using Microsoft.Xna.Framework.Graphics;
using Pac_Man_DesignPatterns.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_DesignPatterns.Entities.TileEntity
{
    public abstract class Food : TileEntity, ICommand
    {
        protected Food(Texture2D parTexture2D,int parPositionX, int parPositionY, int parSize) : base(parTexture2D ,parPositionX, parPositionY, parSize)
        {
        }

        public virtual void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
