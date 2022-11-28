using Pac_Man_DesignPatterns.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_DesignPatterns.Utils
{
    public class Message
    {
        private int aId;

        public int Id
        {
            get => aId;
        }
        
        private ICommand aCommand;

        public ICommand ACommand {
            get => aCommand;
        }

        public Message(int parId, ICommand parCommand = null)
        {
            this.aId = parId;
            this.aCommand = parCommand;
        }


    }
}
