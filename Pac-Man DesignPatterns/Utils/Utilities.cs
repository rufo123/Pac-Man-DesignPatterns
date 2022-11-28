using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Entities;

namespace Pac_Man_DesignPatterns.Utils
{
    public class Utilities
    {

        private Vector2[] aArrayEmptySpaces;

        private Vector2[] aArrayTiles;

        private Vector2[] aArrayMovableEntities;

        private Random aRandomTilesGenerator;

        private Random aRandomAdjancetGenerator;

        private Random aRandomSeedGenerator;

        public Utilities(Vector2[] parArrayEmptySpaces, Vector2[] parArrayTiles, Vector2[] parArrayMovableEntities)
        {
            aArrayEmptySpaces = parArrayEmptySpaces;
            aArrayTiles = parArrayTiles;
            aArrayMovableEntities = parArrayMovableEntities;

            aRandomSeedGenerator = new Random();
            aRandomTilesGenerator = new Random(aRandomSeedGenerator.Next());
            aRandomAdjancetGenerator = new Random(aRandomSeedGenerator.Next());
        }


        /// <summary>
        /// Concatenates two or more arrays into a single one.
        /// Source: https://www.ryadel.com/en/asp-net-merge-concatenate-array-two-arrays-c-sharp-linq/
        /// </summary>
        public static T[] Concat<T>(params T[][] arrays)
        {
            // return (from array in arrays from arr in array select arr).ToArray();

            var result = new T[arrays.Sum(a => a.Length)];
            int offset = 0;
            for (int x = 0; x < arrays.Length; x++)
            {
                arrays[x].CopyTo(result, offset);
                offset += arrays[x].Length;
            }
            return result;
        }

        public Vector2 GetRandomTile()
        {
            int tmpRandomIndex = aArrayEmptySpaces is not null ? aRandomTilesGenerator.Next(0, aArrayEmptySpaces.Length) : 0;

            return aArrayEmptySpaces is not null ? aArrayEmptySpaces[tmpRandomIndex] : Vector2.Zero;
        }

        public Vector2 GetRandomAdjacentTile(Vector2 parVector2)
        {
            Direction tmpDirection = (Direction)aRandomAdjancetGenerator.Next(1, 5);
            switch (tmpDirection)
            {
                case Direction.UP:
                    return new Vector2(parVector2.X, parVector2.Y - 1);
                case Direction.DOWN:
                    return new Vector2(parVector2.X, parVector2.Y + 1);
                case Direction.LEFT:
                    return new Vector2(parVector2.X - 1, parVector2.Y);
                case Direction.RIGHT:
                    return new Vector2(parVector2.X + 1, parVector2.Y);
                default:
                    return parVector2;
            }
        }


    }
}
