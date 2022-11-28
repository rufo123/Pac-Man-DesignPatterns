using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_DesignPatterns.State
{
    public enum StateEnum
    {
        Chase = 0,
        Frightened = 1,
        Home = 2,
        Scatter = 3,
        Dead = 4,
    }
}
