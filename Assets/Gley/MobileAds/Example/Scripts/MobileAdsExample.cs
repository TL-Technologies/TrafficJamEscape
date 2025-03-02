using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

 
namespace Gley.MobileAds.Internal
{


    public class MobileAdsExample : MonoBehaviour
    {
        public bool EnableAds , EnableBanner;
        
        public int ShowAdsEveryLevel=1;
        public Button Reward;
       // public Button rewardedButton;


        public UnityEvent OnShowInterstitialWin , OnShowRewardLose , OnShowInterstitialLose;

        /// <summary>
        /// Initialize the ads
        /// </summary>
        void Awake()
        {
            if (EnableAds)
            {
                Gley.MobileAds.API.Initialize(OnInitialized);
            }
        }
        int lvl;
        void Start()
        {
            if (EnableAds)
            {

           
            lvl = PlayerPrefs.GetInt("Level")+1;
                if(EnableBanner)
            ShowBanner();
            }
        }


        private void OnInitialized()
        {
            //Show ads only after this method is called
            //This callback is not mandatory if you do not want to show banners as soon as you app starts.

            if (EnableBanner  )
                ShowBanner();
             
        }

        void onShowInterstitialWin( )
        {

           
                OnShowInterstitialWin.Invoke();
            
        }

        void onShowInterstitialLose()
        {

           
                OnShowInterstitialLose.Invoke();
            
        }

        /// <summary>
        /// Show banner, assigned from inspector
        /// </summary>
        public void ShowBanner()
        {
            if (EnableAds)
            {
                Gley.MobileAds.API.ShowBanner(BannerPosition.Bottom, BannerType.Banner);
            }
        }

        /// <summary>
        /// Hide banner assigned from inspector
        /// </summary>
        public void HideBanner()
        {
            if (EnableAds)
            {
                Gley.MobileAds.API.HideBanner();
            }
        }


        /// <summary>
        /// Show Interstitial, assigned from inspector
        /// </summary>
        public void ShowInterstitial( bool Win)
        {
            if (EnableAds)
            {
                if (Win)
                {


                    if (Gley.MobileAds.API.IsInterstitialAvailable() && (lvl % ShowAdsEveryLevel) == 0)
                    {
                        Gley.MobileAds.API.ShowInterstitial(onShowInterstitialWin);// load levl witht showing ads
                    }
                    else
                    {
                        onShowInterstitialWin();// load levl without showing ads
                    }


                }
                else
                {
                    if (Gley.MobileAds.API.IsInterstitialAvailable() && (lvl % ShowAdsEveryLevel) == 0)
                    {
                        Gley.MobileAds.API.ShowInterstitial(onShowInterstitialLose);// load levl witht showing ads
                    }
                    else
                    {
                        onShowInterstitialLose();// load levl without showing ads
                    }


                }
            }
            else
            {
                if (Win)
                {
                    onShowInterstitialWin();// load levl without showing ads
                }
                else
                {
                    onShowInterstitialLose();// load levl without showing ads
                }
            }
        }

        /// <summary>
        /// Show rewarded video, assigned from inspector
        /// </summary>
        public void ShowRewardedVideo()
        {
            if (EnableAds)
            {
                Gley.MobileAds.API.ShowRewardedVideo(CompleteMethod);
            }
        }


        /// <summary>
        /// This is for testing purpose
        /// </summary>
        void Update()
        {
            if(Reward != null)
            {

            
                if (EnableAds )
                {
                    if (Gley.MobileAds.API.IsInterstitialAvailable())
                    {
                        //  intersttialButton.interactable = true;
                    }
                    else
                    {
                        // intersttialButton.interactable = false;
                    }

                    if (Gley.MobileAds.API.IsRewardedVideoAvailable())
                    {
                        Reward.interactable = true;
                    }
                    else
                    {
                        Reward.interactable = false;
                    }
                }
                else
                {
                    Reward.interactable = false;
                }
            }
        }

        /// <summary>
        /// Complete method called every time a rewarded video is closed
        /// </summary>
        /// <param name="completed">if true, the video was watched until the end</param>
        private void CompleteMethod(bool completed)
        {

            if (EnableAds)
            {
                OnShowRewardLose.Invoke();
            }
           
        }
    }
}
