using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_DesignPatterns.Menu
{
    public abstract class AMenuItem
    {
        private string aText;

        public MenuManager MenuManager
        {
            get => default;
            set
            {
            }
        }

        public abstract void Press();
    }
}
