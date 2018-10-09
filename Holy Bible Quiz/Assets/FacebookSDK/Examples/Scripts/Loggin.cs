using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using System;
using UnityEngine.UI;


public class Loggin : MonoBehaviour {  

	public GameObject DialogLoggedIn;
	public GameObject DialogLoggedOut;
	public GameObject DialogUsername;
	public GameObject DialogProfilePic;
	public Text UserNameFr;

	public GameObject ScoreEntryPanel;
	public GameObject ScrollScoreList;

    // Use this for initialization
    void Awake () {

		FacebookManager.Instance.InitFB ();
		DealWithFBMenus (FB.IsLoggedIn);
    }
    private void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
			FacebookManager.Instance.IsLoggedIn = true;
			FacebookManager.Instance.GetProfile ();
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            // Print current access token's User ID
			Debug.Log("Facebook Logged In Succesfully!");
            Debug.Log(aToken.UserId);
            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }
        }
        else
        {
            Debug.Log("User cancelled login");
        }
		DealWithFBMenus (FB.IsLoggedIn);
    }
    public void LoginButton()
    {
		List<string> perms = new List<string>() { "public_profile", "email", "user_friends" };
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }
    
    
	void DealWithFBMenus(bool isLoggedIn)
	{
		if (isLoggedIn) {
			DialogLoggedIn.SetActive (true);
			DialogLoggedOut.SetActive (false);

			if (FacebookManager.Instance.ProfileName != null) {
				Text UserName = DialogUsername.GetComponent<Text> ();
				UserName.text = " " + FacebookManager.Instance.ProfileName;
			} else {
				StartCoroutine ("WaitForProfileName");
			}

			if (FacebookManager.Instance.ProfilePic != null) {
				Image ProfilePic = DialogProfilePic.GetComponent<Image> ();
				ProfilePic.sprite = FacebookManager.Instance.ProfilePic;
			} else {
				StartCoroutine ("WaitForProfilePic");
			}

		} else {
			DialogLoggedIn.SetActive (false);
			DialogLoggedOut.SetActive (true);
		}
	}

	IEnumerator WaitForProfileName()
	{
		while (FacebookManager.Instance.ProfileName == null) {
			yield return null;
		}
		DealWithFBMenus (FB.IsLoggedIn);
	}

	IEnumerator WaitForProfilePic()
	{
		while (FacebookManager.Instance.ProfilePic == null) {
			yield return null;
		}
		DealWithFBMenus (FB.IsLoggedIn);
	}
		

	public void QueryScore()
	{
		FB.API ("/app/scores?fields=score,user.limit(30)", HttpMethod.GET, getScoreCallBack);
	}

	void getScoreCallBack(IResult result)
	{
		IDictionary<string, object> data = result.ResultDictionary;
		List<object> scoreList = (List<object>) data["data"];

		foreach (object obj in scoreList) 
		{
			var entry = (Dictionary<string, object>)obj;
			var user = (Dictionary<string, object>)entry ["user"];

			Debug.Log (user ["name"].ToString () + " , " + entry ["score"].ToString ());

			GameObject scorePanel;
			scorePanel = Instantiate (ScoreEntryPanel) as GameObject;
			scorePanel.transform.SetParent (ScrollScoreList.transform, false);

			Transform FName = scorePanel.transform.Find ("FriendName");
			Transform FScore = scorePanel.transform.Find ("FriendScore");
			Transform FAvatar = scorePanel.transform.Find ("FriendAvatar");

			Text FnameText = FName.GetComponent<Text> ();
			Text FscoreText = FScore.GetComponent<Text> ();
			Image FUserAvatar = FAvatar.GetComponent<Image> ();

			FnameText.text = user ["name"].ToString ();
			//FscoreText.text = entry ["score"].ToString ();
			FscoreText.text = ScoreManager.score.ToString ();

			FB.API (user ["id"].ToString () + "/picture?width=120&height=120", HttpMethod.GET, delegate(IGraphResult picResult) {
				if (picResult.Error != null) {
					Debug.Log (picResult.RawResult);
				} else {
					FUserAvatar.sprite = Sprite.Create (picResult.Texture, new Rect (0, 0, 120, 120), new Vector2 (0, 0));
				}
			});
			// scoreDebug.text = result.RawResult;
		}
	}

	public void Share()
	{
		FacebookManager.Instance.ShareLink ();
	}

	public void Invite()
	{
		FacebookManager.Instance.Invite ();
	}

	public void SetScore()
	{
		var scoreData = new Dictionary<string, string> ();
		scoreData ["score"] = ScoreManager.score.ToString ();

		FB.API ("/me/scores", HttpMethod.POST, delegate(IGraphResult result) {
			Debug.Log("Score Submitted Successfully!" + result.RawResult);
			}, scoreData);
	}
}
