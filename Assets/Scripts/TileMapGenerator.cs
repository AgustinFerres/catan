using UnityEngine;

public class TileMapGenerator : MonoBehaviour
{
    [SerializeField] private Tile hexTile;
    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;

    void Start()
    {
        int i = 0;

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

                i++;
            }
        }
    }

    Vector3 HexToPixel(Vector2 hex)
    {
        var x = (Mathf.Sqrt(3) * hex.x) + (Mathf.Sqrt(3) / 2 * hex.y);
        var z = (3.0f / 2.0f) * hex.y;
        return new Vector3(x, 0, -z);
    }

    int ActualLength(int initialLength, int j)
    {
        return j <= gridHeight / 2 ? initialLength + j : gridWidth - (j - gridHeight / 2);
    }

    int ActualPosition(int k, int j)
    {
        return j <= gridHeight / 2 ? k : k + (j - gridHeight / 2);
    }
}
