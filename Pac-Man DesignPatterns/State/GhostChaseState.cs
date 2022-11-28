using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Entities.MovableEntity.Ghosts;

namespace Pac_Man_DesignPatterns.State
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
                Ghost.ChangeState(StateEnum.Scatter);
            }
        }

        public override void PowerCookieActivated()
        {
            Ghost.ChangeState(StateEnum.Frightened);
        }


        public GhostChaseState(Ghost parGhost) : base(parGhost)
        {
        }
    }
}
