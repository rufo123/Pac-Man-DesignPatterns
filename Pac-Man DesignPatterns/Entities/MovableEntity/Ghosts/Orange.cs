using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Game;
using Pac_Man_DesignPatterns.Strategy;

namespace Pac_Man_DesignPatterns.Entities.MovableEntity.Ghosts
{
    public class Orange : Ghost
    {
        public Orange(string parTexturePath, int parPositionX, int parPositionY, int parSize, Vector2 parGhostHousePos, IGhostStrategy parGhostStrategy, CollisionDetector parCollisionDetector, string parFrightenedTexturePath, string parDeadTexturePath) : base(parTexturePath, parPositionX, parPositionY, parSize, parGhostHousePos, parGhostStrategy, parCollisionDetector, parFrightenedTexturePath, parDeadTexturePath, Color.Orange)
        {
        }
    }
}
