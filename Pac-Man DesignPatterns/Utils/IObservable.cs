using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_DesignPatterns.Utils
{
    public interface IObservable
    {
        public void RegisterObserver(IObserver parObserver);

        public void RemoveObserver(IObserver parObserver);

        public void Notify();
    }
}
