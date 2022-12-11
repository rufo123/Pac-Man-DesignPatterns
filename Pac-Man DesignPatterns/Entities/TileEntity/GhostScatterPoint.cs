using Microsoft.Xna.Framework;

namespace Pac_Man_DesignPatterns.Entities.TileEntity
{
    internal class GhostScatterPoint : TileEntity
    {
        public GhostScatterPoint(string parTexturePath, int parPositionX, int parPositionY, int parSize, Color parColor, int parRotation = 0) : base(parTexturePath, parPositionX, parPositionY, parSize, parColor, parRotation)
        {
        }
    }
}
