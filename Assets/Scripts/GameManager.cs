using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _objectLayer;
   
    public void IncreaseObjectLayer()
    {
        _objectLayer++;
    }

    public int GetObjectLayer()
    {
        return _objectLayer;
    }
}
