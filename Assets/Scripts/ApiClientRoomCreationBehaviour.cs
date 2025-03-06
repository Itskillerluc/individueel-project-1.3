using System.Linq;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApiClientRoomCreationBehaviour : MonoBehaviour
{
    public TMPTextWrapper roomNameInputField;
    public TMPTextWrapper roomWidthInputField;
    public TMPTextWrapper roomHeightInputField;
    public TileSelection tileSelection;
    public TextMeshProUGUIWrapper statusMessage;

    private ApiUtil _apiUtil;
    private ApiClientRoomCreation _apiClientRoomCreation;

    private void Awake()
    {
        _apiUtil = new ApiUtil();
        _apiClientRoomCreation = new ApiClientRoomCreation();
    }

    public async void CreateRoom()
    {
        if (await _apiClientRoomCreation.CreateRoom(_apiUtil, statusMessage, roomNameInputField, roomHeightInputField,
                roomWidthInputField, tileSelection.Tiles[tileSelection.SelectedTileIndex].name, UserSingleton.Instance))
        {
            await SceneManager.LoadSceneAsync("Scenes/RoomChoice");
        }
    }
}

public class ApiClientRoomCreation
{
    public async Task<bool> CreateRoom(IApiUtil apiUtil, IText statusMessage, IText roomNameInputField, IText roomHeightInputField, IText roomWidthInputField, string tileName, IUserSingleton userSingleton)
    {
        if (userSingleton.AccessToken == null)
        {
            Debug.LogError("No token found");
            return false;
        }
        Debug.Log("Getting rooms");

        var roomsList = await GetRoomsList(apiUtil, userSingleton);

        if (roomsList.Count(room => room.isOwner) >= 5)
        {
            Debug.Log("user already has 5 rooms");
            statusMessage.text = "You have reached the maximum number of 5 rooms.";
            return false;
        }
        
        Debug.Log("Creating room");
        
        if (roomNameInputField.text == "")
        {
            statusMessage.text = "Please enter a room name.";
            return false;
        }

        if (roomNameInputField.text.Length > 25)
        {
            statusMessage.text = "The room name should be shorter than 25 characters.";
            return false;
        }
        
        if (roomWidthInputField.text == "")
        {
            statusMessage.text = "Please enter a room width.";
            return false;
        }
        
        if (roomHeightInputField.text == "")
        {
            statusMessage.text = "Please enter a room height.";
            return false;
        }
        
        var roomName = roomNameInputField.text;
        var roomWidth = int.Parse(roomWidthInputField.text);
        var roomHeight = int.Parse(roomHeightInputField.text);

        if (roomWidth < 20)
        {
            statusMessage.text = "The room width should be larger than 20.";
            return false;
        }

        if (roomHeight < 10)
        {
            statusMessage.text = "The room height should be larger than 10.";
            return false;
        }

        if (roomWidth > 200)
        {
            statusMessage.text = "The room width should be smaller than 200.";
            return false;
        }
        
        if (roomHeight > 100)
        {
            statusMessage.text = "The room height should be smaller than 100.";
            return false;
        }
        
        
        
        if (roomsList.Any(room => room.name.Equals(roomName)))
        {
            statusMessage.text = "A room with this name already exists.";
            return false;
        }
        
        var room = new PostRoomsRequestDto
        {
            name = roomName,
            width = roomWidth,
            height = roomHeight,
            tileId = tileName
        };
        
        await apiUtil.PerformApiCall("https://avansict2226538.azurewebsites.net/api/Rooms", "Post", JsonConvert.SerializeObject(room), userSingleton.AccessToken);

        statusMessage.text = "Room created!";

        return true;
    }
    
    private async Task<GetRoomsResponseDto[]> GetRoomsList(IApiUtil apiUtil, IUserSingleton userSingleton)
    {
        var response = await apiUtil.PerformApiCall("https://avansict2226538.azurewebsites.net/api/Rooms", "Get", token: userSingleton.AccessToken);
        var room = JsonConvert.DeserializeObject<GetRoomsResponseDto[]>(response);
        return room;
    } 
}
