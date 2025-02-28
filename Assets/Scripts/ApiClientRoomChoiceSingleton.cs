using System;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;
using UnityEngine;

public class ApiClientRoomChoiceSingleton : MonoBehaviour
{
    public static ApiClientRoomChoiceSingleton Instance { get; private set; }
    
    public async Task<GetRoomsResponseDto[]> GetRoomsList()
    {
        //var response = await ApiUtil.PerformApiCall("https://avansict2226538.azurewebsites.net/api/Rooms", "Get", token: ApiTokenSingleton.Instance.Token);
        var response = await ApiUtil.PerformApiCall("https://localhost:7244/api/Rooms", "Get", token: UserSingleton.Instance.Token);
        var room = JsonConvert.DeserializeObject<GetRoomsResponseDto[]>(response);
        return room;
    }
    
    public async Task DeleteRoom(Guid roomId)
    {
        // todo
        // "https://avansict2226538.azurewebsites.net/api/Rooms"
        await ApiUtil.PerformApiCall($"https://localhost:7244/api/Rooms?roomId={roomId}", "Delete", token: UserSingleton.Instance.Token);
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
    }
}