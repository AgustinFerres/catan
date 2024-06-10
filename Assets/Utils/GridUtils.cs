using UnityEngine;

namespace Utils
{
    public static class GridUtils
    {
        public static int ActualLength(int initialLength, int j, int gridHeight, int gridWidth)
        {
            return j <= gridHeight / 2 ? initialLength + j : gridWidth - (j - gridHeight / 2);
        }
        
        public static int ActualPosition(int initialPosition, int j, int gridHeight)
        {
            return j <= gridHeight / 2 ? initialPosition : initialPosition + (j - gridHeight / 2);
        }
    }
}