using UnityEngine;
using UnityEngine.SceneManagement;

public class Logout : MonoBehaviour
{
    public async void LogoutUser()
    {
        UserSingleton.Instance.Token = null;
        await SceneManager.LoadSceneAsync("Scenes/LoginScreen");
        Debug.Log("User logged out!");
    }
}