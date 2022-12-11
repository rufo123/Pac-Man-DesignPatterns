using Pac_Man_DesignPatterns.Command;

namespace Pac_Man_DesignPatterns.Utils
{
    public class Message
    {
        private readonly MessageCodes aMessageCode;

        public MessageCodes MessageCode => aMessageCode;

        private readonly ICommand aCommand;

        public ICommand ACommand => aCommand;

        public Message(MessageCodes parCode, ICommand parCommand = null)
        {
            aMessageCode = parCode;
            aCommand = parCommand;
        }


    }
}
