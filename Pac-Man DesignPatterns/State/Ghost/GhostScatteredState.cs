using Microsoft.Xna.Framework;

namespace Pac_Man_DesignPatterns.State.Ghost
{
    public class GhostScatteredState : GhostStateAbs
    {
        public override Vector2 GetTargetPos()
        {
            return Ghost.GhostStrategy.GetScatterTilePos();
        }

        public override void ExecuteLogic()
        {
            if (!IsTimerActive())
            {
                Ghost.ChangeState(GhostStateEnum.Chase);
            }
        }

        public override void PowerCookieActivated()
        {
            Ghost.ChangeState(GhostStateEnum.Frightened);
        }

        public GhostScatteredState(Entities.MovableEntity.Ghosts.Ghost parGhost) : base(parGhost)
        {
        }


    }
}
