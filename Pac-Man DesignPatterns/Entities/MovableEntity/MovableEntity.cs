using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pac_Man_DesignPatterns.Command;
using Pac_Man_DesignPatterns.State.MovableEntity;
using Pac_Man_DesignPatterns.Utils;

namespace Pac_Man_DesignPatterns.Entities.MovableEntity
{
    public abstract class MovableEntity : Entity, ICommandReSpawn
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

        private readonly Vector2 aSpawnPoint;


        protected MovableEntity(string parTexturePath, int parPositionX, int parPositionY, int parSize, bool parControlledByUser, Color parColor) : base(parTexturePath, parPositionX, parPositionY, parSize, parColor)
        {
            aControlledByUser = parControlledByUser;
            aPixelsToMove = 64;
            aSpeed =  1;
            aDefaultSpeed = aSpeed;
            aSpawnPoint = new Vector2(parPositionX, parPositionY);
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

        public Direction Direction => aDirection;

        public Direction EnqueuedDirection => aEnqueuedDirection;

        public void ChangeDirection(Direction parDirection)
        {
            if (parDirection != Direction.Nothing)
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
                

                if (aDirection != Direction.Nothing)
                {
                    aMovableState.Move(aDirection, parGameTime, aPixelsToMove);
                }
            }

            if (aIsBlocked)
            {

                Position = new Vector2((int)Math.Round(Position.X), (int)Math.Round(Position.Y));

                switch (aDirection)
                {
                    case Direction.Nothing:
                        break;
                    case Direction.Up:
                        if (Position.Y % Size != 0)
                        {
                            Position = new Vector2(Position.X, (int)(Position.Y - (Position.Y % Size)));
                        }
                        AdjustPositionY(false);
                        break;
                    case Direction.Down:
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
                    case Direction.Left:
                        if (Position.X % Size != 0)
                        {
                            Position = new Vector2((int)(Position.X - (Position.X % Size)), Position.Y);
                        }
                        AdjustPositionX(false);
                        break;
                    case Direction.Right:
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

            if (aEnqueuedDirection != Direction.Nothing)
            {
                aMovableState.ChangeState(aEnqueuedDirection);
            }

        }

        public Rectangle PredictRectangleNextPos(GameTime parGameTime, Direction parDirection)
        {
            float deltaTime = (float)parGameTime.ElapsedGameTime.TotalSeconds;

            switch (parDirection)
            {
                case Direction.Up:
                    return new Rectangle((int)Position.X, (int)Position.Y - (int)Math.Round((GetSpeed() * aPixelsToMove) * deltaTime), Size, Size);
                case Direction.Down:
                    return new Rectangle((int)Position.X, (int)Position.Y + (int)Math.Round((GetSpeed() * aPixelsToMove) * deltaTime), Size, Size);
                case Direction.Left:
                    return new Rectangle((int)Position.X - (int)Math.Round((GetSpeed() * aPixelsToMove) * deltaTime), (int)Position.Y, Size, Size);
                case Direction.Right:
                    return new Rectangle((int)Position.X + (int)Math.Round((GetSpeed() * aPixelsToMove) * deltaTime), (int)Position.Y, Size, Size);
                default:
                    return GetRectangleHitBox();

            }

        }

        public void ChangeMovableState(Direction parDirection, bool parCanRotate180 = true)
        {
            switch (parDirection)
            {
                case Direction.Up:
                    aMovableState = aMovableStateUp;
                    break;
                case Direction.Down:
                    aMovableState = aMovableStateDown;
                    break;
                case Direction.Left:
                    aMovableState = aMovableStateLeft;
                    break;
                case Direction.Right:
                    aMovableState = aMovableStateRight;
                    break;
            }

            aDirection = parDirection;
            aEnqueuedDirection = Direction.Nothing;
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

        public virtual void ReSpawn()
        {
            Position = aSpawnPoint;
        }
    }
}
