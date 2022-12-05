using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_DesignPatterns.Menu
{
    public abstract class MenuItem
    {
#pragma warning disable CS0169
        private string aText;
#pragma warning restore CS0169

        public MenuManager MenuManager
        {
            get => default;
            set
            {
            }
        }

        protected MenuItem(string parText)
        {
            aText = parText;
        }
    }
}
