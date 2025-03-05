using TMPro;
using UnityEngine;

public class TextMeshProUGUIWrapper : MonoBehaviour, IText
{
    public TextMeshProUGUI textMeshProUGUI;
    
    public string text
    {
        get => textMeshProUGUI.text;
        set => textMeshProUGUI.text = value;
    }
}