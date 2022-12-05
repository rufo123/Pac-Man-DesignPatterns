using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Strategy;
using Pac_Man_DesignPatterns.Game;
using Pac_Man_DesignPatterns.Utils;

namespace Pac_Man_DesignPatterns.Entities.MovableEntity.Ghosts
{
    public class Pink : Ghost
    {
        public Pink(string parTexturePath, int parPositionX, int parPositionY, int parSize, Utilities parUtilities, Vector2 parGhostHousePos, IGhostStrategy parGhostStrategy, CollisionDetector parCollisionDetector, string parFrightenedTexturePath, string parDeadTexturePath) : base(parTexturePath, parPositionX, parPositionY, parSize, parUtilities, parGhostHousePos, parGhostStrategy, parCollisionDetector, parFrightenedTexturePath, parDeadTexturePath, Color.Pink)
        {
        }
    }
}
