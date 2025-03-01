using System.Linq;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApiClientRoomCreation : MonoBehaviour
{
    public TMP_InputField roomNameInputField;
    public TMP_InputField roomWidthInputField;
    public TMP_InputField roomHeightInputField;
    public TileSelection tileSelection;
    public TextMeshProUGUI statusMessage;

    public async void CreateRoom()
    {
        if (UserSingleton.Instance?.Token == null)
        {
            Debug.LogError("No token found");
            return;
        }
        Debug.Log("Getting rooms");

        var roomsList = await GetRoomsList();

        if (roomsList.Count(room => room.users.Single(user => user.user.Equals(UserSingleton.Instance.Name)).isOwner) >= 5)
        {
            Debug.Log("user already has 5 rooms");
            return;
        }
        
        Debug.Log("Creating room");
        
        var roomName = roomNameInputField.text;
        var roomWidth = int.Parse(roomWidthInputField.text);
        var roomHeight = int.Parse(roomHeightInputField.text);

        if (roomWidth < 5 || roomHeight < 5)
        {
            statusMessage.text = "The room width and height should be larger than 5.";
            return;
        }
        
        if (roomsList.Any(room => room.name.Equals(roomName)))
        {
            statusMessage.text = "A room with this name already exists.";
            return;
        }
        
        var tiles = tileSelection.tiles[tileSelection.selectedTileIndex];

        var room = new PostRoomsRequestDto
        {
            name = roomName,
            width = roomWidth,
            height = roomHeight,
            tileId = tiles.name
        };
        
        var response = await ApiUtil.PerformApiCall("https://localhost:7244/api/Rooms", "Post", JsonConvert.SerializeObject(room), UserSingleton.Instance.Token);
        //var response = await ApiUtil.PerformApiCall("https://avansict2226538.azurewebsites.net/api/Rooms", "Post", JsonUtility.ToJson(room), ApiTokenSingleton.Instance.Token);

        await SceneManager.LoadSceneAsync("Scenes/RoomChoice");
    }
    
    private async Task<GetRoomsResponseDto[]> GetRoomsList()
    {
        //var response = await ApiUtil.PerformApiCall("https://avansict2226538.azurewebsites.net/api/Rooms", "Get", token: ApiTokenSingleton.Instance.Token);
        var response = await ApiUtil.PerformApiCall("https://localhost:7244/api/Rooms", "Get", token: UserSingleton.Instance.Token);
        var room = JsonConvert.DeserializeObject<GetRoomsResponseDto[]>(response);
        return room;
    } 
}
