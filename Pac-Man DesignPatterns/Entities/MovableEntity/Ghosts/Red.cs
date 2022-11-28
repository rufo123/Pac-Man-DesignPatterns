using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pac_Man_DesignPatterns.Game;
using Pac_Man_DesignPatterns.Strategy;

namespace Pac_Man_DesignPatterns.Entities.MovableEntity.Ghosts
{
    public class Red : Ghost
    {
        public Red(Texture2D parTexture2D, int parPositionX, int parPositionY, int parSize, IGhostStrategy parGhostStrategy, Vector2 parGhostHousePos, CollisionDetector parCollisionDetector) : base(parTexture2D, parPositionX, parPositionY, parSize, null, parGhostHousePos, parGhostStrategy, parCollisionDetector)
        {
            this.aColor = Color.Red;
        }
    }
}
