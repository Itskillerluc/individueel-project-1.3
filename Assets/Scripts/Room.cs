using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class Room : MonoBehaviour
{
    private const float WallWidth = 5f;
    private const float HalfWallHeight = .2f;


    public List<Prop> values;
    
    public List<GameObject> editUI;
    public List<PropKeyValue> prefabs;
    public GameObject wall;
    public GameObject corner;
    public TextMeshProUGUI roomName;
    
    public float width;
    public float height;
    

    private void Start()
    {
        GenerateNewRoom();
        roomName.text = RoomSingleton.Instance.Room.name;
        foreach (var obj in editUI)
        {
            obj.SetActive(RoomSingleton.Instance.CanEdit);
        }
    }
    
    private void GenerateNewRoom()
    {
        var room = RoomSingleton.Instance.Room;
        
        width = room.width;
        height = room.height;

        var floorObject = Instantiate(prefabs.Find(kv => kv.name == room.tileId).prop, new Vector3(0, 0, 105), Quaternion.identity);
        floorObject.GetComponent<Prop>().enabled = false;
        var spriteRenderer = floorObject.GetComponent<SpriteRenderer>();
        spriteRenderer.size = new Vector2(width, height);

        GenerateLeftWall(width, height);
        GenerateRightWall(width, height);
        GenerateTopWall(width, height);
        GenerateBottomWall(width, height);

        GenerateCorners(width, height);
        
        foreach (var prop in room.props)
        {
            if (prop.propId.Equals(Guid.Empty)) continue;
            var propPrefab = prefabs.Find(kv => kv.name == prop.propId.ToString()).prop;
            if (propPrefab is null) continue;
            var propObject = Instantiate(propPrefab, new Vector3(prop.posX, prop.posY, 100), quaternion.Euler(0, 0, prop.rotation));
            propObject.transform.localScale = new Vector3(prop.scaleX, prop.scaleY, 1);
            propObject.GetComponent<SpriteRenderer>().sortingOrder = prop.sortingLayer;
        }
    }

    private void GenerateLeftWall(float roomWidth, float roomHeight)
    {
        var wallInstance = Instantiate(wall, Vector3.zero - new Vector3((roomWidth / 2) + HalfWallHeight, 0, -105), Quaternion.identity);
        wallInstance.GetComponent<Prop>().enabled = false;
        wallInstance.transform.rotation = Quaternion.Euler(0, 0, 90);
        wallInstance.transform.localScale = new Vector3(roomHeight / WallWidth, 1, 1);
    }

    private void GenerateRightWall(float roomWidth, float roomHeight)
    {
        var wallInstance = Instantiate(wall, Vector3.zero + new Vector3((roomWidth / 2) + HalfWallHeight, 0, 105), Quaternion.identity);
        wallInstance.GetComponent<Prop>().enabled = false;
        wallInstance.transform.rotation = Quaternion.Euler(0, 0, 90);
        wallInstance.transform.localScale = new Vector3(roomHeight / WallWidth, 1, 1);
    }

    private void GenerateTopWall(float roomWidth, float roomHeight)
    {
        var wallInstance = Instantiate(wall, Vector3.zero + new Vector3(0, (roomHeight / 2) + HalfWallHeight, 105), Quaternion.identity);
        wallInstance.GetComponent<Prop>().enabled = false;
        wallInstance.transform.localScale = new Vector3(roomWidth / WallWidth, 1, 1);
    }

    private void GenerateBottomWall(float roomWidth, float roomHeight)
    {
        var wallInstance = Instantiate(wall, Vector3.zero - new Vector3(0, (roomHeight / 2) + HalfWallHeight, -105), Quaternion.identity);
        wallInstance.GetComponent<Prop>().enabled = false;
        wallInstance.transform.localScale = new Vector3(roomWidth / WallWidth, 1, 1);
    }

    private void GenerateCorners(float roomWidth, float roomHeight)
    {
        var corner1 = Instantiate(corner, Vector3.zero + new Vector3((roomWidth / 2) - 1 + 0.41f, (roomHeight / 2) - 1 + 0.41f, 100), Quaternion.identity);
        corner1.GetComponent<Prop>().enabled = false;
        var corner2 = Instantiate(corner, Vector3.zero + new Vector3((roomWidth / 2) - 1 + 0.41f, -(roomHeight / 2) + 1 - 0.41f, 100), Quaternion.Euler(0, 0, 270));
        corner2.GetComponent<Prop>().enabled = false;
        var corner3 = Instantiate(corner, Vector3.zero + new Vector3(-(roomWidth / 2) + 1 - 0.41f, (roomHeight / 2) - 1 + 0.41f, 100), Quaternion.Euler(0, 0, 90));
        corner3.GetComponent<Prop>().enabled = false;
        var corner4 = Instantiate(corner, Vector3.zero + new Vector3(-(roomWidth / 2) + 1 - 0.41f, -(roomHeight / 2) + 1 - 0.41f, 100), Quaternion.Euler(0, 0, 180));
        corner4.GetComponent<Prop>().enabled = false;
    }
    
    // So I don't have to type 200+ strings.
    [ContextMenu("fill")]
    public void Fill()
    {
        prefabs.Clear();
        foreach (var val in values)
        {
            prefabs.Add(new PropKeyValue {name = val.name, prop = val});
        }
    }
}