using Models;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class ApiClientEnvironmentBuilder : MonoBehaviour
{
    public TextMeshProUGUI statusText;
    public GameManager gameManager;

    public async void Save()
    {
        var props = gameManager.props;

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
            
            // todo
            // "https://avansict2226538.azurewebsites.net/api/Rooms"
            await ApiUtil.PerformApiCall($"https://localhost:7244/api/Props", "Post", JsonConvert.SerializeObject(postPropsRequestDto), token: UserSingleton.Instance.Token);
        }
        statusText.text = "Saved!";
    }
}
