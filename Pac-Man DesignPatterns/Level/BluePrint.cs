using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_DesignPatterns.Level
{
    public class BluePrint
    {
        private readonly Vector2 aPosition;
        private readonly int aVariationIndex;
        private readonly Direction[] aSameTypeNeighbours;

        private readonly Dictionary<Direction, Vector2> aConnectedToTileId;
  

        public BluePrint(Vector2 parPosition, int parVariationIndex, Direction[] parSameTypeNeighbours = null)
        {
            this.aPosition = parPosition;
            this.aVariationIndex = parVariationIndex;
            this.aSameTypeNeighbours = parSameTypeNeighbours;
            this.aConnectedToTileId = new Dictionary<Direction, Vector2>();
        }

        public Vector2 Position
        {
            get => aPosition;
        }
        public int VariationIndex
        {
            get => aVariationIndex;
        }

        public Direction[] GetSameTypeNeighbours
        {
            get => aSameTypeNeighbours;
        }

        public void AddConnectedToTileId(Direction parDirection, Vector2 parTilePos) {

            this.aConnectedToTileId.Add(parDirection, parTilePos);
        }

        public Vector2 GetConnectedToTileId(Direction parDirection) {
            Vector2 tmpReturnValue = new Vector2(-1, -1);
            this.aConnectedToTileId.TryGetValue(parDirection, out tmpReturnValue);
            return tmpReturnValue;
        }

    }
}
