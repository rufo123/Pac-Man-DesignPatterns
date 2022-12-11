using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Entities.MovableEntity.Ghosts;
using Pac_Man_DesignPatterns.Game;
using Pac_Man_DesignPatterns.Strategy;

namespace Pac_Man_DesignPatterns.GhostFactory
{
    public class OrangeGhost : GhostFactory
    {
        public override Orange CreateGhost(string parTexturePath, int parXPosition, int parYPosition, int parScale, Vector2 parGhostHousePos, CollisionDetector parCollisionDetector, string parFrightenedTexturePath, string parDeadTexturePath)
        {

            Orange tmpGhost = new Orange(parTexturePath, parXPosition, parYPosition, parScale,  parGhostHousePos, new OrangeGhostStrategy(), parCollisionDetector, parFrightenedTexturePath, parDeadTexturePath);
            ((OrangeGhostStrategy)tmpGhost.GhostStrategy).SetGhostToStrategy(tmpGhost);
            return tmpGhost;
        }
    }
}
