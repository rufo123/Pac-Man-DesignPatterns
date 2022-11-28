using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pac_Man_DesignPatterns.Game;

namespace Pac_Man_DesignPatterns.Entities
{
    public abstract class Entity
    {

        private int aRotation;

        private int aSize;

        private Vector2 aPosition;
        protected Color aColor;

        private float aSpeed;
        private bool isDrawn;

        private Texture2D aTexture;

        public int Size
        {
            get => aSize;
        }

        protected Entity(Texture2D parTexture, int parPositionX, int parPositionY, int parSize, int parRotation = 0)
        {
            aSize = parSize;

            aPosition = new Vector2(parPositionX, parPositionY);
            isDrawn = true;
            aTexture = parTexture;

            aColor = Color.White;

            aRotation = parRotation;

        }

        public Game.Game Agregation
        {
            get => default;
            set
            {
            }
        }

        public Vector2 Position
        {
            get => aPosition;
            set => aPosition = value;
        }

        public int Rotation 
        {
            get => aRotation;
        }

        public Rectangle GetRectangleHitBox()
        {
            return new Rectangle((int)aPosition.X, (int)aPosition.Y, aSize, aSize);
        }

        public virtual void Update(GameTime parGameTime)
        {
        }

        public virtual void Draw(SpriteBatch parSpriteBatch)
        {
            SpriteEffects tmpEffects = SpriteEffects.None;
            Color tmpColor = Color.White;

            if (aRotation == 90)
            {
               // tmpEffects = SpriteEffects.FlipHorizontally;
            }
            else if (aRotation == 180)
            {
                // tmpEffects = SpriteEffects.FlipVertically | SpriteEffects.FlipHorizontally;
            }
            else if (aRotation == 270) {
               // tmpEffects = SpriteEffects.FlipVertically;
            }



            Vector2 originOffset = new Vector2(aTexture.Width / 2, aTexture.Height / 2);
            parSpriteBatch.Draw(aTexture, GetRectangleHitBox(), new Rectangle(0, 0, aTexture.Height, aTexture.Height), tmpColor, MathHelper.ToRadians(aRotation), originOffset, tmpEffects, 0);
        }

    }
}
