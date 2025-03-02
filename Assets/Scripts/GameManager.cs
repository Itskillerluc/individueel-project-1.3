using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> props;
    private int _objectLayer;
   
    public void AddProp(Prop prop, bool isBackground)
    {
        props.Add(prop.gameObject);
        if (!isBackground)
        {
            prop.gameObject.GetComponent<SpriteRenderer>().sortingOrder = ++_objectLayer;
        }
        else
        {
            //todo check if this works well.
            prop.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
    }
}
