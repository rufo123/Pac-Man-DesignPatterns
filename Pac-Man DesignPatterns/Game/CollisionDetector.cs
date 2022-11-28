using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Entities;
using Pac_Man_DesignPatterns.Entities.TileEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_DesignPatterns.Game
{
    public class CollisionDetector
    {
        private TileEntity[] aGameEntitiesArray;

        public CollisionDetector(TileEntity[] parGameEntitiesArray)
        {
            this.aGameEntitiesArray = parGameEntitiesArray;
        }

        public bool DetectCollision(Entity parEntity)
        {
            foreach (var itemEntity in this.aGameEntitiesArray)
            {
                if (itemEntity.GetRectangleHitBox().Intersects(parEntity.GetRectangleHitBox())) {
                    return true;
                }
            }

            return false;
        }

        public bool DetectCollision(Rectangle parRectangle, out Entity parOutCollidedWithEntity)
        {

            int tmpIndex = 0;
            foreach (var itemEntity in this.aGameEntitiesArray)
            {
                tmpIndex++;
                if (itemEntity.GetRectangleHitBox().Intersects(parRectangle))
                {
                    parOutCollidedWithEntity = itemEntity;
                    return true;
                }
            }

            parOutCollidedWithEntity = null;
            return false;
        }
    }
}
