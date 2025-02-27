using UnityEngine;

public class ApiClientRoomChoice : MonoBehaviour
{
    public async void GetRoomsList()
    {
        //todo
        var response = await ApiUtil.PerformApiCall("https://localhost:7244/api/Rooms", "Get", token: ApiTokenSingleton.Instance.Token);
        //var response = await ApiUtil.PerformApiCall("https://avansict2226538.azurewebsites.net/api/Rooms", "Get", token: ApiTokenSingleton.Instance.Token);
        Debug.Log(response);
    }
}