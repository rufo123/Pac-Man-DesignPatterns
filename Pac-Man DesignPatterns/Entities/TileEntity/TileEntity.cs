using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pac_Man_DesignPatterns.Entities.TileEntity
{
    public abstract class TileEntity : Entity
    {

 

        protected TileEntity(string parTexturePath, int parPositionX, int parPositionY, int parSize, Color parColor, int parRotation = 0) : base(parTexturePath, parPositionX, parPositionY, parSize, parColor, parRotation)
        {
  
        }

        protected TileEntity(Texture2D parTexture, int parPositionX, int parPositionY, int parSize, Color parColor, int parRotation = 0) : base(parTexture, parPositionX, parPositionY, parSize, parColor, parRotation)
        {
    
        }

  
    }
}
