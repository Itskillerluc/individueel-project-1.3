using System.Text.RegularExpressions;
using Models;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApiClientLogin : MonoBehaviour
{
    public TextMeshProUGUI statusMessage;
    public TMP_InputField email;
    public TMP_InputField password;

    //todo error when there is no connection
    
    public async void Register()
    {
        statusMessage.text = "Loading...";

        if (email.text.ToLower() == "" || password.text == "")
        {
            statusMessage.text = "Fill in both email and password.";
            return;
        }

        if (!Regex.IsMatch(email.text.ToLower(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            statusMessage.text = "Email must be a valid email address.";
            return;
        }

        if (!(password.text.Length >= 10) &&
            !Regex.IsMatch(password.text, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).+$"))
        {
            statusMessage.text =
                "Password must be at least 10 characters long and contain at least one uppercase letter, one lowercase letter, one number and one special character.";
            return;
        }

        var dto = new PostRegisterRequestDto { email = email.text.ToLower(), password = password.text };
        //todo
        // var response = await ApiUtil.PerformApiCall("https://avansict2226538.azurewebsites.net/account/register",
        //     "Post", JsonUtility.ToJson(dto));
        
        var response = await ApiUtil.PerformApiCall("https://localhost:7244/account/register",
            "Post", JsonConvert.SerializeObject(dto));

        if (response == "HTTP/1.1 400 Bad Request")
        {
            statusMessage.text = "User already exists";
        }
    }

    public async void Login()
    {
        statusMessage.text = "Loading...";

        if (email.text == "" || password.text == "")
        {
            statusMessage.text = "Fill in both email and password.";
            return;
        }

        var dto = new PostLoginRequestDto { email = email.text, password = password.text };
        //todo
        // var response = await ApiUtil.PerformApiCall("https://avansict2226538.azurewebsites.net/account/login", "Post",
        //     JsonUtility.ToJson(dto));
        
        var response = await ApiUtil.PerformApiCall("https://localhost:7244/account/login", "Post",
            JsonConvert.SerializeObject(dto));

        if (response == "HTTP/1.1 401 Unauthorized")
        {
            statusMessage.text = "Wrong email or password";
            Debug.Log("Login failed.");
            return;
        }

        if (response == "HTTP/1.1 500 Internal Server Error")
        {
            statusMessage.text = "Something went wrong :( Please try again.";
            Debug.Log("Server Error");
            return;
        }


        var postLoginResponseDto = JsonConvert.DeserializeObject<PostLoginResponseDto>(response);


        UserSingleton.Instance.Token = postLoginResponseDto.accessToken;
        UserSingleton.Instance.Name = email.text.ToLower();

        await SceneManager.LoadSceneAsync("Scenes/RoomChoice");
    }
}