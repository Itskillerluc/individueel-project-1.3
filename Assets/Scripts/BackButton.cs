using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public async void OnClick()
    {
        await SceneManager.LoadSceneAsync("Scenes/RoomChoice");
    }
}