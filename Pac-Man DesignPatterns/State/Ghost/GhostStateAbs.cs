using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pac_Man_DesignPatterns.State.Ghost
{
    public abstract class GhostStateAbs
    {

        private readonly Entities.MovableEntity.Ghosts.Ghost aGhost;

        public Entities.MovableEntity.Ghosts.Ghost Ghost => aGhost;

        private double aTimer;

        protected int aTimerThreshold;

        public int TimerThreshold => aTimerThreshold;

        public double Timer
        {
            get => aTimer;
            set => aTimer = value;
        }

        protected GhostStateAbs(Entities.MovableEntity.Ghosts.Ghost parGhost)
        {
            aGhost = parGhost;
            aTimerThreshold = 10;
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

        public virtual void Update(GameTime parGameTime)
        {
            Debug.WriteLine(this);
            TimerTick(parGameTime);
        }

        public virtual Vector2 GetTargetPos()
        {
            return Vector2.Zero;
        }

        public virtual Texture2D GetAlternativeTexture()
        {
            return null;
        }

        public virtual void ExecuteLogic()
        {

        }

        public virtual void PowerCookieActivated()
        {

        }

        public virtual void LoadAlternativeTexture(ContentManager parContent)
        {
            
        }

        public virtual float GetSpeed()
        {
            return Ghost.GetDefaultSpeed();
        }


    }
}
