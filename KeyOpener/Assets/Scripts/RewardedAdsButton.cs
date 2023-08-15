using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
using UnityEngine.Playables;
public class RewardedAdsButton : MonoBehaviour
{
    public RewardedAd rewardedAds;
    public string _adUnitId;

    public ChestManager tresure;
    public LocalCSVLoader nextLevel;

    private ChangeValueAnimator changeStar;
    private againShowHide againPanel;

    public PlayableDirector stars;
    public GameObject moreCoinsOBJ;
    //public UIScript uiScript;
    // Start is called before the first frame update
    void Start()
    {
        //MobileAds.Initialize((InitializationStatus initStatus) =>
        //{
        //    LoadRewardedAd();
        //});
    }

    public void LoadAfter()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            LoadRewardedAd();
        });
    }

    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (rewardedAds != null)
        {
            rewardedAds.Destroy();
            rewardedAds = null;
            tresure.RewardButton.SetActive(true);
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest.Builder().Build();

        // send the request to load the ad.
        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error.GetMessage());
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAds = ad;
                RegisterEventHandlers(rewardedAds);
            });
    }


    public void ShowRewardedAd()
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedAds != null && rewardedAds.CanShowAd())
        {
            rewardedAds.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                //uiScript.OnUserEarnedReward();
                tresure.RewardChest();
            });
        }
    }

    public void ShowNextLevelRewarddAd()
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedAds != null && rewardedAds.CanShowAd())
        {
            rewardedAds.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                //uiScript.OnUserEarnedReward();
                againPanel = GameObject.FindObjectOfType<againShowHide>();
                againPanel.Hide();
                nextLevel.CreateNextPrefab();

                PlayerPrefs.SetInt("exp", PlayerPrefs.GetInt("exp") + 1);
            });
        }
    }

    public void ShowMoreCoinRewarddAd()
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedAds != null && rewardedAds.CanShowAd())
        {
            rewardedAds.Show((Reward reward) =>
            {
                changeStar = GameObject.FindObjectOfType<ChangeValueAnimator>();
                changeStar.ChangeValueUp(100);

                stars.Play();
                moreCoinsOBJ.SetActive(false);
            });
        }
    }


    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen content closed.");
            LoadRewardedAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
            LoadRewardedAd();
        };
    }

    // Update is called once per frame
    void Update()
    {

    }
}