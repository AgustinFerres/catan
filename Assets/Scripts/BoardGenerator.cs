using System;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField] private Tile hexTile;
    [SerializeField] private int gridLength = 3;
    [SerializeField] private int gridHeight = 3;
    private readonly float hexSize = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int i = 1;

        for (int j = 0; j < 3; j++)
        {
            for (int k = 0; k < 3; k++)
            {
                var hex = Instantiate(hexTile);
                hex.SetText(i++.ToString());

            }
        }
        //for (int q = -gridLength; q <= gridLength; q++)
        //{
        //    for (int r = -gridHeight; r <= gridHeight; r++)
        //    {
        //        if (Math.Abs(q + r) <= MathF.Max(gridHeight, gridLength))
        //        {
        //            
        //            hex.SetText(i++.ToString());
        //            // assign type
        //            // assign number

        //            hex.transform.position = HexToPixel(new Vector2(q, r));

        //        }
        //    }
        //}

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

    (double, double) HexCorner((double, double) center, double size, int i)
    {
        double angle_deg = 60 * i + 30;
        double angle_rad = Math.PI / 180 * angle_deg;
        return (
            center.Item1 + size * Math.Cos(angle_rad),
            center.Item2 + size * Math.Sin(angle_rad)
        );
    }
}
