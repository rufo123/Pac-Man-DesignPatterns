using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Strategy;
using Pac_Man_DesignPatterns.Game;

namespace Pac_Man_DesignPatterns.Entities.MovableEntity.Ghosts
{
    public class Pink : Ghost
    {
        public Pink(Texture2D parTexture2D, int parPositionX, int parPositionY, int parSize, IGhostStrategy parGhostStrategy, CollisionDetector parCollisionDetector) : base(parTexture2D ,parPositionX, parPositionY, parSize, null, Vector2.Zero, parGhostStrategy, parCollisionDetector)
        {
        }
    }
}
