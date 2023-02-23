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

        // �α���
        Social.localUser.Authenticate((bool success) => 
        {
            if (success)
            {
                Debug.Log("�α��� ����");
            }
            else
            {
                Debug.Log("�α��� ����");
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
                    Debug.Log("�α��� ����");
                    DoShowReaderBoards();

                }
                else
                {
                    Debug.Log("�α��� ����");
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
                print("����");
            }
            else
            {
                print("����");
            }
        });

        Social.ShowLeaderboardUI();
    }

}
