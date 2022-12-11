using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Game;
using Pac_Man_DesignPatterns.Strategy;

namespace Pac_Man_DesignPatterns.Entities.MovableEntity.Ghosts
{
    public class Red : Ghost
    {
        public Red(string parTexturePath, int parPositionX, int parPositionY, int parSize, IGhostStrategy parGhostStrategy, Vector2 parGhostHousePos, CollisionDetector parCollisionDetector, string parFrightenedTexturePath, string parDeadTexturePath) : base(parTexturePath, parPositionX, parPositionY, parSize, parGhostHousePos, parGhostStrategy, parCollisionDetector, parFrightenedTexturePath, parDeadTexturePath, Color.Red)
        {
            
        }
    }
}
