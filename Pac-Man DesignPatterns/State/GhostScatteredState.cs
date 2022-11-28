using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Entities.MovableEntity.Ghosts;

namespace Pac_Man_DesignPatterns.State
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
                Ghost.ChangeState(StateEnum.Chase);
            }
        }

        public override void PowerCookieActivated()
        {
            Ghost.ChangeState(StateEnum.Frightened);
        }

        public GhostScatteredState(Ghost parGhost) : base(parGhost)
        {
        }


    }
}
