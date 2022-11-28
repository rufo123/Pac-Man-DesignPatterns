using Microsoft.Xna.Framework.Graphics;
using Pac_Man_DesignPatterns.Entities.MovableEntity.Ghosts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Strategy;
using Pac_Man_DesignPatterns.Game;

namespace Pac_Man_DesignPatterns.GhostFactory
{
    public class RedGhost : GhostFactory
    {
        public override Red CreateGhost(Texture2D parTexture, int parXPosition, int parYPosition, int parScale, Vector2 parGhostHousePos, CollisionDetector parCollisionDetector)
        {
            return new Red(parTexture, parXPosition, parYPosition, parScale, new RedGhostStrategy(), parGhostHousePos, parCollisionDetector);
        }
    }
}
