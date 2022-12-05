using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pac_Man_DesignPatterns.State;
using Pac_Man_DesignPatterns.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Pac_Man_DesignPatterns.State.MovableEntity;

namespace Pac_Man_DesignPatterns.Entities.MovableEntity
{
    public abstract class MovableEntity : Entity
    {
        private Direction aDirection;

        private Direction aEnqueuedDirection;

        private readonly bool aControlledByUser;

        private readonly int aPixelsToMove;

        private bool aIsBlocked;

        private MovableStateAbs aMovableState;

        private MovableStateAbs aMovableStateUp;

        private MovableStateAbs aMovableStateDown;

        private MovableStateAbs aMovableStateRight;

        private MovableStateAbs aMovableStateLeft;

        private readonly float aSpeed;
        private readonly float aDefaultSpeed;


        protected MovableEntity(string parTexturePath, int parPositionX, int parPositionY, int parSize, bool parControlledByUser, Color parColor) : base(parTexturePath, parPositionX, parPositionY, parSize, parColor)
        {
            this.aControlledByUser = parControlledByUser;
            aPixelsToMove = 64;
            aSpeed =  1;
            aDefaultSpeed = aSpeed;
            InitStates();
        }

        private void InitStates()
        {
            aMovableStateUp = new MovableUp(this);
            aMovableStateRight = new MovableRight(this);
            aMovableStateDown = new MovableDown(this);
            aMovableStateLeft = new MovableLeft(this);

            aMovableState = aMovableStateRight;
        }

        public bool IsBlocked
        {
            get => aIsBlocked;
            set => aIsBlocked = value;
        }

        public bool ControlledByUser => aControlledByUser;

        public Direction Direction
        {
            get => aDirection;
        }

        public Direction EnqueuedDirection
        {
            get => aEnqueuedDirection;
        }

        public void ChangeDirection(Direction parDirection)
        {
            if (parDirection != Direction.NOTHING)
            {
                aEnqueuedDirection = parDirection;
            }
        }

        public float ModPositionAxis(float parPositionAxis)
        {
            return parPositionAxis - parPositionAxis % Size;
        }

        public void AdjustPositionX(bool parToUpperLimit)
        {
            if (parToUpperLimit && Size - (Position.X % Size) < 0.01)
            {
                Position = new Vector2(Position.X + Size - (Position.X % Size), Position.Y);
            }
            else if ((Position.X % Size) < 0.01)
            {
                Position = new Vector2(Position.X - (Position.X % Size), Position.Y);
            }
        }

        public void AdjustPositionY(bool parToUpperLimit)
        {
            if (parToUpperLimit && Size - (Position.Y % Size) < 0.01)
            {
                Position = new Vector2(Position.X, Position.Y + Size - (Position.Y % Size));
            }
            else if ((Position.Y % Size) < 0.01)
            {
                Position = new Vector2(Position.X, Position.Y - (Position.Y % Size));
            }
        }

        public void Move(GameTime parGameTime)
        {

            Debug.WriteLine(parGameTime.ElapsedGameTime.TotalSeconds + " " + this);

            if (!aIsBlocked)
            {
                

                if (aDirection != Direction.NOTHING)
                {
                    aMovableState.Move(aDirection, parGameTime, aPixelsToMove);
                }
            }

            if (aIsBlocked)
            {

                Position = new Vector2((int)Math.Round(Position.X), (int)Math.Round(Position.Y));

                switch (aDirection)
                {
                    case Direction.NOTHING:
                        break;
                    case Direction.UP:
                        if (Position.Y % Size != 0)
                        {
                            Position = new Vector2(Position.X, (int)(Position.Y - (Position.Y % Size)));
                        }
                        AdjustPositionY(false);
                        break;
                    case Direction.DOWN:
                        if (Position.Y % Size != 0 && Position.Y % Size <= 1)
                        {
                            Position = new Vector2(Position.X, (int)(Position.Y - (Position.Y % Size)));
                        }
                        else if (Position.Y % Size != 0 && Position.Y % Size > 1)
                        {
                            Position = new Vector2(Position.X, (int)(Position.Y - (Position.Y % Size) + Size));

                        }
                        AdjustPositionY(true);

                        break;
                    case Direction.LEFT:
                        if (Position.X % Size != 0)
                        {
                            Position = new Vector2((int)(Position.X - (Position.X % Size)), Position.Y);
                        }
                        AdjustPositionX(false);
                        break;
                    case Direction.RIGHT:
                        if (Position.X % Size != 0 && Position.X % Size <= 1)
                        {
                            Position = new Vector2((int)(Position.X - (Position.X % Size)), Position.Y);
                        }
                        else if (Position.X % Size != 0 && Position.X % Size > 1)
                        {
                            Position = new Vector2((int)(Position.X - (Position.X % Size) + Size), Position.Y);
                        }
                        AdjustPositionX(true);
                        break;

                }

            }
        }

        public void ChangeEnqueuedDirectionToDirection()
        {

            if (aEnqueuedDirection != Direction.NOTHING)
            {
                aMovableState.ChangeState(aEnqueuedDirection);
            }

        }

        public Rectangle PredictRectangleNextPos(GameTime parGameTime, Direction parDirection)
        {
            float deltaTime = (float)parGameTime.ElapsedGameTime.TotalSeconds;

            switch (parDirection)
            {
                case Direction.UP:
                    return new Rectangle((int)Position.X, (int)Position.Y - (int)Math.Round((GetSpeed() * aPixelsToMove) * deltaTime), Size, Size);
                case Direction.DOWN:
                    return new Rectangle((int)Position.X, (int)Position.Y + (int)Math.Round((GetSpeed() * aPixelsToMove) * deltaTime), Size, Size);
                case Direction.LEFT:
                    return new Rectangle((int)Position.X - (int)Math.Round((GetSpeed() * aPixelsToMove) * deltaTime), (int)Position.Y, Size, Size);
                case Direction.RIGHT:
                    return new Rectangle((int)Position.X + (int)Math.Round((GetSpeed() * aPixelsToMove) * deltaTime), (int)Position.Y, Size, Size);
                default:
                    return GetRectangleHitBox();

            }

        }

        public void ChangeMovableState(Direction parDirection, bool parCanRotate180 = true)
        {
            switch (parDirection)
            {
                case Direction.UP:
                    aMovableState = aMovableStateUp;
                    break;
                case Direction.DOWN:
                    aMovableState = aMovableStateDown;
                    break;
                case Direction.LEFT:
                    aMovableState = aMovableStateLeft;
                    break;
                case Direction.RIGHT:
                    aMovableState = aMovableStateRight;
                    break;
            }

            aDirection = parDirection;
            aEnqueuedDirection = Direction.NOTHING;
        }

        public override float GetRotation()
        {
            return aMovableState.GetRotation();
        }

        public override SpriteEffects GetSpriteEffects()
        {
            return aMovableState.GetSpriteEffects();
        }

        public virtual float GetSpeed()
        {
            return aSpeed;
        }

        public float GetDefaultSpeed()
        {
            return aDefaultSpeed;
        }

    }
}
