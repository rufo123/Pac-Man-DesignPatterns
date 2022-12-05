using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_DesignPatterns.Menu
{
    public class MenuManager
    {

#pragma warning disable CS0414
        private bool aVisible;
#pragma warning restore CS0414
        private MenuItem[] aArrayMenuItems;

        public MenuManager()
        {
            aVisible = false;
        }

        public void CreateMenu(MenuItem[] parArrayMenuItems)
        {
            aArrayMenuItems = parArrayMenuItems;
        }

        public void Show()
        {
        }

        public void Hide()
        {

        }
    }
}
