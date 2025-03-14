using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApiClientLoginBehaviour : MonoBehaviour
{
    public TextMeshProUGUIWrapper statusMessage;
    public TMPTextWrapper email;
    public TMPTextWrapper password;
    
    private ApiClientLogin _apiClientLogin;
    private ApiUtil _apiUtil;

    private void Awake()
    {
        _apiClientLogin = new ApiClientLogin();
        _apiUtil = new ApiUtil();
    }

    public async void Register()
    {
        await _apiClientLogin.Register(statusMessage, email, password, _apiUtil);
    }
    
    public async void Login()
    {
        if (await _apiClientLogin.Login(statusMessage, email, password, _apiUtil, UserSingleton.Instance))
        {
            await SceneManager.LoadSceneAsync("Scenes/RoomChoice");
        }
    }
}

public class ApiClientLogin
{
    public async Task Register(IText statusMessage, IText email, IText password, IApiUtil apiUtil)
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
        
        var response = await apiUtil.PerformApiCall("https://avansict2226538.azurewebsites.net/account/register",
            "Post", JsonConvert.SerializeObject(dto));

        if (response == "HTTP/1.1 400 Bad Request")
        {
            statusMessage.text = "User already exists";
            return;
        }

        if (response == "Cannot connect to destination host")
        {
            statusMessage.text = "Cannot connect to server";
            return;
        }
        
        statusMessage.text = "Registered successfully!";
    }

    public async Task<bool> Login(IText statusMessage, IText email, IText password, IApiUtil apiUtil, IUserSingleton userSingleton)
    {
        statusMessage.text = "Loading...";

        if (email.text.ToLower() == "" || password.text == "")
        {
            statusMessage.text = "Fill in both email and password.";
            return false;
        }

        var dto = new PostLoginRequestDto { email = email.text.ToLower(), password = password.text };
        
        var response = await apiUtil.PerformApiCall("https://avansict2226538.azurewebsites.net/account/login", "Post",
            JsonConvert.SerializeObject(dto));

        if (response == "HTTP/1.1 401 Unauthorized")
        {
            statusMessage.text = "Wrong email or password";
            Debug.Log("Login failed.");
            return false;
        }

        if (response == "HTTP/1.1 500 Internal Server Error")
        {
            statusMessage.text = "Something went wrong :( Please try again.";
            Debug.Log("Server Error");
            return false;
        }

        if (response == "Cannot connect to destination host")
        {
            statusMessage.text = "Cannot connect to server";
            return false;
        }

        var postLoginResponseDto = JsonConvert.DeserializeObject<PostLoginResponseDto>(response);


        userSingleton.AccessToken = postLoginResponseDto.accessToken;
        userSingleton.Name = email.text.ToLower();
        userSingleton.ExpiresIn = postLoginResponseDto.expiresIn;
        userSingleton.RefreshToken = postLoginResponseDto.refreshToken;
        userSingleton.Updated();
        return true;
    }
}