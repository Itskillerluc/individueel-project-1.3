using Models;
using UnityEngine;

public class RoomSingleton : MonoBehaviour
{
    public static RoomSingleton Instance { get; private set; }
    
    public GetRoomsResponseDto Room { get; set; }
    public bool CanEdit { get; set; }

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