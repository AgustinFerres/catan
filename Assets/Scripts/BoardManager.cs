using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    private BoardGenerator boardGenerator;
    private TMP_InputField widthInput, heightInput;
    private Button generateButton;
    private Camera mainCamera;

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
        mainCamera = Camera.main;
        
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
        mainCamera.transform.position = boardGenerator.GetCenter();
        mainCamera.transform.position += new Vector3(0, 4 * height, 0);
        mainCamera.transform.rotation = Quaternion.Euler(90, 0, 30);
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
