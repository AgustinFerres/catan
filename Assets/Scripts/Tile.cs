using UnityEngine;
using TMPro;

public class Tile : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    private TileType type;
    private int number;

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

    public void SetType(TileType value)
    {
        type = value;
    }

    public void SetNumber(int value)
    {
        number = value;
    }
}