using Microsoft.Xna.Framework;

namespace Pac_Man_DesignPatterns.Utils
{
    public static class Utilities
    {
        public static Vector2 GenerateVectorPosFromDirection(Direction parDirection, int parScale, Vector2 parPosition)
        {

            switch (parDirection)
            {
                case Direction.Nothing:
                    return parPosition;
                case Direction.Up:
                    return new Vector2(parPosition.X, parPosition.Y - parScale);
                case Direction.Down:
                    return new Vector2(parPosition.X, parPosition.Y + parScale);
                case Direction.Left:
                    return new Vector2(parPosition.X - parScale, parPosition.Y);
                case Direction.Right:
                    return new Vector2(parPosition.X + parScale, parPosition.Y);
                default:
                    return parPosition;
            }
        }

    }
}
