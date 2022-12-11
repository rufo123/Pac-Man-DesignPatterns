using Microsoft.Xna.Framework;

namespace Pac_Man_DesignPatterns.State.PacMan
{
    public abstract class PacManStateAbs
    {

        protected double aTimer;

        protected Entities.MovableEntity.PacMan aPacMan;

        protected PacManStateAbs(Entities.MovableEntity.PacMan parPacMan)
        {
            aPacMan = parPacMan;
            aTimer = 0;
        }

        public virtual void Update(GameTime parGameTime)
        {

        }

        public virtual void Init()
        {
        }

        public virtual void ResetTimer()
        {
            aTimer = 0;
        }


    }
}
