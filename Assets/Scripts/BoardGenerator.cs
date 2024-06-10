using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BoardGenerator : MonoBehaviour
{
    public int gridHeight;
    public int gridWidth;
    public float hexSize = 1f;
    public float pathOffset = .85f;
    
    public GameObject tiles;
    public GameObject nodes;
    public GameObject paths;
    
    public Tile tilePrefab;
    public Node nodePrefab;
    public Path pathPrefab;

    private List<(float, float)> nodePositions = new();
    private List<(float, float)> pathPositions = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        
        gridHeight = gridHeight % 2 != 0 ? gridHeight : gridHeight - 1; // make sure gridHeight is odd
        gridHeight =
            gridHeight > gridWidth * 2
                ? gridWidth * 2 - 1
                : gridHeight; // make sure gridHeight is less than gridLength * 2

        var initialLength = gridWidth - Mathf.FloorToInt(gridHeight / 2f);
        var tileId = 0;

        for (var j = 0; j < gridHeight; j++)
        {
            var actualLength = GridUtils.ActualLength(initialLength, j, gridHeight, gridWidth);
            for (var k = 0; k < actualLength; k++)
            {
                var hexPosition = CalculateHexPosition(GridUtils.ActualPosition(k, j, gridHeight), -j);
                var tile = Instantiate(tilePrefab, hexPosition, Quaternion.identity, tiles.transform);
                tile.SetText($"{tileId}");
                
                GenerateNodes(hexPosition);
                GeneratePaths(hexPosition);
                
                tileId++;
            }
        }
    }

    Vector3 CalculateHexPosition(int hexX, int hexY)
    {
        var hex = new Vector2(hexX, hexY);

        var x = hexSize * 3 / 2 * hex.x;
        var z = hexSize * Mathf.Sqrt(3) * (hex.y + hex.x / 2);
        return new Vector3(x, 0, z);
    }

    private void GenerateNodes(Vector3 hexPosition)
    {
        const float angleDeg = 60f;
        for (var i = 0; i < 6; i++)
        {
            var angleRad = Mathf.Deg2Rad * (angleDeg * i);
            var position = new Vector3(
                hexPosition.x + hexSize * Mathf.Cos(angleRad),
                0,
                hexPosition.z + hexSize * Mathf.Sin(angleRad)
            );
            
            var roundedPosition = (Mathf.Round(position.x * 100) / 100, Mathf.Round(position.z * 100) / 100);

            if (!nodePositions.Contains(roundedPosition))
            {
                var node =Instantiate(nodePrefab, position, Quaternion.identity, nodes.transform);
                node.SetText($"{roundedPosition}");
                nodePositions.Add(roundedPosition);
            }
        }
    }

    private void GeneratePaths(Vector3 hexPosition)
    {
        const float angleDeg = 60f;
        for (var i = 0; i < 6; i++)
        {
            var angleRad = Mathf.Deg2Rad * (angleDeg * i + 30);
            var position = new Vector3(
                hexPosition.x + hexSize * Mathf.Cos(angleRad) * pathOffset,
                    0,
                hexPosition.z + hexSize * Mathf.Sin(angleRad) * pathOffset
            );
            
            var roundedPosition = (Mathf.Round(position.x * 100) / 100, Mathf.Round(position.z * 100) / 100);
            
            if (!pathPositions.Contains(roundedPosition))
            {
                var path = Instantiate(pathPrefab, position, Quaternion.identity, paths.transform);
                pathPositions.Add(roundedPosition);
                
                path.transform.LookAt(hexPosition);
                path.transform.position += new Vector3(0, .1f, 0);
            }
        }
    }
}