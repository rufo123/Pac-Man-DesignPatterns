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
        private readonly MessageCodes aMessageCode;

        public MessageCodes MessageCode
        {
            get => aMessageCode;
        }
        
        private readonly ICommand aCommand;

        public ICommand ACommand {
            get => aCommand;
        }

        public Message(MessageCodes parCode, ICommand parCommand = null)
        {
            this.aMessageCode = parCode;
            this.aCommand = parCommand;
        }


    }
}
