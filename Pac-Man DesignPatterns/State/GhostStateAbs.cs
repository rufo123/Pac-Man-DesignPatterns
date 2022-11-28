using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Entities.MovableEntity.Ghosts;
using Pac_Man_DesignPatterns.Utils;

namespace Pac_Man_DesignPatterns.State
{
    public abstract class GhostStateAbs
    {

        private Ghost aGhost;

        public Ghost Ghost => aGhost;

        private double aTimer;

        private readonly int aTimerThreshold;

        public int TimerThreshold => aTimerThreshold;

        public double Timer
        {
            get => aTimer;
            set => aTimer = value;
        }

        protected GhostStateAbs(Ghost parGhost)
        {
            this.aGhost = parGhost;
            this.aTimerThreshold = 10;
        }


        public bool IsTimerActive()
        {
            return aTimer >= 0;
        }

        private void ResetTimer()
        {
            aTimer = -1;
        }

        private void TimerTick(GameTime parGameTime)
        {

            if (aTimer >= 0)
            {
                aTimer += parGameTime.ElapsedGameTime.TotalSeconds;
            }

            if (aTimer > aTimerThreshold)
            {
                ResetTimer();
                ExecuteLogic();
                InitTimer();
            }
        }

        protected void InitTimer()
        {
            aTimer = 0;
        }

        public virtual void Initialize(Utilities parUtilities)
        {
        }

        public virtual void Update(GameTime parGameTime)
        {
            Debug.WriteLine(this);
            TimerTick(parGameTime);
        }

        public virtual Vector2 GetTargetPos()
        {
            return Vector2.Zero;
        }

        public virtual void ExecuteLogic()
        {

        }

        public virtual void PowerCookieActivated()
        {

        }


    }
}
