using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class DeletePropButton : MonoBehaviour
{
    [CanBeNull] 
    public Prop prop;
    public Button button;

    public void Delete()
    {
        FindAnyObjectByType<GameManager>().props.Remove(gameObject);
        Destroy(prop?.gameObject);
        button.interactable = false;
        prop = null;
    }
}