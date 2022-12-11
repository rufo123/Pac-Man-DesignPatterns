using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Utils;

namespace Pac_Man_DesignPatterns.State.MovableEntity
{
    public class MovableDown : MovableStateAbs
    {
        public override int GetRotation()
        {
            return 90;
        }


        public MovableDown(Entities.MovableEntity.MovableEntity parMovableEntity) : base(parMovableEntity)
        {
        }


        public override void Move(Direction parDirection, GameTime parGameTime, int parPixelsToMove)
        {
            float deltaTime = (float)parGameTime.ElapsedGameTime.TotalSeconds;

            MovableEntity.Position = new Vector2(MovableEntity.ModPositionAxis(MovableEntity.Position.X), MovableEntity.Position.Y + (MovableEntity.GetSpeed() * parPixelsToMove) * deltaTime);
            MovableEntity.AdjustPositionY(true);
        }

        public override bool ChangeState(Direction parDirection)
        {
            if (!MovableEntity.ControlledByUser)
            {
                if (parDirection != Direction.Up || MovableEntity.IsBlocked)
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
