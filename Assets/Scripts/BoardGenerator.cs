using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField] private Tile hexTile;
    [SerializeField] private Node node;
    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;
    [SerializeField] private Camera camera;
    private readonly float hexSize = 1.0f;
    private Dictionary<int, Vector3> nodes = new Dictionary<int, Vector3>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int i = 0; // Start i from 0

        camera.transform.position = new Vector3(gridWidth * hexSize * .6f, (gridWidth + gridHeight) * 2, -gridHeight * hexSize * Mathf.Sqrt(3) * .22f);
        camera.transform.rotation = Quaternion.Euler(90, -30, 0);

        gridHeight = gridHeight % 2 != 0 ? gridHeight : gridHeight - 1; // make sure gridHeight is odd
        gridHeight = gridHeight > gridWidth * 2 ? gridWidth * 2 - 1 : gridHeight; // make sure gridHeight is less than gridLength * 2


        int initialLength = gridWidth - Mathf.FloorToInt(gridHeight / 2);
        for (int j = 0; j < gridHeight; j++)
        {
            int actualLength = ActualLength(initialLength, j);
            for (int k = 0; k < actualLength; k++)
            {
                var hex = Instantiate(hexTile);
                hex.SetText(i.ToString());
                hex.transform.position = HexToPixel(new Vector2(ActualPosition(k, j), -j));

                GenerateTopNodes(hex, i * 2 + j, nodes); // Generate top nodes
                GenerateBottomNodes(hex, i * 2 + j + actualLength + 2, nodes); // Generate bottom nodes
                i++;
            }
        }

        foreach (var node in nodes)
        {
            var n = Instantiate(this.node);
            n.transform.position = node.Value;
            n.SetText(node.Key.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 HexToPixel(Vector2 hex)
    {
        var x = hexSize * 3 / 2 * hex.x;
        var z = hexSize * Mathf.Sqrt(3) * (hex.y + hex.x / 2);
        return new Vector3(x, 0, z);
    }

    int ActualLength(int initialLength, int j)
    {
        return j <= gridHeight / 2 ? initialLength + j : gridWidth - (j - gridHeight / 2);
    }

    int ActualPosition(int k, int j)
    {
        return j <= gridHeight / 2 ? k : k + (j - gridHeight / 2);
    }

    void GenerateTopNodes(Tile hexTile, int value, Dictionary<int, Vector3> nodes)
    {
        // node 1
        if (!nodes.ContainsKey(value))
        {
            nodes.Add(value, hexTile.transform.position - new Vector3(hexSize, 0, 0));
        }

        // node 2
        if (!nodes.ContainsKey(value + 1))
        {
            nodes.Add(value + 1, hexTile.transform.position - new Vector3(hexSize / 2, 0, hexSize * Mathf.Sqrt(3) / -2));
        }

        // node 3
        if (!nodes.ContainsKey(value + 2))
        {
            nodes.Add(value + 2, hexTile.transform.position + new Vector3(hexSize / 2, 0, hexSize * Mathf.Sqrt(3) / 2));
        }
    }

    void GenerateBottomNodes(Tile hexTile, int value, Dictionary<int, Vector3> nodes)
    {
        // node 4
        if (!nodes.ContainsKey(value))
        {
            nodes.Add(value, hexTile.transform.position - new Vector3(hexSize / 2, 0, hexSize * Mathf.Sqrt(3) / 2));
        }

        // node 5
        if (!nodes.ContainsKey(value + 1))
        {
            nodes.Add(value + 1, hexTile.transform.position + new Vector3(hexSize / 2, 0, hexSize * Mathf.Sqrt(3) / -2));
        }

        // node 6
        if (!nodes.ContainsKey(value + 2))
        {
            nodes.Add(value + 2, hexTile.transform.position + new Vector3(hexSize, 0, 0));
        }
    }   

    //List<Node> GetNodesByHex(Tile hexTile)
    //{
    //    int hexValue = int.Parse(hexTile.GetText());


    //}
}
