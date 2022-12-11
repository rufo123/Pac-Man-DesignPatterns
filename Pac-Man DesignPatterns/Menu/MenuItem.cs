using Pac_Man_DesignPatterns.Command;

namespace Pac_Man_DesignPatterns.Menu
{
    public abstract class MenuItem : ICommand
    {
        private readonly string aText;

        public string Text => aText;

        protected MenuItem(string parText)
        {
            aText = parText;
        }

        public virtual void Execute()
        {
           
        }
    }
}
