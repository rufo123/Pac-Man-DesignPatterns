

using Pac_Man_DesignPatterns.Game;

namespace Pac_Man_DesignPatterns;

class Program
{
    public static void Main(string[] parArgs)
    {
        GameManager tmpGameManager = GameManager.GetInstance();

        tmpGameManager.RunGame();
    }
}