using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Entities.MovableEntity.Ghosts;

namespace Pac_Man_DesignPatterns.State
{
    public class GhostHomeState : GhostStateAbs
    {
        private Vector2 aGhostHousePos;

        public override Vector2 GetTargetPos()
        {
            return aGhostHousePos;
        }

        public override void ExecuteLogic()
        {
           Ghost.ChangeState(StateEnum.Chase);
        }

        public GhostHomeState(Ghost parGhost, Vector2 parGhostHousePos) : base(parGhost)
        {
            aGhostHousePos = parGhostHousePos;
        }
    }
}
