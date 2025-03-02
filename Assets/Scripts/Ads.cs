using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ads : MonoBehaviour
{
    static Ads instance;

    public static Ads Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Ads>();

                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(Ads).Name;
                    instance = obj.AddComponent<Ads>();
                    DontDestroyOnLoad(obj);
                }
            }

            return instance;
        }
    }

    void Start()
    {
        Gley.MobileAds.API.Initialize(OnInitialized);
    }

    private void OnInitialized()
    {
        //Show ads only after this method is called
        //This callback is not mandatory if you do not want to show banners as soon as you app starts.
    }

    public void ShowInterstitial()
    {
        Gley.MobileAds.API.ShowInterstitial();
    }

    public void ShowRewardedVideo()
    {
        Gley.MobileAds.API.ShowRewardedVideo(CompleteMethod);
    }

    private void CompleteMethod(bool completed)
    {
        // skip level
        GameManager.Instance.SkipLevel();
    }
}
