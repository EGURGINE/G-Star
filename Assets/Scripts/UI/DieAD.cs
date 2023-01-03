using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using Sirenix.OdinInspector.Editor.Drawers;

public class DieAD : MonoBehaviour
{
    private InterstitialAd interstitial;

    private void Start()
    {
        ShowAD();
    }

    private void ShowAD()
    {
        string adUnitld = "ca-app-pub-5525985757997085/8852576638";

        this.interstitial = new InterstitialAd(adUnitld);

        this.interstitial.OnAdClosed += HandleOnAdClosed;

        AdRequest request = new AdRequest.Builder().Build();

        this.interstitial.LoadAd(request);

    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        //reGame

        //GameManager.Instance.btnGM.ReTryBtn();
        ShowAD();
    }
    
    public void ADCheck()
    {
        int num = UnityEngine.Random.Range(0, 3);

        if (num == 0)
        {
            print("ShowAD");
            if (this.interstitial.IsLoaded())
            {
                this.interstitial.Show();
            }

        }
    }

}
