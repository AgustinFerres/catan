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
    private Dictionary<Vector3, int> nodes = new Dictionary<Vector3, int>();
    private List<int> nodesIndex = new List<int>();

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

                int value = i * 2 + j;
                GenerateTopNodes(hex, value, nodes); // Generate top nodes
                int value2 = j < gridHeight / 2 ? value + actualLength * 2 + 2 : value + i + k + 2;
                GenerateBottomNodes(hex, value2, nodes); // Generate bottom node
                i++;
            }
        }

        foreach (var node in nodes)
        {
            var n = Instantiate(this.node);
            n.transform.position = node.Key;
            n.SetText(node.Value.ToString());
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

    void GenerateTopNodes(Tile hexTile, int value, Dictionary<Vector3, int> nodes)
    {
        // node 1
        Vector3 node1 = hexTile.transform.position - new Vector3(hexSize, 0, 0);
        CheckAndAddNode(node1, value, nodes);

        // node 2
        Vector3 node2 = hexTile.transform.position - new Vector3(hexSize / 2, 0, hexSize * Mathf.Sqrt(3) / -2);
        CheckAndAddNode(node2, value + 1, nodes);

        // node 3
        Vector3 node3 = hexTile.transform.position + new Vector3(hexSize / 2, 0, hexSize * Mathf.Sqrt(3) / 2);
        CheckAndAddNode(node3, value + 2, nodes);
    }

    void GenerateBottomNodes(Tile hexTile, int value, Dictionary<Vector3, int> nodes)
    {
        // node 4
        Vector3 node4 = hexTile.transform.position - new Vector3(hexSize / 2, 0, hexSize * Mathf.Sqrt(3) / 2);
        CheckAndAddNode(node4, value, nodes);

        // node 5
        Vector3 node5 = hexTile.transform.position + new Vector3(hexSize / 2, 0, hexSize * Mathf.Sqrt(3) / -2);
        CheckAndAddNode(node5, value + 1, nodes);

        // node 6
        Vector3 node6 = hexTile.transform.position + new Vector3(hexSize, 0, 0);
        CheckAndAddNode(node6, value + 2, nodes);
    }

    void CheckAndAddNode(Vector3 node, int value, Dictionary<Vector3, int> nodes)
    {
        if (!nodes.ContainsKey(node) && !nodesIndex.Contains(value))
        {
            nodes.Add(node, value);
            nodesIndex.Add(value);
        }
    }

}
