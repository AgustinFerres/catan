using System;
using System.Collections.Generic;
using Classes;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

public class BoardGenerator : MonoBehaviour
{
    [Header("Grid Settings")] [SerializeField]
    private int gridHeight;

    [SerializeField] private int gridWidth;
    [SerializeField] private float hexSize = 1f;
    [SerializeField] private float pathOffset = .865f;

    [Header("Prefabs")] [SerializeField] private Tile tilePrefab;
    [SerializeField] private Node nodePrefab;
    [SerializeField] private Path pathPrefab;

    // private variables
    private List<(float, float)> nodePositions = new(); // lists of node ids
    private List<(float, float)> pathPositions = new(); // lists of path ids

    private GameObject tiles; // parent object for tiles
    private GameObject nodes; // parent object for nodes
    private GameObject paths; // parent object for paths

    private List<Vector3> tilePositions = new(); // list of tile positions
    private Dictionary<TileType, int> tileTypeCount = new(); // dictionary to keep track of tile types
    private Vector3 center; // center tile
    
    public void Generate(int gridWidth, int gridHeight)
    {
        this.gridWidth = gridWidth;
        this.gridHeight = gridHeight;
        
        // Verify and adjust
        VerifyHeight();

        // Initialize
        InitializeChildren();
        InitializeVariables();

        // Generate Grid
        GenerateGrid();
        
        // Set center tile
        this.center = GridUtils.GetCenter(tilePositions);
    }
    
    public void Remove()
    {
        Destroy(tiles, 0);
        Destroy(nodes, 0);
        Destroy(paths, 0);
        
        nodePositions.Clear();
        pathPositions.Clear();
    }

    private void InitializeChildren()
    {
        tiles = new GameObject("Tiles") { transform = { parent = transform } };
        nodes = new GameObject("Nodes") { transform = { parent = transform } };
        paths = new GameObject("Paths") { transform = { parent = transform } };
    }

    private void InitializeVariables()
    {
        tilePositions = GridUtils.GetTilesPosition(gridWidth, gridHeight, hexSize);
        tileTypeCount = GridUtils.GetTileTypeCount(
            tilePositions.Count,
            new Dictionary<TileType, float>
            {
                { TileType.Desert, .053f },
                { TileType.Sheep, .211f },
                { TileType.Wheat, .211f },
                { TileType.Wood, .211f },
                { TileType.Brick, .158f },
                { TileType.Rock, .158f },
            }
        );
    }

    private void VerifyHeight()
    {
        // make sure gridHeight is odd
        gridHeight = gridHeight % 2 != 0 ? gridHeight : gridHeight - 1;
        // make sure gridHeight is less than gridLength * 2
        gridHeight = Mathf.Min(gridHeight, gridWidth * 2 - 1);
    }

    private void GenerateGrid()
    {
        foreach (var hexPosition in tilePositions)
        {
            var tile = Instantiate(tilePrefab, hexPosition, Quaternion.identity, tiles.transform);

            var tileType = GetRandomTileType();
            tile.SetType(tileType);
            tile.SetText($"{tileType}");

            GenerateNodes(hexPosition);
            GeneratePaths(hexPosition);
        }
    }

    private void GenerateNodes(Vector3 hexPosition)
    {
        foreach (var position in GridUtils.GetNodesPosition(hexPosition, hexSize))
        {
            // round the position to 2 decimal places to create a unique id
            var roundedPosition = (Mathf.Round(position.x * 100) / 100, Mathf.Round(position.z * 100) / 100);

            // check if the node already exists, if not create a new node
            if (!nodePositions.Contains(roundedPosition))
            {
                var node = Instantiate(nodePrefab, position, Quaternion.identity, nodes.transform);
                node.SetText($"{roundedPosition}");
                nodePositions.Add(roundedPosition);
            }
        }
    }

    private void GeneratePaths(Vector3 hexPosition)
    {
        foreach (var position in GridUtils.GetPathsPosition(hexPosition, hexSize, pathOffset))
        {
            // round the position to 2 decimal places to create a unique id
            var roundedPosition = (Mathf.Round(position.x * 100) / 100, Mathf.Round(position.z * 100) / 100);

            // check if the path already exists, if not create a new path
            if (!pathPositions.Contains(roundedPosition))
            {
                var path = Instantiate(pathPrefab, position, Quaternion.identity, paths.transform);
                pathPositions.Add(roundedPosition);

                path.transform.LookAt(hexPosition);
                path.transform.position += new Vector3(0, .1f, 0);
            }
        }
    }

    private TileType GetRandomTileType()
    {
        var random = Random.Range(0, tileTypeCount.Count);
        var index = 0;
        foreach (var tileType in tileTypeCount.Keys)
        {
            if (index == random)
            {
                tileTypeCount[tileType]--;
                if (tileTypeCount[tileType] == 0)
                {
                    tileTypeCount.Remove(tileType);
                }
                return tileType;
            }

            index++;
        }

        return TileType.Water;
    }
    
    public Vector3 GetCenter()
    {
        return center;
    }
}