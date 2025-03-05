using System.Collections.Generic;
using UnityEngine;

public class TileSelection : MonoBehaviour, ITileSelection
{
    public List<GameObject> tiles;
    public List<GameObject> outlines;
    public int selectedTileIndex;

    public List<GameObject> Tiles => tiles;
    public List<GameObject> Outlines => outlines;
    public int SelectedTileIndex => selectedTileIndex;

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

public interface ITileSelection
{
    public List<GameObject> Tiles { get; }
    public List<GameObject> Outlines { get; }
    public int SelectedTileIndex { get; }
    public void SetTile(int tileIndex);
}
