using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using TMPro;
using UnityEngine;

public class ApiClientRoomCreation : MonoBehaviour
{
    public TMP_InputField roomNameInputField;
    public TMP_InputField roomWidthInputField;
    public TMP_InputField roomHeightInputField;
    public TileSelection tileSelection;
    public TextMeshProUGUI statusMessage;

    public async void CreateRoom()
    {
        if (ApiTokenSingleton.Instance?.Token == null)
        {
            Debug.LogError("No token found");
            return;
        }
        Debug.Log("Getting rooms");

        var roomsList = await GetRoomsList();

        if (roomsList.rooms.Count >= 5)
        {
            Debug.Log("user already has 5 rooms");
            return;
        }
        
        Debug.Log("Creating room");
        
        var roomName = roomNameInputField.text;
        var roomWidth = int.Parse(roomWidthInputField.text);
        var roomHeight = int.Parse(roomHeightInputField.text);
        var tiles = tileSelection.tiles[tileSelection.selectedTileIndex];

        var room = new PostRoomsRequestDto
        {
            name = roomName,
            widht = roomWidth,
            height = roomHeight,
            tileId = tiles.name
        };
        
        var response = await ApiUtil.PerformApiCall("https://localhost:7244/api/Rooms", "Post", JsonUtility.ToJson(room), ApiTokenSingleton.Instance.Token);
        //var response = await ApiUtil.PerformApiCall("https://avansict2226538.azurewebsites.net/api/Rooms", "Post", JsonUtility.ToJson(room), ApiTokenSingleton.Instance.Token);
        Debug.Log(response);
    }
    
    private async Task<GetRoomsResponseDto> GetRoomsList()
    {
        //var response = await ApiUtil.PerformApiCall("https://avansict2226538.azurewebsites.net/api/Rooms", "Get", token: ApiTokenSingleton.Instance.Token);
        var response = await ApiUtil.PerformApiCall("https://localhost:7244/api/Rooms", "Get", token: ApiTokenSingleton.Instance.Token);
        //BUG:
        var room = JsonUtility.FromJson<GetRoomsResponseDto>(response);
        Debug.Log(response);
        return room;
    }
}
