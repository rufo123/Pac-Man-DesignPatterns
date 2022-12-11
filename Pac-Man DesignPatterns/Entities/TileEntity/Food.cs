using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Command;

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
