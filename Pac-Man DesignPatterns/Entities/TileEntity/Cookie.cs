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
        public Cookie(Texture2D parTexture2D, int parPositionX, int parPositionY, int parSize) : base(parTexture2D, parPositionX, parPositionY, parSize)
        {
        }

        public override void Execute()
        {
            base.Execute();
            // Dorobit, zatial vyhodi Exec z basu
        }
    }
}
