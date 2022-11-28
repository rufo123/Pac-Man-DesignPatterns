using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Entities.MovableEntity.Ghosts;
using Pac_Man_DesignPatterns.Utils;

namespace Pac_Man_DesignPatterns.State
{
    public class GhostFrightenedState : GhostStateAbs
    {

        private int aTimer;

        private int aTimerThresholdSeconds;

        private Utilities aUtilities;

        public GhostFrightenedState(Ghost parGhost) : base(parGhost)
        {
            aTimerThresholdSeconds = 10;
            aTimer = -1;
        }

        public override Vector2 GetTargetPos()
        {
            return aUtilities.GetRandomTile();
        }

        public override void ExecuteLogic()
        {
      
        }

   


        public override void Initialize(Utilities parUtilities)
        {
            aUtilities = parUtilities;
        }
    }
}
