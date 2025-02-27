using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;
using UnityEngine;

public class ApiClientRoomChoice : MonoBehaviour
{
    public async Task<GetRoomsResponseDto[]> GetRoomsList()
    {
        //var response = await ApiUtil.PerformApiCall("https://avansict2226538.azurewebsites.net/api/Rooms", "Get", token: ApiTokenSingleton.Instance.Token);
        var response = await ApiUtil.PerformApiCall("https://localhost:7244/api/Rooms", "Get", token: UserSingleton.Instance.Token);
        var room = JsonConvert.DeserializeObject<GetRoomsResponseDto[]>(response);
        return room;
    } 
}