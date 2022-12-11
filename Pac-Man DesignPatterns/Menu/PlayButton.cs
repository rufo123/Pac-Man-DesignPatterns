using Pac_Man_DesignPatterns.Game;

namespace Pac_Man_DesignPatterns.Menu
{
    public class PlayButton : MenuItem
    {
        public PlayButton(string parText) : base(parText)
        {
           
        }

        public override void Execute()
        {
            GameManager.GetInstance().StartGame();
        }
    }
}
