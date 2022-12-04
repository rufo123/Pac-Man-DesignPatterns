using Microsoft.Xna.Framework.Graphics;
using Pac_Man_DesignPatterns.Entities.MovableEntity.Ghosts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Game;
using Pac_Man_DesignPatterns.Strategy;
using Pac_Man_DesignPatterns.Utils;

namespace Pac_Man_DesignPatterns.GhostFactory
{
    public class CyanGhost : GhostFactory
    {

        public override Ghost CreateGhost(string parTexturePath, int parXPosition, int parYPosition, int parScale, Vector2 parGhostHousePos, CollisionDetector parCollisionDetector, string parFrightenedTexturePath, string parDeadTexturePath)
        {
            return new Cyan(parTexturePath, parXPosition, parYPosition, parScale, null, parGhostHousePos, new CyanGhostStrategy(), parCollisionDetector, parFrightenedTexturePath, parDeadTexturePath);
        }
    }
}
