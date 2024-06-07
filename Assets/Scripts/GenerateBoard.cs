using System;
using UnityEngine;

public class GenerateBoard : MonoBehaviour
{
    [SerializeField] private GameObject hexTile;
    private readonly int gridRadius = 2;
    private readonly float hexSize = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int q = -gridRadius; q <= gridRadius; q++)
        {
            for (int r = -gridRadius; r <= gridRadius; r++)
            {
                if (Math.Abs(q + r) <= gridRadius)
                {
                    var hex = Instantiate(hexTile);

                    hex.transform.position = HexToPixel(new Vector2(q, r));

                    hex.transform.parent = transform;
                }
            }
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
}
