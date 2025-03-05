using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
    public List<GameObject> props;
    private int _objectLayer;
    
    public List<GameObject> Props => props;
    
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

public interface IGameManager
{
    public List<GameObject> Props { get; }
    public void AddProp(Prop prop, bool isBackground);
}
