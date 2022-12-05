using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pac_Man_DesignPatterns.Command;

namespace Pac_Man_DesignPatterns.Menu
{
    public abstract class MenuItem : ICommand
    {
        private string aText;

        public string Text => aText;

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

        public virtual void Execute()
        {
           
        }
    }
}
