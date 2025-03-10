using System;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;
using UnityEngine;

public class ApiClientEnvironmentBuilderBehaviour : MonoBehaviour
{
    public TextMeshProUGUIWrapper statusText;
    public GameManager gameManager;

    private ApiUtil _apiUtil;
    private ApiClientEnvironmentBuilder _apiClientEnvironmentBuilder;

    private void Awake()
    {
        _apiUtil = new ApiUtil();
        _apiClientEnvironmentBuilder = new ApiClientEnvironmentBuilder();
    }

    public async void Save()
    {
        await _apiClientEnvironmentBuilder.Save(gameManager, _apiUtil, statusText, RoomSingleton.Instance.Room.roomId, UserSingleton.Instance);
    }
}

public class ApiClientEnvironmentBuilder
{
    public async Task Save(IGameManager gameManager, IApiUtil apiUtil, IText statusText, Guid roomId, IUserSingleton userSingleton)
    {
        var props = gameManager.Props;

        await apiUtil.PerformApiCall($"https://avansict2226538.azurewebsites.net/api/Props?roomId={roomId}", "DELETE", token: userSingleton.AccessToken);
        
        foreach (var prop in props)
        {
            var postPropsRequestDto = new PostPropsRequestDto
            {
                prefabId = prop.name.Replace("(Clone)", ""),
                posX = prop.transform.position.x,
                posY = prop.transform.position.y,
                rotation = prop.transform.rotation.eulerAngles.z,
                scaleX = prop.transform.localScale.x, 
                scaleY = prop.transform.localScale.y,
                sortingLayer = prop.GetComponent<SpriteRenderer>().sortingOrder,
                roomId = RoomSingleton.Instance.Room.roomId
            };
            await apiUtil.PerformApiCall($"https://avansict2226538.azurewebsites.net/api/Props", "Post", JsonConvert.SerializeObject(postPropsRequestDto), token: userSingleton.AccessToken);
        }
        statusText.text = "Saved!";
    }
}
