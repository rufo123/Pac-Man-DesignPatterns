using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pac_Man_DesignPatterns.Command;

namespace Pac_Man_DesignPatterns.Menu
{
    public class PlayButton : MenuItem
    {
        public PlayButton(string parText) : base(parText)
        {
        }

        public override void Execute()
        {
            base.Execute();
        }
    }
}
