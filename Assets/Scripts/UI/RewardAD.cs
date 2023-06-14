using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
public class RewardAD : MonoBehaviour
{
    private RewardedAd rewardAD;


    [SerializeField] private bool isReviveBtn;

    private void Start()
    {
        MobileAds.Initialize(initStatus => { });

        RequestRewardedAd();
    }

    private void RequestRewardedAd()
    {
        string adUnitld = "ca-app-pub-5708876822263347/5752830069";

        this.rewardAD = new RewardedAd(adUnitld);

        // Called when the ad is closed.
        this.rewardAD.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardAD.LoadAd(request);
    }


    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        print("HandleRewardedAdClosed event received");
        if (isReviveBtn == true) GameManager.Instance.reviveWnd.Revive(); //revive
        else GameManager.Instance.Money += 2000;
        RequestRewardedAd();
    }

    public void ShowRewardedAd()
    {
        if (this.rewardAD.IsLoaded())
        {
            this.rewardAD.Show();
        }
        else
        {
            Debug.Log("NOT Loaded Interstitial");
            RequestRewardedAd();
        }
    }

}
