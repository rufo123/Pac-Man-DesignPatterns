

using Pac_Man_DesignPatterns.Game;

namespace Pac_Man_DesignPatterns;

class Program
{
    public static void Main(string[] args)
    {
        GameManager tmpGameManager = GameManager.GetInstance();

        tmpGameManager.Game.Run();
    }
}