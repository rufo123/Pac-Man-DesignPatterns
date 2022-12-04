using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pac_Man_DesignPatterns.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pac_Man_DesignPatterns.Command;
using Pac_Man_DesignPatterns.Game;
using Pac_Man_DesignPatterns.Utils;
using Pac_Man_DesignPatterns.Entities.TileEntity;
using Pac_Man_DesignPatterns.State;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;
using Pac_Man_DesignPatterns.State.Ghost;

namespace Pac_Man_DesignPatterns.Entities.MovableEntity.Ghosts
{
    public abstract class Ghost : MovableEntity, ICommand
    {

        protected IGhostStrategy aGhostStrategy;

        protected GhostStateAbs aGhostState;

        private GhostStateAbs aChaseState;
        private GhostStateAbs aDeadState;
        private GhostStateAbs aHomeState;
        private GhostStateAbs aFrightenedState;
        private GhostStateAbs aScatteredState;

        private readonly Vector2 aGhostHousePos;

        private readonly CollisionDetector aCollisionDetector;

        private readonly GameManager aGameManager;

        private readonly string aFrightenedTexturePath;
        private readonly string aDeadTexturePath;

        private Stack<Vector2> aStackPath;

        private Vector2 aTarget;

        private Vector2 aTargetSource;

        // Stack na Path.

        public IGhostStrategy GhostStrategy => aGhostStrategy;


        protected Ghost(string parTexturePath, int parPositionX, int parPositionY, int parSize, Utilities parUtilities, Vector2 parGhostHousePos, IGhostStrategy parGhostStrategy, CollisionDetector parCollisionDetector, string parFrightenedTexturePath, string parDeadTexturePath) : base(parTexturePath, parPositionX, parPositionY, parSize, false)
        {
            aGhostHousePos = parGhostHousePos;
            aGhostStrategy = parGhostStrategy;
            aFrightenedTexturePath = parFrightenedTexturePath;
            aDeadTexturePath = parDeadTexturePath;
            InitStates();
            aCollisionDetector = parCollisionDetector;
            aGameManager = GameManager.GetInstance();
            aTarget = Position;
            aStackPath = new Stack<Vector2>();
            aTargetSource = Position;
        }

        private void InitStates()
        {
            aChaseState = new GhostChaseState(this);
            aDeadState = new GhostDeadState(this, aGhostHousePos, aDeadTexturePath);
            aHomeState = new GhostHomeState(this, aGhostHousePos);
            aFrightenedState = new GhostFrightenedState(this, aFrightenedTexturePath);
            aScatteredState = new GhostScatteredState(this);

            aGhostState = aHomeState;
        }

        public void ChangeState(GhostStateEnum parGhostState)
        {
            switch (parGhostState)
            {
                case GhostStateEnum.Chase:
                    aGhostState = aChaseState;
                    break;
                case GhostStateEnum.Frightened:
                    aGhostState = aFrightenedState;
                    break;
                case GhostStateEnum.Home:
                    aGhostState = aHomeState;
                    break;
                case GhostStateEnum.Scatter:
                    aGhostState = aScatteredState;
                    break;
                case GhostStateEnum.Dead:
                    aGhostState = aDeadState;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(parGhostState), parGhostState, null);
            }
        }

        public override void LoadContent(ContentManager parContent)
        {

            aChaseState.LoadAlternativeTexture(parContent);
            aFrightenedState.LoadAlternativeTexture(parContent);
            aScatteredState.LoadAlternativeTexture(parContent);
            aDeadState.LoadAlternativeTexture(parContent);
            aHomeState.LoadAlternativeTexture(parContent);
            aGhostState.LoadAlternativeTexture(parContent);

            base.LoadContent(parContent);
        }

        public Vector2 GetTargetSourcePosition()
        {
            return aTargetSource;
        }

        public void SetTargetSourcePosition(Vector2 parTargetSourcePosition)
        {
            aTargetSource = parTargetSourcePosition;
        }


        public Vector2 GetTargetPosition()
        {
            return aGhostState.GetTargetPos();
        }

        public void PursuitTarget()
        {

            Vector2 parTaget = aTarget;

            if (Vector2.Distance(parTaget, Position) <  1)
            {
                bool tmpCanBePopped = aStackPath.TryPop(out Vector2 tmpNewTarget);

                if (tmpCanBePopped)
                {
                    aTarget = tmpNewTarget;
                }
            }

        /*    if (GetRectangleHitBox().Intersects(new Rectangle((int)aTarget.X, (int)aTarget.Y, (int)aTarget.X * Size,
                    (int)aTarget.Y * Size)))
            {
                bool tmpCanBePopped = aStackPath.TryPop(out Vector2 tmpNewTarget);

                if (tmpCanBePopped)
                {
                    aTarget = tmpNewTarget;
                }
            } */

            int tmpRoundedTargetX = (int)Position.X - ((int)Position.X % Size);
            int tmpRoundedTargetY = (int)Position.Y - ((int)Position.Y % Size);

            if (aTarget.X == tmpRoundedTargetX)
            {
                if ((int)tmpRoundedTargetY < aTarget.Y)
                {
                    ChangeDirection(Utils.Direction.DOWN);
                }
                else if ((int)tmpRoundedTargetY == aTarget.Y)
                {
                   // ChangeDirection(Utils.Direction.NOTHING);
                }
                else {
                    ChangeDirection(Utils.Direction.UP);
                }
            }
            else if (aTarget.Y == tmpRoundedTargetY) {
                if ((int)tmpRoundedTargetX < aTarget.X)
                {
                    ChangeDirection(Utils.Direction.RIGHT);
                }
                else if ((int)tmpRoundedTargetX == aTarget.X)
                {
                  //  ChangeDirection(Utils.Direction.NOTHING);
                }
                else {
                    ChangeDirection(Utils.Direction.LEFT);
                }
            }

        }

   

        public override void Update(GameTime parGameTime)
        {

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
                        if (tmpEntityCollidedWith[i] is Wall && (tmpEntityCollidedWith[i] is not GhostHouse || aGhostState is GhostHomeState) )
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
                    if (tmpEntityCollidedWithNext[i] is Wall && tmpEntityCollidedWithNext[i] is not GhostHouse || aGhostState is GhostHomeState)
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
            else
            {
                this.IsBlocked = false;
            }


            aGhostState.Update(parGameTime);
            Move(parGameTime);
            base.Update(parGameTime);
        }

        public GhostStateAbs GhostState => aGhostState;
        public void Execute()
        {
            if (aGhostState == aFrightenedState)
            {
                ChangeState(GhostStateEnum.Dead);
               
            } else if (aGhostState != aDeadState)
            {
                aGameManager.ReSpawnPacMan();
            }
        }

        public void SetPath(Vector2[] parPath)
        {
            aStackPath = new Stack<Vector2>(parPath.Reverse());

            bool tmpCanBePop = aStackPath.TryPop(out Vector2 tmpPopped);

            if (tmpCanBePop)
            {
                aTarget = tmpPopped;
            }

        }

        public Stack<Vector2> GetPath()
        {
            return aStackPath;
        }


        public override Texture2D GetTexture()
        {
            return aGhostState.GetAlternativeTexture() is not null ? aGhostState.GetAlternativeTexture() : aTexture;
        }

        public override float GetSpeed()
        {
            return aGhostState.GetSpeed();
        }
    }
}
