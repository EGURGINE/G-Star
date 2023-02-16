using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
public class ReaderBoards : MonoBehaviour
{
    private void Awake()
    {
        PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder().Build());
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    public void DoLogin()
    {
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool isSuccess) =>
            {
                if (isSuccess)
                {
                    Debug.Log("dlswmd tjdrhd -> " + Social.localUser.userName);

                    DoShowLeaderBoard();
                }
                else
                {
                    Debug.Log("인증 실패");
                }
            });
        }

    }
    
    public void DoShowLeaderBoard()
    {
        Social.ReportScore(GameManager.Instance.ClearCount, GPGSIds.leaderboard_clearboards, (bool isSuccess) => { });

        Social.ShowLeaderboardUI();
    }
}
