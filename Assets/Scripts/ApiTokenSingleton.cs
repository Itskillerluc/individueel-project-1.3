﻿using UnityEngine;

public class ApiTokenSingleton : MonoBehaviour
{
    public static ApiTokenSingleton Instance { get; private set; }
    
    public string Token { get; set; }

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