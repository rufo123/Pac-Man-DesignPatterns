using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pac_Man_DesignPatterns.Command;
using Pac_Man_DesignPatterns.Game;

namespace Pac_Man_DesignPatterns.Menu
{
    public class QuitButton : MenuItem
    {
        public QuitButton(string parText) : base(parText)
        {
        }

        public override void Execute()
        {
            GameManager.GetInstance().Game.Exit();
        }
    }
}
