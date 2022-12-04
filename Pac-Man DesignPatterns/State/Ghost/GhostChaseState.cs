using Microsoft.Xna.Framework;

namespace Pac_Man_DesignPatterns.State.Ghost
{
    public class GhostChaseState : GhostStateAbs
    {
        public override Vector2 GetTargetPos()
        {
           return Ghost.GhostStrategy.GetChaseTilePos();
        }

        public override void ExecuteLogic()
        {
            if (!IsTimerActive())
            {
                Ghost.ChangeState(GhostStateEnum.Scatter);
            }
        }

        public override void PowerCookieActivated()
        {
            Ghost.ChangeState(GhostStateEnum.Frightened);
        }


        public GhostChaseState(Entities.MovableEntity.Ghosts.Ghost parGhost) : base(parGhost)
        {
        }
    }
}
