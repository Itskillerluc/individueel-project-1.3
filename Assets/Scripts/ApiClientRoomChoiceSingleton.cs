using System;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;
using UnityEngine;

public class ApiClientRoomChoiceSingleton : MonoBehaviour
{
    public static ApiClientRoomChoiceSingleton Instance { get; private set; }
    
    private ApiUtil _apiUtil;
    
    
    public async Task<GetRoomsResponseDto[]> GetRoomsList()
    {
        //var response = await ApiUtil.PerformApiCall("https://avansict2226538.azurewebsites.net/api/Rooms", "Get", token: ApiTokenSingleton.Instance.Token);
        var response = await _apiUtil.PerformApiCall("https://avansict2226538.azurewebsites.net/api/Rooms", "Get", token: UserSingleton.Instance.AccessToken);
        var room = JsonConvert.DeserializeObject<GetRoomsResponseDto[]>(response);
        return room;
    }
    
    public async Task DeleteRoom(Guid roomId)
    {
        // todo
        // "https://avansict2226538.azurewebsites.net/api/Rooms"
        await _apiUtil.PerformApiCall($"https://avansict2226538.azurewebsites.net/api/Rooms?roomId={roomId}", "Delete", token: UserSingleton.Instance.AccessToken);
    }
    
    public async Task DeleteUserRoom(string username, Guid roomId)
    {
        // todo
        // "https://avansict2226538.azurewebsites.net/api/Rooms"
        await _apiUtil.PerformApiCall($"https://avansict2226538.azurewebsites.net/api/UserRooms?username={username}&roomId={roomId}", "Delete", token: UserSingleton.Instance.AccessToken);
    }
    
    private void Awake()
    {
        // Destroy this object if we already have a singleton configured
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _apiUtil = new ApiUtil();
    }
}