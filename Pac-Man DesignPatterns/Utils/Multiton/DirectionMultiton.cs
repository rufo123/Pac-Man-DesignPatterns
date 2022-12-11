using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Pac_Man_DesignPatterns.Utils.Multiton
{
    public class DirectionMultiton
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once ArrangeObjectCreationWhenTypeEvident
        private static Dictionary<Direction, DirectionMultiton> aOriginals = new Dictionary<Direction, DirectionMultiton>();

        private readonly Direction aDirection;

        protected DirectionMultiton(Direction parDirection)
        {
            aDirection = parDirection;
        }

        public static DirectionMultiton GetInstance(Direction parDirection)
        {
            if (!aOriginals.TryGetValue(parDirection, out DirectionMultiton tmpInstance))
            {
                tmpInstance = new DirectionMultiton(parDirection);

                aOriginals.Add(parDirection, tmpInstance);
            }

            return tmpInstance;
        }

        public Direction GetDirection()
        {
            return aDirection;
        }

        public Vector2 GetPositionAhead(Vector2 parPosition, int parEntitySize, int parHowManyBlocks)
        {

            int tmpPositionX = (int)(parPosition.X - (parPosition.X % parEntitySize));
            int tmpPositionY = (int)(parPosition.Y - (parPosition.Y % parEntitySize));

            switch (GetDirection())
            {
                case Direction.Up:
                    tmpPositionY -= (parEntitySize * 4);
                    tmpPositionX -= (parEntitySize * 4);
                    break;
                case Direction.Down:
                    tmpPositionY += (parEntitySize * 4);
                    break;
                case Direction.Right:
                    tmpPositionX += (parEntitySize * 4);
                    break;
                case Direction.Left:
                    tmpPositionX -= (parEntitySize * 4);
                    break;
            }

            return new Vector2(tmpPositionX, tmpPositionY);
        }

    }
}
