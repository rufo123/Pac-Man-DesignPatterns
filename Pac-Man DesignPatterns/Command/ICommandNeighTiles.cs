using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Level;
using Pac_Man_DesignPatterns.Utils;

// ReSharper disable UnusedMemberInSuper.Global

namespace Pac_Man_DesignPatterns.Command
{
    public interface ICommandNeighTiles
    {
        public void OneNeighbourLogic(Direction[] parNeighBours, int parScale, out bool parOutIsCorner, out float parOutRotation);
        public void TwoNeighbourLogic(Direction[] parNeighBours, int parScale, out bool parOutIsCorner, out float parOutRotation);
        public void ThreeNeighbourLogic(Direction[] parNeighBours, int parScale, out bool parOutIsCorner, out float parOutRotation);
        public void FourNeighbourLogic(Direction[] parNeighBours, int parScale, out bool parOutIsCorner, out float parOutRotation, Dictionary<Vector2, BluePrint> parDictWallsConnToTileIdPos);
    }
}
