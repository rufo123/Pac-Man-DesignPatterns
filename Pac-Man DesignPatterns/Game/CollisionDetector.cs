using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Entities;
using Pac_Man_DesignPatterns.Entities.TileEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Pac_Man_DesignPatterns.Entities.MovableEntity;

namespace Pac_Man_DesignPatterns.Game
{
    public class CollisionDetector
    {
        private readonly Entity[] aGameEntitiesArray;

        public CollisionDetector(Entity[] parGameEntitiesArray)
        {
            this.aGameEntitiesArray = parGameEntitiesArray;
        }

        public CollisionDetector(List<Entity> parGameEntitiesList)
        {
            this.aGameEntitiesArray = parGameEntitiesList.ToArray();
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

        public bool DetectCollision(Rectangle parRectangle, out Entity[] parOutCollidedWithEntity)
        {
            bool tmpCollidedWithEntity = false;
            parOutCollidedWithEntity = null;
            List<Entity> tmpListCollidedWithEntities = new List<Entity>();

            int tmpIndex = 0;
            foreach (var itemEntity in this.aGameEntitiesArray)
            {
                tmpIndex++;
                if (itemEntity.GetRectangleHitBox().Intersects(parRectangle))
                {
                    tmpCollidedWithEntity = true;
                    tmpListCollidedWithEntities.Add(itemEntity);

                }
            }

            if (tmpCollidedWithEntity)
            {
                parOutCollidedWithEntity = tmpListCollidedWithEntities.ToArray();
                return true;
            }

            return false;
        }

        public void EdgeTeleporter(int parMazeWidth, int parMazeHeight, Entity parEntity)
        {
            // Teleport To The Right
            if (parEntity.Position.X <= 0)
            {
                parEntity.Position = new Vector2(parMazeWidth - parEntity.Size, parEntity.Position.Y);
            }

            // Teleport To The Left
            if (parEntity.Position.X >= parMazeWidth)
            {
                parEntity.Position = new Vector2(0, parEntity.Position.Y);
            }

            // Teleport To The Up
            if (parEntity.Position.Y >= parMazeHeight)
            {
                parEntity.Position = new Vector2(parEntity.Position.X, 0);
            }

            // Teleport To The Down
            if (parEntity.Position.Y <= 0)
            {
                parEntity.Position = new Vector2(parEntity.Position.X, parEntity.Position.Y - parEntity.Size);
            }
        }
    }
}
