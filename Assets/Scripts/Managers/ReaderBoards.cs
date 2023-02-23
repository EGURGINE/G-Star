using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System.Threading.Tasks;
public class ReaderBoards : MonoBehaviour
{
    private void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        // 로그인
        Social.localUser.Authenticate((bool success) => 
        {
            if (success)
            {
                Debug.Log("로그인 성공");
            }
            else
            {
                Debug.Log("로그인 실패");
            }
        });
    }
    public void DoLogin()
    {
        if (Social.localUser.authenticated)
        {
            DoShowReaderBoards();
        }
        else
        {
            Social.localUser.Authenticate((bool isSucess) =>
            {
                if (isSucess)
                {
                    Debug.Log("로그인 성공");
                    DoShowReaderBoards();

                }
                else
                {
                    Debug.Log("로그인 실패");
                }
            });
        }

    }

    private void DoShowReaderBoards()
    {
        Social.ReportScore(((int)GameManager.Instance.highScore), GPGSIds.leaderboard_clearboards, (bool sucess) =>
        {
            if (sucess)
            {
                print("성공");
            }
            else
            {
                print("실패");
            }
        });

        Social.ShowLeaderboardUI();
    }

}
