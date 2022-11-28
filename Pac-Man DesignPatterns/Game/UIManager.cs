using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pac_Man_DesignPatterns.Entities.TileEntity;
using Pac_Man_DesignPatterns.Utils;

namespace Pac_Man_DesignPatterns.Game
{
    // ReSharper disable once InconsistentNaming
    public class UIManager : IObserver
    {
        private int aScore;
        private int aLevelTime;
        private int aTotalTime;
        private int aLives;


        public Utils.IObserver Implementation
        {
            get => default;
            set
            {
            }
        }

        public void UpdateEntityCollision()
        {
            throw new NotImplementedException();
        }

        public void Update(Message parMessage)
        {
            switch (parMessage.Id)
            {
                case -1:
                    aScore++;
                    break;

                default:
                    break;
            }
        }
    }
}
