using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pac_Man_DesignPatterns.Game;

namespace Pac_Man_DesignPatterns.Entities
{
    public abstract class Entity
    {

        protected int aRotation;

        private readonly int aSize;

        private Vector2 aPosition;
        protected Color aColor;

        protected Texture2D aTexture;

        private readonly string aTexturePath;

        public int Size
        {
            get => aSize;
        }

        protected Entity(string parTexturePath, int parPositionX, int parPositionY, int parSize, int parRotation = 0)
        {
            aSize = parSize;

            aPosition = new Vector2(parPositionX, parPositionY);

            aTexture = null;

            aTexturePath = parTexturePath;

            aColor = Color.White;

            aRotation = parRotation;

        }

        protected Entity(Texture2D parTexture, int parPositionX, int parPositionY, int parSize, int parRotation = 0)
        {
            aSize = parSize;

            aPosition = new Vector2(parPositionX, parPositionY);

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

        private Rectangle AddOffSetToRectangle(Rectangle parRectangle, float parXOffset, float parYOffset)
        {
            float tmpLeft = parRectangle.Left + parXOffset;
            float tmpTop = parRectangle.Top + parYOffset;

            return new Rectangle((int)tmpLeft, (int)tmpTop, parRectangle.Width, parRectangle.Height);
        }

        public virtual void LoadContent(ContentManager parContent)
        {
            if (aTexturePath is null && aTexture is null)
            {
                this.aTexture = null;
                return;
            }

            if (aTexturePath != string.Empty && aTexture is null)
            {
                this.aTexture = parContent.Load<Texture2D>(aTexturePath);
            }
        }

        public virtual void Update(GameTime parGameTime)
        {
        }

        public virtual void Draw(SpriteBatch parSpriteBatch, float parXOffset = 0, float parYOffset = 0, int parColor = -1)
        {
            Color tmpColor = Color.White;

            switch (parColor)
            {
                case 0:
                    tmpColor = Color.Red;
                    break;
                case 1:
                    tmpColor = Color.Cyan;
                    break;
                case 2:
                    tmpColor = Color.Pink;
                    break;
                case 3:
                    tmpColor = Color.Green;
                    break;
                default:
                    break;
            }

            Vector2 originOffset = new Vector2(GetTexture().Width / 2, GetTexture().Height / 2);
            parSpriteBatch.Draw(GetTexture(), AddOffSetToRectangle(GetRectangleHitBox(), parXOffset, parYOffset), new Rectangle(0, 0, aTexture.Height, aTexture.Height), tmpColor, MathHelper.ToRadians(GetRotation()), originOffset, GetSpriteEffects(), 0);
        }


        public virtual float GetRotation()
        {
            return aRotation;
        }

        public virtual SpriteEffects GetSpriteEffects()
        {
            return SpriteEffects.None;
        }

        public virtual Texture2D GetTexture()
        {
            return aTexture;
        }

    }
}
