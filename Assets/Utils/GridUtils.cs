using System.Collections.Generic;
using Classes;
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
        
        public static Vector3 CalculateHexPosition(int hexX, int hexY, float hexSize)
        {
            var hex = new Vector2(hexX, hexY);

            var x = hexSize * 3 / 2 * hex.x;
            var z = hexSize * Mathf.Sqrt(3) * (hex.y + hex.x / 2);
            return new Vector3(x, 0, z);
        }
        
        public static List<Vector3> GetTilesPosition(int gridWidth, int gridHeight, float hexSize)
        {
            var tiles = new List<Vector3>();
            var initialLength = gridWidth - Mathf.FloorToInt(gridHeight / 2f);

            for (var j = 0; j < gridHeight; j++)
            {
                var actualLength = ActualLength(initialLength, j, gridHeight, gridWidth);
                for (var k = 0; k < actualLength; k++)
                {
                    tiles.Add(CalculateHexPosition(GridUtils.ActualPosition(k, j, gridHeight), -j, hexSize));
                }
            }

            return tiles;
        }
        
        public static List<Vector3> GetNodesPosition(Vector3 hexPosition, float hexSize)
        {
            var nodes = new List<Vector3>();
            const float angleDeg = 60f;
            for (var i = 0; i < 6; i++)
            {
                var angleRad = Mathf.Deg2Rad * (angleDeg * i);
                nodes.Add(new Vector3(
                    hexPosition.x + hexSize * Mathf.Cos(angleRad),
                    0,
                    hexPosition.z + hexSize * Mathf.Sin(angleRad)
                ));
            }

            return nodes;
        }
        
        public static List<Vector3> GetPathsPosition(Vector3 hexPosition, float hexSize, float pathOffset)
        {
            var paths = new List<Vector3>();
            const float angleDeg = 60f;
            for (var i = 0; i < 6; i++)
            {
                var angleRad = Mathf.Deg2Rad * (angleDeg * i + 30);
                paths.Add(new Vector3(
                    hexPosition.x + hexSize * Mathf.Cos(angleRad) * pathOffset,
                    0,
                    hexPosition.z + hexSize * Mathf.Sin(angleRad) * pathOffset
                ));
            }

            return paths;
        }
        
        public static Dictionary<TileType, int> GetTileTypeCount(int totalTiles, Dictionary<TileType, float> tileTypeDistribution)
        {
            var tileTypeCount = new Dictionary<TileType, int>();
            foreach (var tileType in tileTypeDistribution.Keys)
            {
                tileTypeCount[tileType] = Mathf.FloorToInt(totalTiles * tileTypeDistribution[tileType]);
            }

            return tileTypeCount;
        }
        
        public static Vector3 GetCenter(List<Vector3> positions)
        {
            var center = Vector3.zero;
            positions.ForEach(position => center += position);

            return center / positions.Count;
        }
    }
}