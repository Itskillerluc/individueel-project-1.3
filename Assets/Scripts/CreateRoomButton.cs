using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateRoomButton : MonoBehaviour
{
    public async void OnClick()
    {
        await SceneManager.LoadSceneAsync("Scenes/RoomCreation");
    }
}