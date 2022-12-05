using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pac_Man_DesignPatterns.Game;

namespace Pac_Man_DesignPatterns.Menu
{
    public class Menu
    {
        private Color aColor;
        private int aWidth;
        private int aHeight;
        private MenuItem[] aArrayMenuItems;
        private Texture2D aBackgroundTexture;
        private SpriteFont aFont;

        public Menu(Color parBackgroundColor, int parWidth, int parHeight, MenuItem[] parMenuItems)
        {
            aColor = parBackgroundColor;
            aWidth = parWidth;
            aHeight = parHeight;
            aArrayMenuItems = parMenuItems;
            aBackgroundTexture = new Texture2D(GameManager.GetInstance().Game.GraphicsDevice, 1, 1);
        }

        public void Draw(SpriteBatch parSpriteBatch)
        {
            parSpriteBatch.Draw(aBackgroundTexture, new Rectangle(0, 0, aWidth, aHeight), Color.Purple);
        }

        public void Load(ContentManager parContentManager)
        {
            aFont = parContentManager.Load<SpriteFont>("assets\\fonts\\font_bigger");
            aBackgroundTexture.SetData(new[] { Color.Purple });
        }

        public void Initialise()
        {

        }
    }
}
