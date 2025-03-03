using System.Collections.Generic;
using UnityEngine;

public class PropList : MonoBehaviour
{
    public List<GameObject> prefabs;
    public GameManager gameManager;
    
    public void AddObject(GameObject instance, bool isBackground)
    {
        var mousePosition = Camera.main?.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition == null) return;
        instance.transform.position = new Vector3(mousePosition.Value.x, mousePosition.Value.y, 0);
        var prop = instance.GetComponent<Prop>();
        gameManager.AddProp(prop, isBackground);
        prop.SetDragging();
        prop.SetHovering();
    }
}
