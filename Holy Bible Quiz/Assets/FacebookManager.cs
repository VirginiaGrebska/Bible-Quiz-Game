using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using System;
using UnityEngine.UI;

//Този клас управлява Facebook SDK с всички нужни методи към него, като Share, Invite, както и за влизане 
//в профила на дадения потребител и изобразяване на профилната му снимка
public class FacebookManager : MonoBehaviour {

	private static FacebookManager _instance;

	public static FacebookManager Instance
	{
		get{ 
			if (_instance == null) {
				GameObject fbm = new GameObject ("FBManager");
				fbm.AddComponent<FacebookManager> ();
			}

			return _instance;
		}
	}

	public bool IsLoggedIn { get; set; }
	public string ProfileName { get; set;}
	public Sprite ProfilePic { get; set;}
	public string AppLinkURL { get; set; }

	void Awake()
	{
		DontDestroyOnLoad (this.gameObject);
		_instance = this;

		IsLoggedIn = true;
	}

	public void InitFB()
	{
		if (!FB.IsInitialized)
		{
			// Initialize the Facebook SDK
			FB.Init(InitCallback, OnHideUnity);

		}
		else
		{
			// Already initialized, signal an app activation App Event
			IsLoggedIn = FB.IsLoggedIn;
			FB.ActivateApp();
		}
	}

	private void InitCallback()
	{
		if (FB.IsInitialized)
		{
			// Signal an app activation App Event
			FB.ActivateApp();
			// Continue with Facebook SDK
			// ...
			GetProfile();
		}
		else
		{
			Debug.Log("Failed to Initialize the Facebook SDK");
		}
		IsLoggedIn = FB.IsLoggedIn;
	}

	private void OnHideUnity(bool isGameShown)
	{
		if (!isGameShown)
		{
			// Pause the game - we will need to hide
			Time.timeScale = 0;
		}
		else
		{
			// Resume the game - we're getting focus again
			Time.timeScale = 1;
		}
	}

	public void GetProfile()
	{
		FB.API ("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
		FB.API ("/me/picture?type=square&height=100&width=100", HttpMethod.GET, DisplayProfilePicture);
	}

	void DisplayUsername(IResult result)
	{

		if (result.Error == null) {
			ProfileName =" " + result.ResultDictionary ["first_name"];
		} else {
			Debug.Log (result.Error);
		}
	}

	void DisplayProfilePicture(IGraphResult result)
	{
		if (result.Texture != null) {
			ProfilePic = Sprite.Create (result.Texture, new Rect (0, 0, 100, 100), new Vector2 ());
		} 
	}
	void DealWithAppLink(IAppLinkResult result)
	{
		if (!String.IsNullOrEmpty (result.Url)) {
			AppLinkURL = result.Url;
		}
	}
	private void ShareCallback(IShareResult result)
	{
		if (result.Cancelled || !string.IsNullOrEmpty(result.Error))
		{
			Debug.Log("ShareLink Error: " + result.Error);
		}
		else if (!string.IsNullOrEmpty(result.PostId))
		{
			// Print post identifier of the shared content
			Debug.Log(result.PostId);
		}
		else
		{
			// Share succeeded without postID
			Debug.Log("ShareLink success!");
		}
	}

	public void ShareLink()
	{
		FB.ShareLink(new Uri("https://developers.facebook.com/"), callback: ShareCallback);
	}


	public void Share()
	{
		FB.FeedShare (
			string.Empty,
			new Uri(AppLinkURL),
			"Hello this is the title",
			"This is the caption",
			"Check out this game",
			new Uri("https://developers.facebook.com/"),
			string.Empty,
			ShareCallback
		);
	}

	public void Invite()
	{
		FB.Mobile.AppInvite(new Uri("https://developers.facebook.com/"), callback:InviteCallback);
	}
	void InviteCallback(IResult result)
	{
		if (result.Cancelled) {
			Debug.Log ("Invite Cancelled");
		} else if (!string.IsNullOrEmpty (result.Error)) {
			Debug.Log ("Error on invite!");
		} else if (!string.IsNullOrEmpty (result.RawResult)) {
			Debug.Log ("Success on Invite!");
		}
	}

}
