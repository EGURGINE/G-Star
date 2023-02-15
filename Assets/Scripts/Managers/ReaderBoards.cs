using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
public class ReaderBoards : MonoBehaviour
{

    private void Awake()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }
    // Start is called before the first frame update
    void Start()
    {
        
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
                    Debug.Log("���� ����");
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
