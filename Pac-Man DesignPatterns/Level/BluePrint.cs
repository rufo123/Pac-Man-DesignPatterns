using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Command;
using Pac_Man_DesignPatterns.Utils;

namespace Pac_Man_DesignPatterns.Level
{
    public class BluePrint : ICommandNeighTiles
    {
        private readonly Vector2 aPosition;
        private readonly Direction[] aSameTypeNeighbours;

        private readonly Dictionary<Direction, Vector2> aConnectedToTileId;
  

        public BluePrint(Vector2 parPosition, Direction[] parSameTypeNeighbours = null)
        {
            aPosition = parPosition;
            aSameTypeNeighbours = parSameTypeNeighbours;
            aConnectedToTileId = new Dictionary<Direction, Vector2>();
        }

        public Vector2 Position => aPosition;

        public Direction[] GetSameTypeNeighbours => aSameTypeNeighbours;

        public void AddConnectedToTileId(Direction parDirection, Vector2 parTilePos) {

            aConnectedToTileId.Add(parDirection, parTilePos);
        }

        public Vector2 GetConnectedToTileId(Direction parDirection) {
            // ReSharper disable once RedundantAssignment
            // ReSharper disable once InlineOutVariableDeclaration
            Vector2 tmpReturnValue = new Vector2(-1, -1);
            aConnectedToTileId.TryGetValue(parDirection, out tmpReturnValue);
            return tmpReturnValue;
        }


        public void OneNeighbourLogic(Direction[] parNeighBours, int parScale, out bool parOutIsCorner, out float parOutRotation)
        {
            int tmpRotation = 0;

            switch (parNeighBours[0])
            {
                case Direction.Up:
                    tmpRotation = 0;
                    AddConnectedToTileId(Direction.Up, Utilities.GenerateVectorPosFromDirection(Direction.Up, parScale, Position));
                    break;
                case Direction.Down:
                    tmpRotation = 180;
                    AddConnectedToTileId(Direction.Down, Utilities.GenerateVectorPosFromDirection(Direction.Down, parScale, Position));
                    break;
                case Direction.Left:
                    tmpRotation = 270;
                    AddConnectedToTileId(Direction.Left, Utilities.GenerateVectorPosFromDirection(Direction.Left, parScale, Position));
                    break;
                case Direction.Right:
                    tmpRotation = 90;
                    AddConnectedToTileId(Direction.Right, Utilities.GenerateVectorPosFromDirection(Direction.Right, parScale, Position));
                    break;
            }

            parOutIsCorner = false;
            parOutRotation = tmpRotation;
        }

        public void TwoNeighbourLogic(Direction[] parNeighBours, int parScale, out bool parOutIsCorner, out float parOutRotation)
        {
            Direction tmpDirectionFirstNeighbour = parNeighBours[0];
            Direction tmpDirectionSecondNeighbour = parNeighBours[1];

            int tmpRotation = 0;

            bool tmpIsCorner = false;


            if (tmpDirectionFirstNeighbour == Direction.Up || tmpDirectionSecondNeighbour == Direction.Up)
            {
                if (tmpDirectionFirstNeighbour == Direction.Left || tmpDirectionSecondNeighbour == Direction.Left)
                {
                    tmpRotation = 180;
                    tmpIsCorner = true;
                    AddConnectedToTileId(Direction.Up, Utilities.GenerateVectorPosFromDirection(Direction.Up, parScale, Position));
                    AddConnectedToTileId(Direction.Left, Utilities.GenerateVectorPosFromDirection(Direction.Left, parScale, Position));


                }
                else if (tmpDirectionFirstNeighbour == Direction.Right || tmpDirectionSecondNeighbour == Direction.Right)
                {
                    tmpRotation = 270;
                    tmpIsCorner = true;
                    AddConnectedToTileId(Direction.Up, Utilities.GenerateVectorPosFromDirection(Direction.Up, parScale, Position));
                    AddConnectedToTileId(Direction.Right, Utilities.GenerateVectorPosFromDirection(Direction.Right, parScale, Position));
                }
                else if (tmpDirectionFirstNeighbour == Direction.Down || tmpDirectionSecondNeighbour == Direction.Down)
                {
                    tmpRotation = 0;
                    AddConnectedToTileId(Direction.Up, Utilities.GenerateVectorPosFromDirection(Direction.Up, parScale, Position));
                    AddConnectedToTileId(Direction.Down, Utilities.GenerateVectorPosFromDirection(Direction.Down, parScale, Position));
                }


            }
            else if (tmpDirectionFirstNeighbour == Direction.Down || tmpDirectionSecondNeighbour == Direction.Down)
            {

                if (tmpDirectionFirstNeighbour == Direction.Left || tmpDirectionSecondNeighbour == Direction.Left)
                {
                    tmpRotation = 90;
                    tmpIsCorner = true;
                    AddConnectedToTileId(Direction.Down, Utilities.GenerateVectorPosFromDirection(Direction.Down, parScale, Position));
                    AddConnectedToTileId(Direction.Left, Utilities.GenerateVectorPosFromDirection(Direction.Left, parScale, Position));
                }
                else if (tmpDirectionFirstNeighbour == Direction.Right || tmpDirectionSecondNeighbour == Direction.Right)
                {
                    tmpRotation = 0;
                    tmpIsCorner = true;
                    AddConnectedToTileId(Direction.Down, Utilities.GenerateVectorPosFromDirection(Direction.Down, parScale, Position));
                    AddConnectedToTileId(Direction.Right, Utilities.GenerateVectorPosFromDirection(Direction.Right, parScale, Position));
                }


            }
            else if (tmpDirectionFirstNeighbour == Direction.Left || tmpDirectionSecondNeighbour == Direction.Left)
            {
                if (tmpDirectionFirstNeighbour == Direction.Right || tmpDirectionSecondNeighbour == Direction.Right)
                {
                    tmpRotation = 90;
                    AddConnectedToTileId(Direction.Left, Utilities.GenerateVectorPosFromDirection(Direction.Left, parScale, Position));
                    AddConnectedToTileId(Direction.Right, Utilities.GenerateVectorPosFromDirection(Direction.Right, parScale, Position));
                }
            }
            parOutIsCorner = tmpIsCorner;
            parOutRotation = tmpRotation;
            
        }

