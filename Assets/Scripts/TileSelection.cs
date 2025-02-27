using System.Collections.Generic;
using UnityEngine;

public class TileSelection : MonoBehaviour
{
    public List<GameObject> tiles;
    public List<GameObject> outlines;
    public int selectedTileIndex = 0;

    public void SetTile(int tileIndex)
    {
        selectedTileIndex = tileIndex;
        foreach (var outline in outlines)
        {
            outline.gameObject.SetActive(false);
        }
        outlines[tileIndex].gameObject.SetActive(true);
    }
}
