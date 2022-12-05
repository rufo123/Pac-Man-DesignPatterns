using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pac_Man_DesignPatterns.Game;
using Pac_Man_DesignPatterns.Utils;

namespace Pac_Man_DesignPatterns.Menu
{
    public class Menu
    {
        private Color aColor;
        private int aWidth;
        private int aHeight;
        private MenuItem[] aArrayMenuItems;
        private Texture2D aBackgroundTexture;
        private Texture2D aButtonTexture;
        private SpriteFont aFont;

        private KeyboardState aKeyBoardNew;
        private KeyboardState aKeyBoardOld;

        private int aSelectedIndex;

        public Menu(GraphicsDevice parGraphicsDevice, Color parBackgroundColor, int parWidth, int parHeight, MenuItem[] parMenuItems)
        {
            aColor = parBackgroundColor;
            aWidth = parWidth;
            aHeight = parHeight;
            aArrayMenuItems = parMenuItems;
            aBackgroundTexture = new Texture2D(GameManager.GetInstance().Game.GraphicsDevice, 1, 1);
            aButtonTexture = new Texture2D(GameManager.GetInstance().Game.GraphicsDevice, 1, 1);
            var test = new List<MenuItem>(parMenuItems);
            aArrayMenuItems = test.ToArray();

            aSelectedIndex = 0;
        }

        public void Draw(SpriteBatch parSpriteBatch)
        {

            parSpriteBatch.Draw(aBackgroundTexture, new Rectangle(0, 0, aWidth, aHeight), aColor);

            GameManager.GetInstance().Game.GraphicsDevice.SamplerStates[0] = SamplerState.AnisotropicWrap;

            int tmpWidth = aWidth / aArrayMenuItems.Length;
            int tmpHeight = aHeight / aArrayMenuItems.Length;


            var tmpScale = 141.666672f;

            var tmpFontScale = tmpHeight / tmpScale;

            int tmpSpacesYOffset = 0;

            int tmpOffsetConstant = tmpWidth / 20;

            int tmpOffsetYStartingPoint = -tmpOffsetConstant * (aArrayMenuItems.Length - 1);

            for (int i = 0; i < aArrayMenuItems.Length; i++)
            {
                tmpSpacesYOffset -= tmpOffsetConstant;

                int tmpLeftPos = (aWidth - tmpWidth) / 2;
                int tmpTopPos = tmpOffsetYStartingPoint + (i * (tmpHeight / aArrayMenuItems.Length)) + ((aHeight - tmpHeight) / 2) - tmpSpacesYOffset;

                Vector2 tmpFontSizeVector = (aFont.MeasureString(aArrayMenuItems[i].Text) * tmpFontScale) / 2;

                parSpriteBatch.Draw(aButtonTexture, new Rectangle(tmpLeftPos, tmpTopPos, tmpWidth, tmpHeight / aArrayMenuItems.Length), Color.Cyan * (aSelectedIndex == i ? 0.5f : 1));
                parSpriteBatch.DrawString(aFont, aArrayMenuItems[i].Text, new Vector2((tmpLeftPos + tmpWidth / 2) - tmpFontSizeVector.X, (tmpTopPos + (tmpHeight / aArrayMenuItems.Length) / 2) - tmpFontSizeVector.Y), Color.Black, 0, Vector2.Zero, tmpFontScale, SpriteEffects.None, 0);
            }

            GameManager.GetInstance().Game.GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp;

        }

        public void Load(ContentManager parContentManager)
        {
            aFont = parContentManager.Load<SpriteFont>("assets\\fonts\\font_bigger");
            aBackgroundTexture.SetData(new[] { aColor });
            aButtonTexture.SetData(new[] { Color.Cyan });
        }

        public void Initialise()
        {

        }

        public void Update()
        {
            aKeyBoardNew = Keyboard.GetState();

            if (IsKeyDown(Keys.W) || IsKeyDown(Keys.Up))
            {
                if (aSelectedIndex > 0)
                {
                    aSelectedIndex--;
                }

            }
            else if (IsKeyDown(Keys.S) || IsKeyDown(Keys.Down))
            {
                if (aSelectedIndex < aArrayMenuItems.Length - 1)
                {
                    aSelectedIndex++;
                }
            }

            if (IsKeyDown(Keys.Enter))
            {
                aArrayMenuItems[aSelectedIndex].Execute();
            }

            aKeyBoardOld = aKeyBoardNew;
        }


        public bool IsKeyDown(Keys parKey)
        {
            return (aKeyBoardNew.IsKeyDown(parKey) && aKeyBoardOld.IsKeyUp(parKey));
        }
    }

}
