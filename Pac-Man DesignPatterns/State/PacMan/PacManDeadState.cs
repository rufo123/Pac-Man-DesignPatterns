using Microsoft.Xna.Framework;

namespace Pac_Man_DesignPatterns.State.PacMan
{
    public class PacManDeadState : PacManStateAbs
    {



        private readonly double aDeadTimerThreshold;

        private double aBlinkingInterval;

        private readonly double aBlinkingIntervalMax;

        public PacManDeadState(Entities.MovableEntity.PacMan parPacMan) : base(parPacMan)
        {
            aDeadTimerThreshold = 5;
            aBlinkingInterval = 0.5;
            aBlinkingIntervalMax = aBlinkingInterval;
        }

        public override void Update(GameTime parGameTime)
        {

            if (aTimer <= 0)
            {
                Init();
                aPacMan.ReSpawn();
            }

            aTimer += parGameTime.ElapsedGameTime.TotalSeconds;

            if (aTimer > aDeadTimerThreshold)
            {
                aPacMan.IsHidden = false;
                aTimer = 0;
                aPacMan.ChangeState(PacManStateEnum.Alive);
                return;
            }

            aBlinkingInterval = (aBlinkingIntervalMax / aTimer);

            if (aTimer % (1 - aBlinkingIntervalMax / aTimer) > aBlinkingInterval)
            {
                aPacMan.IsHidden = false;
                return;
            }

            if (aTimer % (1 - aBlinkingIntervalMax / aTimer) < aBlinkingInterval)
            {
                aPacMan.IsHidden = true;
            }

          

        }
    }
}
