using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Utils;

namespace Pac_Man_DesignPatterns.State.MovableEntity
{
    public class MovableRight : MovableStateAbs
    {
        public override int GetRotation()
        {
            return 0;
        }


        public MovableRight(Entities.MovableEntity.MovableEntity parMovableEntity) : base(parMovableEntity)
        {

        }

        public override void Move(Direction parDirection, GameTime parGameTime, int parPixelsToMove)
        {
            float deltaTime = (float)parGameTime.ElapsedGameTime.TotalSeconds;

            MovableEntity.Position = new Vector2(MovableEntity.Position.X + (MovableEntity.GetSpeed() * parPixelsToMove) * deltaTime, MovableEntity.ModPositionAxis(MovableEntity.Position.Y));
            MovableEntity.AdjustPositionY(false);


        }

        public override bool ChangeState(Direction parDirection)
        {
            if (!MovableEntity.ControlledByUser)
            {
                if (parDirection != Direction.Left || MovableEntity.IsBlocked)
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