        public void ThreeNeighbourLogic(Direction[] parNeighBours, int parScale, out bool parOutIsCorner, out float parOutRotation)
        {
            int tmpVerticalMatches = 0;
            int tmpHorizontalMatches = 0;

            for (int i = 0; i < parNeighBours.Length; i++)
            {

                if (parNeighBours[i] == Direction.Left || parNeighBours[i] == Direction.Right)
                {
                    tmpHorizontalMatches++;
                }

                if (parNeighBours[i] == Direction.Up || parNeighBours[i] == Direction.Down)
                {
                    tmpVerticalMatches++;
                }
            }

            if (tmpHorizontalMatches > 1)
            {

                
                AddConnectedToTileId(Direction.Left, Utilities.GenerateVectorPosFromDirection(Direction.Left, parScale, Position));
                AddConnectedToTileId(Direction.Right, Utilities.GenerateVectorPosFromDirection(Direction.Right, parScale, Position));
            }

            else if (tmpVerticalMatches > 1)
            {
                AddConnectedToTileId(Direction.Up, Utilities.GenerateVectorPosFromDirection(Direction.Up, parScale, Position));
                AddConnectedToTileId(Direction.Down, Utilities.GenerateVectorPosFromDirection(Direction.Down, parScale, Position));
                
            }

            parOutIsCorner = false;
            parOutRotation = tmpHorizontalMatches > 1 ? 90 : 0;
        }

        public void FourNeighbourLogic(Direction[] parNeighBours, int parScale, out bool parOutIsCorner, out float parOutRotation, Dictionary<Vector2, BluePrint> parDictWallsConnToTileIdPos)
        {

            Vector2 tmpUpPos = Utilities.GenerateVectorPosFromDirection(Direction.Up, parScale, Position);
            Vector2 tmpRightPos = Utilities.GenerateVectorPosFromDirection(Direction.Right, parScale, Position);
            Vector2 tmpDownPos = Utilities.GenerateVectorPosFromDirection(Direction.Down, parScale, Position);
            Vector2 tmpLeftPos = Utilities.GenerateVectorPosFromDirection(Direction.Left, parScale, Position);

            parDictWallsConnToTileIdPos.TryGetValue(tmpUpPos, out var tmpUpTile);
            parDictWallsConnToTileIdPos.TryGetValue(tmpRightPos, out var tmpRightTile);
            parDictWallsConnToTileIdPos.TryGetValue(tmpDownPos, out var tmpDownTile);
            parDictWallsConnToTileIdPos.TryGetValue(tmpLeftPos, out var tmpLeftTile);

            int tmpRotation = -1;

            // RIGHT - DOWN CORNER - 0

            if (tmpRightTile != null && tmpDownTile != null && tmpRightTile.GetConnectedToTileId(Direction.Left) == Position && tmpDownTile.GetConnectedToTileId(Direction.Up) == Position)
            {
                tmpRotation = 0;
            }

            // LEFT - DOWN CORNER - 90

            else if (tmpLeftTile != null && tmpDownTile != null && tmpLeftTile.GetConnectedToTileId(Direction.Right) == Position && tmpDownTile.GetConnectedToTileId(Direction.Up) == Position)
            {
                tmpRotation = 90;
            }

            // LEFT - UP CORNER - 180


            else if (tmpLeftTile != null && tmpUpTile != null && tmpLeftTile.GetConnectedToTileId(Direction.Right) == Position && tmpUpTile.GetConnectedToTileId(Direction.Down) == Position)
            {
                tmpRotation = 180;
            }

            // RIGHT - UP CORNER - 270

            else if (tmpRightTile != null && tmpUpTile != null && tmpRightTile.GetConnectedToTileId(Direction.Left) == Position && tmpUpTile.GetConnectedToTileId(Direction.Down) == Position)
            {
                tmpRotation = 270;
            }

            parOutIsCorner = true;
            parOutRotation = tmpRotation;
           
        }
    }
}
