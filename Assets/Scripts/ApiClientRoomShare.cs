using Models;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class ApiClientRoomShare : MonoBehaviour
{
    public TMP_InputField emailInput;
    
    public async void ShareRoom()
    {
        //var response = await ApiUtil.PerformApiCall("https://avansict2226538.azurewebsites.net/api/Rooms", "Get", token: ApiTokenSingleton.Instance.Token);
        var request = new PostUserRoomsRequestDto
        {
            roomid = RoomSingleton.Instance.Room.roomId,
            username = emailInput.text.ToLower(),
            isOwner = false
        };
        await ApiUtil.PerformApiCall("https://localhost:7244/api/UserRooms", "Post", JsonConvert.SerializeObject(request), token: UserSingleton.Instance.Token);
    }

}
