using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pac_Man_DesignPatterns.Menu
{
    public class MenuManager
    {

        private MenuItem[] aArrayMenuItems;
        private Menu aMenu;

        public void CreateMenu(GraphicsDevice parGraphicsDevice)
        {
            MenuItem tmpPlayButton = new PlayButton("Play");
            MenuItem tmpQuitButton = new QuitButton("Quit");
            aArrayMenuItems = new[] { tmpPlayButton, tmpQuitButton };
            aMenu = new Menu(Color.Purple, parGraphicsDevice.PresentationParameters.BackBufferWidth, parGraphicsDevice.PresentationParameters.BackBufferHeight, aArrayMenuItems);
        }

        public void Update()
        {
            aMenu.Update();
        }

        public void Draw(SpriteBatch parSpriteBatch)
        {
            aMenu.Draw(parSpriteBatch);
        }

        public void LoadContent(ContentManager parContentManager)
        {
            aMenu.LoadContent(parContentManager);
        }
    }
}
