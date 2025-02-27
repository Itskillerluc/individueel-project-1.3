using UnityEngine;

public class Room : MonoBehaviour
{
    private const float WallWidth = 5f;
    private const float HalfWallHeight = .2f;

    public GameObject wall;
    public GameObject corner;
    public GameObject tile;

    public float width = 0;
    public float height = 0;

    //Todo: remove this when done with testing
    public void Test()
    {
        GenerateNewRoom(10, 10, tile, wall, corner);
    }

    public void GenerateNewRoom(float width, float height, GameObject floor, GameObject wall, GameObject corner)
    {
        this.width = width;
        this.height = height;

        var floorObject = Instantiate(floor, new Vector3(0, 0, 105), Quaternion.identity);
        floorObject.GetComponent<Prop>().enabled = false;
        var spriteRenderer = floor.GetComponent<SpriteRenderer>();
        spriteRenderer.size = new Vector2(width, height);

        GenerateLeftWall(width, height);
        GenerateRightWall(width, height);
        GenerateTopWall(width, height);
        GenerateBottomWall(width, height);

        GenerateCorners(width, height);
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
}