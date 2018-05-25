using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Platform;

public class AppEntitlementCheck: MonoBehaviour {

	void Awake ()
	{
		try 
		{
			Core.AsyncInitialize("1464350177018247");
			Entitlements.IsUserEntitledToApplication().OnComplete(GetEntitlementCallback);
		} 
		catch(UnityException e) 
		{
			Debug.LogError("Platform failed to initialize due to exception.");
			Debug.LogException(e);
			// Immediately quit the application.
			UnityEngine.Application.Quit();
		}
	}

	void GetEntitlementCallback (Message msg) 
	{
		if (msg.IsError) 
		{
			Debug.LogError("You are NOT entitled to use this app.");
			UnityEngine.Application.Quit();
		} 
		else 
		{
			Debug.Log("You are entitled to use this app.");
		}
	}
}    