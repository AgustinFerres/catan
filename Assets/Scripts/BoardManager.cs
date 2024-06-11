using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    private BoardGenerator boardGenerator;
    private TMP_InputField widthInput, heightInput;
    private Button generateButton;

    private int width;
    private int height;
    private int count = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boardGenerator = FindObjectsByType<BoardGenerator>(FindObjectsSortMode.None)[0];
        generateButton = GetComponentInChildren<Button>();
        var inputFields = GetComponentsInChildren<TMP_InputField>();
        widthInput = inputFields[0];
        heightInput = inputFields[1];
        
        generateButton.onClick.AddListener(GenerateBoard);
        widthInput.onValueChanged.AddListener(OnWidthChanged);
        heightInput.onValueChanged.AddListener(OnHeightChanged);
    }
    
    private void GenerateBoard()
    {
        if (count != 0)
        {
            boardGenerator.Remove();
        }
        boardGenerator.Generate(width, height);
        count = 1;
    }
    
    private void OnWidthChanged(string value)
    {
        if (Regex.IsMatch(value, "[A-Za-z]"))
        {
            Debug.LogError("Width must be a number");
            return;
        }
        
        width = int.Parse(value);
    }
    
    private void OnHeightChanged(string value)
    {
        if (Regex.IsMatch(value, "[A-Za-z]"))
        {
            Debug.LogError("Height must be a number");
            return;
        }
        
        height = int.Parse(value);
    }
    
    
    
}
