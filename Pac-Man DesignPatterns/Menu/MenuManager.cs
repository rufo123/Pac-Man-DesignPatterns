using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_DesignPatterns.Menu
{
    public class MenuManager
    {

        private bool aVisible;
        private AMenuItem[] aArrayMenuItems;

        public MenuManager()
        {
            aVisible = false;
        }

        public void CreateMenu(AMenuItem[] parArrayMenuItems)
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
