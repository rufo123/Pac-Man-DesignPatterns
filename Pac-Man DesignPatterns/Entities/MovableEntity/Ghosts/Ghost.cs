using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pac_Man_DesignPatterns.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pac_Man_DesignPatterns.Game;
using Pac_Man_DesignPatterns.Utils;
using Pac_Man_DesignPatterns.State;
using Pac_Man_DesignPatterns.Entities.TileEntity;

namespace Pac_Man_DesignPatterns.Entities.MovableEntity.Ghosts
{
    public abstract class Ghost : MovableEntity
    {
        private bool isChasingPacMan;
        private double aFrightenedTimer;
        private Utilities aUtilities;

        protected IGhostStrategy aGhostStrategy;

        protected GhostStateAbs aGhostState;

        private GhostStateAbs aChaseState;
        private GhostStateAbs aDeadState;
        private GhostStateAbs aHomeState;
        private GhostStateAbs aFrightenedState;
        private GhostStateAbs aScatteredState;

        private Vector2 aGhostHousePos;

        private CollisionDetector aCollisionDetector;

        public IGhostStrategy GhostStrategy => aGhostStrategy;


        protected Ghost(Texture2D parTexture, int parPositionX, int parPositionY, int parSize, Utilities parUtilities, Vector2 parGhostHousePos, IGhostStrategy parGhostStrategy, CollisionDetector parCollisionDetector) : base(parTexture, parPositionX, parPositionY, parSize, false)
        {
            aUtilities = parUtilities;
            aGhostHousePos = parGhostHousePos;
            aGhostStrategy = parGhostStrategy;
            InitStates();
            aCollisionDetector = parCollisionDetector;
        }

        private void InitStates()
        {
            aChaseState = new GhostChaseState(this);
            aDeadState = new GhostDeadState(this, aGhostHousePos);
            aHomeState = new GhostHomeState(this, aGhostHousePos);
            aFrightenedState = new GhostFrightenedState(this);
            aScatteredState = new GhostScatteredState(this);

            aGhostState = aHomeState;
        }

        public void ChangeState(StateEnum parState)
        {
            switch (parState)
            {
                case StateEnum.Chase:
                    aGhostState = aChaseState;
                    break;
                case StateEnum.Frightened:
                    aGhostState = aFrightenedState;
                    break;
                case StateEnum.Home:
                    aGhostState = aHomeState;
                    break;
                case StateEnum.Scatter:
                    aGhostState = aScatteredState;
                    break;
                case StateEnum.Dead:
                    aGhostState = aDeadState;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(parState), parState, null);
            }
        }


        public Vector2 GetTargetPosition()
        {
            return aGhostState.GetTargetPos();
        }

        public void PursuitTarget(Vector2 parTarget) {


            int tmpRoundedTargetX = (int)Position.X - ((int)Position.X % Size);
            int tmpRoundedTargetY = (int)Position.Y - ((int)Position.Y % Size);

            if (parTarget.X == tmpRoundedTargetX)
            {
                if ((int)tmpRoundedTargetY < parTarget.Y)
                {
                    ChangeDirection(Utils.Direction.DOWN);
                }
                else if ((int)tmpRoundedTargetY == parTarget.Y)
                {
                   // ChangeDirection(Utils.Direction.NOTHING);
                }
                else {
                    ChangeDirection(Utils.Direction.UP);
                }
            }
            else if (parTarget.Y == tmpRoundedTargetY) {
                if ((int)tmpRoundedTargetX < parTarget.X)
                {
                    ChangeDirection(Utils.Direction.RIGHT);
                }
                else if ((int)tmpRoundedTargetX == parTarget.X)
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

                bool tmpCollided = aCollisionDetector.DetectCollision(this.PredictRectangleNextPos(parGameTime, this.EnqueuedDirection), out Entity tmpEntityCollidedWith);

                if (!tmpCollided || tmpEntityCollidedWith is not Wall)
                {
                    this.ChangeEnqueuedDirectionToDirection();
                }
            }

            bool tmpIsCollisionDetected = aCollisionDetector.DetectCollision(this.PredictRectangleNextPos(parGameTime, this.Direction), out Entity tmpEntityCollidedWithNext);

            if (tmpEntityCollidedWithNext is Wall)
            {
                this.IsBlocked = true;
            }
            else
            {
                this.IsBlocked = false;
            }


            aGhostState.Update(parGameTime);
            Move(parGameTime);
            base.Update(parGameTime);
        }
    }
}
