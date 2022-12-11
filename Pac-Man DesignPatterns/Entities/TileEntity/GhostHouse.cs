using Microsoft.Xna.Framework;

namespace Pac_Man_DesignPatterns.Entities.TileEntity
{
    public class GhostHouse : Wall
    {
        public GhostHouse(string parTexture, int parPositionX, int parPositionY, int parSize, Color parColor, int parRotation = 0) : base(parTexture, parPositionX, parPositionY, parSize, parColor, parRotation)
        {
        }
    }
}
