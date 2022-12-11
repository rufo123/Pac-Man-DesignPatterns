using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Game;

namespace Pac_Man_DesignPatterns.Entities.TileEntity
{
    public class PowerCookie : Food
    {

        private readonly GameManager aGameManager;

        public PowerCookie(string parTexture2D, int parPositionX, int parPositionY, int parSize, Color parColor) : base(parTexture2D, parPositionX, parPositionY, parSize, parColor)
        {
            aGameManager = GameManager.GetInstance();
        }
        
        public override void Execute()
        {
            aIsHidden = true;
            
            aGameManager.SetGhostsFrightened();

            aGameManager.AddScore(10);
        }
    }
}
