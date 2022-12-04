using Microsoft.Xna.Framework;

namespace Pac_Man_DesignPatterns.State.Ghost
{
    public class GhostHomeState : GhostStateAbs
    {
        private readonly Vector2 aGhostHousePos;

        public override Vector2 GetTargetPos()
        {
            return aGhostHousePos;
        }

        public override void ExecuteLogic()
        {
           Ghost.ChangeState(GhostStateEnum.Chase);
        }

        public GhostHomeState(Entities.MovableEntity.Ghosts.Ghost parGhost, Vector2 parGhostHousePos) : base(parGhost)
        {
            aGhostHousePos = parGhostHousePos;
        }
    }
}
