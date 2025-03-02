using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitConfirmation : MonoBehaviour
{
    public async void Exit()
    {
        await SceneManager.LoadSceneAsync("Scenes/RoomChoice");
    }

    public void Cancel()
    {
        Destroy(gameObject);
    }
}
