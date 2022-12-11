using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Entities;
using Pac_Man_DesignPatterns.Entities.TileEntity;
using Pac_Man_DesignPatterns.Game;
using Pac_Man_DesignPatterns.Utils;

namespace Pac_Man_DesignPatterns.State.PacMan
{
    public class PacManAliveState : PacManStateAbs
    {

        public PacManAliveState(Entities.MovableEntity.PacMan parPacMan) : base(parPacMan)
        {
        }

        public override void Init()
        {
            aPacMan.ReSpawn();
        }

        public override void Update(GameTime parGameTime)
        {

            if (aTimer <= 0)
            {
                Init();
                aTimer += parGameTime.TotalGameTime.TotalSeconds;
            }

            aPacMan.CollisionDetector.DetectCollision(aPacMan.PredictRectangleNextPos(parGameTime, aPacMan.EnqueuedDirection), out Entity[] tmpEntityCollidedWithEveryTick);

            if (tmpEntityCollidedWithEveryTick is not null)
            {
                for (int i = 0; i < tmpEntityCollidedWithEveryTick.Length; i++)
                {
                    if (tmpEntityCollidedWithEveryTick[i] is Food && !((Food)tmpEntityCollidedWithEveryTick[i]).IsHidden)
                    {
                        aPacMan.FoodEaten((Food)tmpEntityCollidedWithEveryTick[i]);
                    }

                    if (tmpEntityCollidedWithEveryTick[i] is Entities.MovableEntity.Ghosts.Ghost)
                    {
                        aPacMan.GhostCollision((Entities.MovableEntity.Ghosts.Ghost)tmpEntityCollidedWithEveryTick[i]);
                    }
                }
            }
            if (aPacMan.EnqueuedDirection != Direction.Nothing)
            {
                bool tmpCollided = aPacMan.CollisionDetector.DetectCollision(aPacMan.PredictRectangleNextPos(parGameTime, aPacMan.EnqueuedDirection), out Entity[] tmpEntityCollidedWith);

                if (!tmpCollided || tmpEntityCollidedWith is null)
                {
                    aPacMan.ChangeEnqueuedDirectionToDirection();
                }

                if (tmpEntityCollidedWith is not null)
                {
                    bool tmpShouldBeStopped = false;

                    for (int i = 0; i < tmpEntityCollidedWith.Length; i++)
                    {
                        if (tmpEntityCollidedWith[i] is Wall)
                        {
                            tmpShouldBeStopped = true;
                        }
                    }
                    if (!tmpShouldBeStopped)
                    {
                        aPacMan.ChangeEnqueuedDirectionToDirection();
                    }
                }
            }
            aPacMan.CollisionDetector.DetectCollision(aPacMan.PredictRectangleNextPos(parGameTime, aPacMan.Direction), out Entity[] tmpEntityCollidedWithNext);


            if (tmpEntityCollidedWithNext is not null)
            {
                bool tmpShouldBeStopped = false;

                for (int i = 0; i < tmpEntityCollidedWithNext.Length; i++)
                {
                    if (tmpEntityCollidedWithNext[i] is Wall)
                    {
                        tmpShouldBeStopped = true;
                    }
                }

                aPacMan.IsBlocked = tmpShouldBeStopped;
            }


            //Debug.WriteLine(tmpIsCollisionDetected);
            aPacMan.Move(parGameTime);

            aPacMan.CollisionDetector.EdgeTeleporter((int)GameManager.GetInstance().GetMazeWidth().X, (int)GameManager.GetInstance().GetMazeWidth().Y, aPacMan);
        }

        
    }
}
