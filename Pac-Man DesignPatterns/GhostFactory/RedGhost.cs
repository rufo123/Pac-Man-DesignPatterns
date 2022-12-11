using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Entities.MovableEntity.Ghosts;
using Pac_Man_DesignPatterns.Game;
using Pac_Man_DesignPatterns.Strategy;

namespace Pac_Man_DesignPatterns.GhostFactory
{
    public class RedGhost : GhostFactory
    {
        public override Red CreateGhost(string parTexturePath, int parXPosition, int parYPosition, int parScale, Vector2 parGhostHousePos, CollisionDetector parCollisionDetector, string parFrightenedTexturePath, string parDeadTexturePath)
        {
            return new Red(parTexturePath, parXPosition, parYPosition, parScale, new RedGhostStrategy(), parGhostHousePos, parCollisionDetector, parFrightenedTexturePath, parDeadTexturePath);
        }
    }
}
