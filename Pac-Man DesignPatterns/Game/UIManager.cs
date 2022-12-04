using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pac_Man_DesignPatterns.Entities.TileEntity;
using Pac_Man_DesignPatterns.Utils;

namespace Pac_Man_DesignPatterns.Game
{
    // ReSharper disable once InconsistentNaming
    public class UIManager : IObserver
    {



        private int aScore;
        private readonly int aLevel;
        private readonly int aLives;

        private readonly Vector2 aSizeVector;
        private readonly Texture2D aBackgroundTexture;

        private SpriteFont aFontSmaller;
        private SpriteFont aFontBigger;

        private readonly float aGridSize;


        public UIManager(Vector2 parVectorSize, int parSize, GraphicsDevice parGraphicsDevice)
        {
            aLevel = 1;
            aScore = 0;
            aLives = 3;

            aSizeVector = parVectorSize;
            aBackgroundTexture = new Texture2D(parGraphicsDevice, 1, 1);

            aGridSize = parVectorSize.X / 4;
        }

        public int GetYOffset()
        {
            return 0;
        }

        public Utils.IObserver Implementation
        {
            get => default;
            set
            {
            }
        }

        public void UpdateEntityCollision()
        {
            throw new NotImplementedException();
        }

        public void LoadContent(ContentManager parContentManager)
        {
            aFontSmaller = parContentManager.Load<SpriteFont>("assets\\fonts\\font");
            aFontBigger = parContentManager.Load<SpriteFont>("assets\\fonts\\font_bigger");
        }

        public void Draw(SpriteBatch parSpriteBatch)
        {

            aBackgroundTexture.SetData(new[] { Color.Purple });

            parSpriteBatch.Draw(aBackgroundTexture, new Rectangle( 0, 0, (int)aSizeVector.X , (int)aSizeVector.Y - 20), Color.Purple);


            // Header
            float tmpYOffset = (aSizeVector.Y * 0.05f);
            parSpriteBatch.DrawString(aFontSmaller, "Level", new Vector2( 0 + GetCenteredStringOffset(aFontSmaller, aGridSize, "Level"), tmpYOffset), Color.White);
            float tmpOffset = 0;
            parSpriteBatch.DrawString(aFontSmaller, "High Score", new Vector2(tmpOffset + aGridSize + GetCenteredStringOffset(aFontSmaller, aGridSize * 2, "High Score"), tmpYOffset), Color.White);
            tmpOffset = aGridSize;
            parSpriteBatch.DrawString(aFontSmaller, "Lives", new Vector2(tmpOffset + aGridSize * 2 + GetCenteredStringOffset(aFontSmaller, aGridSize, "Lives"), tmpYOffset), Color.White);

            // Values


            tmpYOffset = (aSizeVector.Y * 0.50f);
            parSpriteBatch.DrawString(aFontBigger, aLevel.ToString(), new Vector2(0 + GetCenteredStringOffset(aFontBigger, aGridSize, aLevel.ToString()), tmpYOffset + 1), Color.White);
            parSpriteBatch.DrawString(aFontBigger, aLevel.ToString(), new Vector2(0 + GetCenteredStringOffset(aFontBigger, aGridSize, aLevel.ToString()), tmpYOffset), Color.Purple);
            tmpOffset = 0;
            parSpriteBatch.DrawString(aFontBigger, aScore.ToString(), new Vector2(tmpOffset + aGridSize + GetCenteredStringOffset(aFontBigger, aGridSize * 2, aScore.ToString()), tmpYOffset + 1), Color.White);
            parSpriteBatch.DrawString(aFontBigger, aScore.ToString(), new Vector2(tmpOffset + aGridSize + GetCenteredStringOffset(aFontBigger, aGridSize * 2, aScore.ToString()), tmpYOffset), Color.Purple);
            tmpOffset = aGridSize;
            parSpriteBatch.DrawString(aFontBigger, aLives.ToString(), new Vector2(tmpOffset + aGridSize * 2 + GetCenteredStringOffset(aFontBigger, aGridSize, aLives.ToString()), tmpYOffset + 1), Color.White);
            parSpriteBatch.DrawString(aFontBigger, aLives.ToString(), new Vector2(tmpOffset + aGridSize * 2 + GetCenteredStringOffset(aFontBigger, aGridSize, aLives.ToString()), tmpYOffset), Color.Purple);
            
        }

        private float GetCenteredStringOffset(SpriteFont parSpriteFont, float tmpSizeOfCell, string parText)
        {
            float tmpStringWidth = parSpriteFont.MeasureString(parText).X;
            return (tmpSizeOfCell - tmpStringWidth) / 2;
        }

        public void Update(Message parMessage)
        {
            switch (parMessage.MessageCode)
            {
                case 0:

                    if (parMessage.ACommand is PowerCookie)
                    {
                        aScore += 10;
                        break;
                    }

                    aScore++;
                    break;

                default:
                    break;
            }
        }
    }
}
