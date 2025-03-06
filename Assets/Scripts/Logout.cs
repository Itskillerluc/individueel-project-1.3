using UnityEngine;
using UnityEngine.SceneManagement;

public class Logout : MonoBehaviour
{
    public async void LogoutUser()
    {
        UserSingleton.Instance.AccessToken = null;
        UserSingleton.Instance.RefreshToken = null;
        UserSingleton.Instance.ExpiresIn = 0;
        UserSingleton.Instance.Name = null;
        
        await SceneManager.LoadSceneAsync("Scenes/LoginScreen");
        Debug.Log("User logged out!");
    }
}