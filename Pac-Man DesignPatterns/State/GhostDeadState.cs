using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Entities.MovableEntity.Ghosts;

namespace Pac_Man_DesignPatterns.State
{
    public class GhostDeadState : GhostStateAbs
    {
        private Vector2 aGhostHousePos;

        private bool aReachedHome;

        public override Vector2 GetTargetPos()
        {
            return aGhostHousePos;
        }

        public override void ExecuteLogic()
        {
            Ghost.ChangeState(StateEnum.Home);
        }

        public GhostDeadState(Ghost parGhost, Vector2 parGhostHousePos) : base(parGhost)
        {
            this.aGhostHousePos = parGhostHousePos;
        }

        public override void Update(GameTime parGameTime)
        {

            if (Ghost.Position.Equals(this.aGhostHousePos))
            {
                aReachedHome = true;
                Timer = TimerThreshold;
            }

            if (aReachedHome)
            {
                base.Update(parGameTime);
            }
        }
    }
}
