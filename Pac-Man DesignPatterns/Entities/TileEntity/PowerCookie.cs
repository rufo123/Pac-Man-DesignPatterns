using Microsoft.Xna.Framework.Graphics;
using Pac_Man_DesignPatterns.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
    }
}
