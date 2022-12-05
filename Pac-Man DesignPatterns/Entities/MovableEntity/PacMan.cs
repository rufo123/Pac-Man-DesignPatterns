using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pac_Man_DesignPatterns.Game;
using Pac_Man_DesignPatterns.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pac_Man_DesignPatterns.Entities.MovableEntity.Ghosts;
using Pac_Man_DesignPatterns.Entities.TileEntity;

namespace Pac_Man_DesignPatterns.Entities.MovableEntity
{
    public class PacMan : MovableEntity, IObservable
    {
        private bool aIsDead;

        private double aDeadTimer;

        private readonly int aDeadTimerThreshold;

        private readonly IList<IObserver> aListObservers;

        private readonly CollisionDetector aCollisionDetector;

        public Utils.IObservable Implementation
        {
            get => default;
            set
            {
            }
        }

        public IObserver IObserver { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public GameManager GameManager { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public UIManager UIManager { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public PacMan(string parTexturePath, int parPositionX, int parPositionY, int parSize, CollisionDetector parCollisionDetector, Color parColor) : base(parTexturePath, parPositionX, parPositionY, parSize, true, parColor)
        {
            aListObservers = new List<IObserver>();
            aCollisionDetector = parCollisionDetector;
            aIsDead = false;
            aDeadTimer = -1;
            aDeadTimerThreshold = 3;
        }

        public override void Update(GameTime parGameTime)
        {

            if (aIsDead)
            {
                DeadLogic(parGameTime);
            }

            if (!aIsDead)
            {
                aCollisionDetector.DetectCollision(this.PredictRectangleNextPos(parGameTime, this.EnqueuedDirection), out Entity[] tmpEntityCollidedWithEveryTick);

                if (tmpEntityCollidedWithEveryTick is not null)
                {
                    for (int i = 0; i < tmpEntityCollidedWithEveryTick.Length; i++)
                    {
                        if (tmpEntityCollidedWithEveryTick[i] is Food && !((Food)tmpEntityCollidedWithEveryTick[i]).IsHidden)
                        {
                            FoodEaten((Food)tmpEntityCollidedWithEveryTick[i]);
                        }

                        if (tmpEntityCollidedWithEveryTick[i] is Ghost)
                        {
                            GhostCollision((Ghost)tmpEntityCollidedWithEveryTick[i]);
                        }
                    }
                }


                if (this.EnqueuedDirection != Direction.NOTHING)
                {

                    bool tmpCollided = aCollisionDetector.DetectCollision(this.PredictRectangleNextPos(parGameTime, this.EnqueuedDirection), out Entity[] tmpEntityCollidedWith);

                    if (!tmpCollided || tmpEntityCollidedWith is null)
                    {
                        this.ChangeEnqueuedDirectionToDirection();
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
                            this.ChangeEnqueuedDirectionToDirection();
                        }
                    }

                }

                aCollisionDetector.DetectCollision(this.PredictRectangleNextPos(parGameTime, this.Direction), out Entity[] tmpEntityCollidedWithNext);


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

                    if (tmpShouldBeStopped)
                    {
                        this.IsBlocked = true;
                    }
                    else
                    {
                        this.IsBlocked = false;
                    }
                }


                //Debug.WriteLine(tmpIsCollisionDetected);
                Move(parGameTime);
            }

            base.Update(parGameTime);
        }

        public void DeadLogic(GameTime parGameTime)
        {
            if (aIsDead)
            {
                aDeadTimer += parGameTime.ElapsedGameTime.TotalSeconds;

                if (aDeadTimer > aDeadTimerThreshold)
                {
                    aIsDead = false;
                    aDeadTimer = 0;
                }
            }
        }

        public void SeatDead()
        {
            aIsDead = true;
        }

        public void GhostCollision(Ghost parGhost)
        {
            foreach (var itemObserver in aListObservers)
            {
                itemObserver.Update(new Message(MessageCodes.GhostCollision, parGhost));
            }
        }

        public void FoodEaten(Food parFood)
        {
            foreach (var itemObserver in aListObservers)
            {
                itemObserver.Update(new Message(MessageCodes.CookieEaten, parFood));
            }
        }

        public void RegisterObserver(IObserver parObserver)
        {
            aListObservers.Add(parObserver);
        }

        public void RemoveObserver(IObserver parObserver)
        {
            aListObservers.Remove(parObserver);
        }

        public void Notify()
        {
            throw new NotImplementedException();
        }
    }
}
