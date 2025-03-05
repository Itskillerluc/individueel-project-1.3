using TMPro;
using UnityEngine;

public class TMPTextWrapper : MonoBehaviour, IText
{
        public TMP_InputField inputField;
        
        public string text
        {
                get => inputField.text;
                set => inputField.text = value;
        }
}