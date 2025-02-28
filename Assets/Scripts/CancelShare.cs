using UnityEngine;
using UnityEngine.SceneManagement;

public class CancelShare : MonoBehaviour
{
    public async void Cancel()
    {
        await SceneManager.LoadSceneAsync("Scenes/RoomChoice");
    }
}
