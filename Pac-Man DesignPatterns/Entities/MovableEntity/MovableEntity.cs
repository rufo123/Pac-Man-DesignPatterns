using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pac_Man_DesignPatterns.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_DesignPatterns.Entities.MovableEntity
{
    public abstract class MovableEntity : Entity
    {
        private Direction aDirection;

        private Direction aEnqueuedDirection;

        private bool aControlledByUser;

        private int aPixelsToMove;

        private bool aIsBlocked;

        protected MovableEntity(Texture2D parTexture, int parPositionX, int parPositionY, int parSize, bool parControlledByUser) : base(parTexture, parPositionX, parPositionY, parSize)
        {
            this.aControlledByUser = parControlledByUser;
            aPixelsToMove = 64;
        }

        public bool IsBlocked
        {
            get => aIsBlocked;
            set => aIsBlocked = value;
        }

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

        private float ModPositionAxis(float parPositionAxis)
        {
            return parPositionAxis - parPositionAxis % Size;
        }

        private void AdjustPositionX(bool parToUpperLimit)
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

        private void AdjustPositionY(bool parToUpperLimit)
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

                float deltaTime = (float)parGameTime.ElapsedGameTime.TotalSeconds;

                switch (aDirection)
                {
                    case Direction.NOTHING:

                        break;
                    case Direction.UP:
                        Position = new Vector2(ModPositionAxis(Position.X), Position.Y - aPixelsToMove * deltaTime);
                        AdjustPositionY(false);
                        break;
                    case Direction.DOWN:
                        Position = new Vector2(ModPositionAxis(Position.X), Position.Y + aPixelsToMove * deltaTime);
                        AdjustPositionY(true);
                        break;
                    case Direction.LEFT:
                        Position = new Vector2(Position.X - aPixelsToMove * deltaTime, ModPositionAxis(Position.Y));
                        AdjustPositionX(false);
                        break;
                    case Direction.RIGHT:
                        Position = new Vector2(Position.X + aPixelsToMove * deltaTime, ModPositionAxis(Position.Y));
                        AdjustPositionX(true);
                        break;
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

                var test = 64.354 % Size;


            }
        }

        public void ChangeEnqueuedDirectionToDirection()
        {

            if (aEnqueuedDirection != Direction.NOTHING)
            {
                aDirection = aEnqueuedDirection;
            }

            aEnqueuedDirection = Direction.NOTHING;
        }

        public Rectangle PredictRectangleNextPos(GameTime parGameTime, Direction parDirection)
        {
            float deltaTime = (float)parGameTime.ElapsedGameTime.TotalSeconds;

            switch (parDirection)
            {
                case Direction.UP:
                    return new Rectangle((int)Position.X, (int)Position.Y - (int)Math.Round(aPixelsToMove * deltaTime), Size, Size);
                case Direction.DOWN:
                    return new Rectangle((int)Position.X, (int)Position.Y + (int)Math.Round(aPixelsToMove * deltaTime), Size, Size);
                case Direction.LEFT:
                    return new Rectangle((int)Position.X - (int)Math.Round(aPixelsToMove * deltaTime), (int)Position.Y, Size, Size);
                case Direction.RIGHT:
                    return new Rectangle((int)Position.X + (int)Math.Round(aPixelsToMove * deltaTime), (int)Position.Y, Size, Size);
                default:
                    return GetRectangleHitBox();

            }

        }



    }
}
