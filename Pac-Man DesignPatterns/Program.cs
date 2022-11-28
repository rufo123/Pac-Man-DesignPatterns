

using Pac_Man_DesignPatterns.Game;

class Program
{
    public static void Main(string[] args)
    {
        GameManager tmpGameManager = GameManager.GetInstance();

        tmpGameManager.Game.Run();
    }
}
