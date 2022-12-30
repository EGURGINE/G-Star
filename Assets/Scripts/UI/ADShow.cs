using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class ADShow : MonoBehaviour
{
    private void ShowReward()
    {
        if (Advertisement.IsReady("Rewarded Android"))
        {
            ShowOptions options = new ShowOptions { resultCallback = ResultAds };
            Advertisement.Show("Rewarded Android", options);
        }
    }

    private void ResultAds(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Failed:
                break;
            case ShowResult.Skipped:
                break;
            case ShowResult.Finished:
                GameManager.Instance.Money += 2000;
                break;
            default:
                break;
        }
    }
}
