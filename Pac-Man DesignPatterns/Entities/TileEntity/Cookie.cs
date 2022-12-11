using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Game;

namespace Pac_Man_DesignPatterns.Entities.TileEntity
{
    public class Cookie : Food
    {
        public Cookie(string parTexturePath, int parPositionX, int parPositionY, int parSize, Color parColor) : base(parTexturePath, parPositionX, parPositionY, parSize, parColor)
        {
        }

        public override void Execute()
        {
            aIsHidden = true;
            GameManager.GetInstance().AddScore(1);
            base.Execute();
        }
    }
}
