using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;

public class FBScript : MonoBehaviour {

    [SerializeField]
    private GameObject DialogLoggedIn;
    [SerializeField]
    private GameObject DialogLoggedOut;
    [SerializeField]
    private GameObject username;
    [SerializeField]
    private GameObject profilepic;
    [SerializeField]
    private GameObject ShareBtn;

    void Awake()
    {
        FB.Init(SetInIt, OnHideUnity);
    }

    void SetInIt()
    {
        if(FB.IsLoggedIn)
        {
            Debug.Log("Fb is logged in");
        }
        else
        {
            Debug.Log("Fb is not logged in");
        }

        DealWithFBMenus(FB.IsLoggedIn);
    }

    void OnHideUnity(bool isGameShown)
    {
        if(!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void FBlogin()
    {
        List<string> permissions = new List<string>();
        permissions.Add("public_profile");

        FB.LogInWithReadPermissions(permissions, AuthCallBack);
    }

    void AuthCallBack(IResult result)
    {
        if (result.Error != null)
        {
            Debug.Log(result.Error);
        }
        else
        {
            if (FB.IsLoggedIn)
            {
                Debug.Log("Fb is logged in");
            }
            else
            {
                Debug.Log("Fb is not logged in");
            }

            DealWithFBMenus(FB.IsLoggedIn);
        }
    }

    void DealWithFBMenus(bool isLoggedIn)
    {
        if(isLoggedIn)
        {
            DialogLoggedIn.SetActive(true);
            DialogLoggedOut.SetActive(false);
            ShareBtn.SetActive(true);
            FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
            FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
        }
        else
        {
            DialogLoggedIn.SetActive(false);
            DialogLoggedOut.SetActive(true);
            ShareBtn.SetActive(false);
        }
    }

    void DisplayUsername(IResult result)
    {
        Text Username = username.GetComponent<Text>();

        if(result.Error == null)
        {
            Username.text = "Welcome, " + result.ResultDictionary["first_name"];
        }
        else
        {
            Debug.Log(result.Error);
        }
    }

    void DisplayProfilePic(IGraphResult result)
    {
        if(result.Texture != null)
        {
            Image ProfilePic = profilepic.GetComponent<Image>();

            ProfilePic.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
        }
        else
        {
            Debug.Log(result.Error);
        }
    }

    public void Share()
    {
        FB.ShareLink(
            contentTitle:"2D Tower Defense message", 
            contentURL:new System.Uri("https://docs.google.com/presentation/d/1jkn79BBD23QTVsb3dXFkQS63W9iDalVSrcR5VvWnhSU/edit#slide=id.g208d8a1b23_0_30"),
            contentDescription:"Here's a link to the game video",
            callback:OnShare);
    }
    private void OnShare(IShareResult result)
    {
        if(result.Cancelled || !string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("ShareLink error: " + result.Error); 
        }
        else if(!string.IsNullOrEmpty(result.PostId))
        {
            Debug.Log(result.PostId);
        }
        else
        {
            Debug.Log("Share success!");
        }
    }
}
