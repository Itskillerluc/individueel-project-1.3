using UnityEngine;

public class UserSingleton : MonoBehaviour, IUserSingleton
{
    public static UserSingleton Instance { get; private set; }
    
    public string Token { get; set; }
    public string Name { get; set; }

    private void Awake()
	{
		// Destroy this object if we already have a singleton configured
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(this);
	}
}

public interface IUserSingleton
{
	public string Token { get; set; }
	public string Name { get; set; }
}