using System.Collections.Generic;
using Classes;
using UnityEngine;
using TMPro;

public class Tile : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    private TileType type;
    private int number;
    
    private Dictionary<TileType, Color> colors = new()
    {
        {TileType.Water, Color.blue},
        {TileType.Desert, Color.white},
        {TileType.Sheep, Color.green},
        {TileType.Wheat, Color.yellow},
        {TileType.Wood, Color.magenta},
        {TileType.Brick, Color.red},
        {TileType.Rock, Color.gray},
        {TileType.Gold, Color.yellow}
    }; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetText()
    {
        return text.text;
    }

    public void SetText(string value)
    {
        text.text = value;
    }
    
    public void SetColor(Color value)
    {
        text.color = value;
    }

    public void SetType(TileType value)
    {
        type = value;
        this.SetColor(colors[value]);
    }

    public void SetNumber(int value)
    {
        number = value;
    }
}
