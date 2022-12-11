using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Entities.MovableEntity.Ghosts;
using Pac_Man_DesignPatterns.Game;

// ReSharper disable UnusedMemberInSuper.Global

namespace Pac_Man_DesignPatterns.GhostFactory
{
    public abstract class GhostFactory
    {
        public abstract Ghost CreateGhost(string parTexturePath, int parXPosition, int parYPosition, int parScale, Vector2 parGhostHousePos, CollisionDetector parCollisionDetector, string parFrightenedTexturePath, string parDeadTexturePath);
    }
}
