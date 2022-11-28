using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_DesignPatterns.Utils
{
    public static class Calculator
    {
        public static int GetEuclideanDistance(Vector2 parFirstPosition, Vector2 parSecondPosition)
        {
            return (int)Math.Sqrt(Math.Pow(parFirstPosition.X - parSecondPosition.X, 2) + Math.Pow(parFirstPosition.Y - parSecondPosition.Y, 2));
        }
    }
}
