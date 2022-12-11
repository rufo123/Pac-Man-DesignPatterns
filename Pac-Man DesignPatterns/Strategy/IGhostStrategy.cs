using Microsoft.Xna.Framework;

namespace Pac_Man_DesignPatterns.Strategy
{
    public interface IGhostStrategy
    {
        public Vector2 GetChaseTilePos();

        public Vector2 GetScatterTilePos();

    }
}
