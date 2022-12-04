using Microsoft.Xna.Framework.Graphics;
using Pac_Man_DesignPatterns.Entities.MovableEntity.Ghosts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Game;

namespace Pac_Man_DesignPatterns.GhostFactory
{
    public abstract class GhostFactory
    {
        public abstract Ghost CreateGhost(string parTexturePath, int parXPosition, int parYPosition, int parScale, Vector2 parGhostHousePos, CollisionDetector parCollisionDetector, string parFrightenedTexturePath, string parDeadTexturePath);
    }
}
