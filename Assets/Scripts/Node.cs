using System;
using TMPro;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string value)
    {
        text.text = value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(text.text);
    }

}
