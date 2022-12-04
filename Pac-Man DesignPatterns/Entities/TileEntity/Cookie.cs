using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_DesignPatterns.Entities.TileEntity
{
    public class Cookie : Food
    {
        public Cookie(string parTexturePath, int parPositionX, int parPositionY, int parSize) : base(parTexturePath, parPositionX, parPositionY, parSize)
        {
        }

        public override void Execute()
        {
            aIsHidden = true;
            base.Execute();
            // Dorobit, zatial vyhodi Exec z basu
        }
    }
}
