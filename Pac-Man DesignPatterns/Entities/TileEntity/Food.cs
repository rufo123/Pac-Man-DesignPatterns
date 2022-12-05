using Microsoft.Xna.Framework.Graphics;
using Pac_Man_DesignPatterns.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Pac_Man_DesignPatterns.Entities.TileEntity
{
    public abstract class Food : TileEntity, ICommand
    {
        protected Food(string parTexturePath, int parPositionX, int parPositionY, int parSize, Color parColor) : base(parTexturePath, parPositionX, parPositionY, parSize, parColor)
        {
        }

        public virtual void Execute()
        {
            
        }
    }
}
