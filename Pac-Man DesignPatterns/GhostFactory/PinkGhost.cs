using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Entities.MovableEntity.Ghosts;
using Pac_Man_DesignPatterns.Game;
using Pac_Man_DesignPatterns.Strategy;

namespace Pac_Man_DesignPatterns.GhostFactory
{
    public class PinkGhost : GhostFactory
    {
        public override Pink CreateGhost(string parTexturePath, int parXPosition, int parYPosition, int parScale, Vector2 parGhostHousePos, CollisionDetector parCollisionDetector, string parFrightenedTexturePath, string parDeadTexturePath)
        {
            return new Pink(parTexturePath, parXPosition, parYPosition, parScale, parGhostHousePos, new PinkGhostStrategy(), parCollisionDetector, parFrightenedTexturePath, parDeadTexturePath);
        }

    }
}
