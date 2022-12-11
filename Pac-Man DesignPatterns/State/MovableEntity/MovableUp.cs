using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Utils;

namespace Pac_Man_DesignPatterns.State.MovableEntity
{
    public class MovableUp : MovableStateAbs
    {
        public override int GetRotation()
        {
            return 270;
        }


        public MovableUp(Entities.MovableEntity.MovableEntity parMovableEntity) : base(parMovableEntity)
        {
        }

        public override void Move(Direction parDirection, GameTime parGameTime, int parPixelsToMove)
        {

            float deltaTime = (float)parGameTime.ElapsedGameTime.TotalSeconds;

            
            MovableEntity.Position = new Vector2(MovableEntity.ModPositionAxis(MovableEntity.Position.X), MovableEntity.Position.Y - (MovableEntity.GetSpeed() * parPixelsToMove) * deltaTime);
            MovableEntity.AdjustPositionY(false);
        }

        public override bool ChangeState(Direction parDirection)
        {
            if (!MovableEntity.ControlledByUser)
            {
                if (parDirection != Direction.Down || MovableEntity.IsBlocked)
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
