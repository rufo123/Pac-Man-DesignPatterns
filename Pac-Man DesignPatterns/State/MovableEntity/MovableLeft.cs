using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pac_Man_DesignPatterns.Utils;

namespace Pac_Man_DesignPatterns.State.MovableEntity
{
    public class MovableLeft : MovableStateAbs
    {
        public override int GetRotation()
        {
            return 180;
        }

        public override SpriteEffects GetSpriteEffects()
        {
            return SpriteEffects.FlipVertically;
        }

        public MovableLeft(Entities.MovableEntity.MovableEntity parMovableEntity) : base(parMovableEntity)
        {
        }

        public override void Move(Direction parDirection, GameTime parGameTime, int parPixelsToMove)
        {
            float deltaTime = (float)parGameTime.ElapsedGameTime.TotalSeconds;

            MovableEntity.Position = new Vector2(MovableEntity.Position.X - (MovableEntity.GetSpeed() * parPixelsToMove) * deltaTime, MovableEntity.ModPositionAxis(MovableEntity.Position.Y));
            MovableEntity.AdjustPositionX(false);
        }

        public override bool ChangeState(Direction parDirection)
        {
            if (!MovableEntity.ControlledByUser)
            {
                if (parDirection != Direction.Right || MovableEntity.IsBlocked)
                {
                    MovableEntity.ChangeMovableState(parDirection);
                    return true;
                }
                return false;
            }

            MovableEntity.ChangeMovableState(parDirection);
            return true;
        }
    }
}
