using Models;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class ApiClientRoomShare : MonoBehaviour
{
    public TMP_InputField emailInput;
    
    private ApiUtil _apiUtil;
    
    private void Awake()
    {
        _apiUtil = new ApiUtil();
    }
    
    public async void ShareRoom()
    {
        var request = new PostUserRoomsRequestDto
        {
            roomid = RoomSingleton.Instance.Room.roomId,
            username = emailInput.text.ToLower(),
            isOwner = false
        };
        await _apiUtil.PerformApiCall("https://avansict2226538.azurewebsites.net/api/UserRooms", "Post", JsonConvert.SerializeObject(request), token: UserSingleton.Instance.AccessToken);
    }

}
