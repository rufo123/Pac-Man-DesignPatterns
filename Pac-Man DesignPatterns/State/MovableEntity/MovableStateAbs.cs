using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pac_Man_DesignPatterns.Utils;

namespace Pac_Man_DesignPatterns.State.MovableEntity
{
    public class MovableStateAbs
    {
        private readonly Entities.MovableEntity.MovableEntity aMovableEntity;

        public Entities.MovableEntity.MovableEntity MovableEntity => aMovableEntity;

        public MovableStateAbs(Entities.MovableEntity.MovableEntity parMovableEntity)
        {
            aMovableEntity = parMovableEntity;
        }

        public virtual int GetRotation()
        {
            return 0;
        }

        public virtual SpriteEffects GetSpriteEffects()
        {
            return SpriteEffects.None;
        }

        public virtual void Move(Direction parDirection, GameTime parGameTime, int parPixelsToMove )
        {
        }

        public virtual bool ChangeState(Direction parDirection)
        {
            MovableEntity.ChangeMovableState(parDirection);
            return true;
        }
    }
}
