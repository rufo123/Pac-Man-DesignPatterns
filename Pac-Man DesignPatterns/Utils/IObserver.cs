using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pac_Man_DesignPatterns.Entities.TileEntity;

namespace Pac_Man_DesignPatterns.Utils
{
    public interface IObserver
    {
        public void Update(Message parMessage);

    }
}
