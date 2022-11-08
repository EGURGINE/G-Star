using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnManager : MonoBehaviour
{
    [Header("창들")]
    [SerializeField] private GameObject menuWnd;
    [Space(10f)]
    [SerializeField] private GameObject[] wnds; 
    [Space(10f)]
    [SerializeField] private GameObject gameWnd;
    [SerializeField] private GameObject gameOverWnd;
    public void ReTryBtn()
    {
        SoundManager.Instance.PlaySound(ESoundSources.BUTTON);

        gameOverWnd.SetActive(false);
        GameManager.Instance.StartSet();
    }
    public void MenuBtn()
    {
        SoundManager.Instance.PlaySound(ESoundSources.BUTTON);

        CameraSetting.Instance.MainPost();
        gameOverWnd.SetActive(false);
        gameWnd.SetActive(false);
        menuWnd.SetActive(true);
    }
    public void StartBtn()
    {
        SoundManager.Instance.PlaySound(ESoundSources.BUTTON);

        menuWnd.SetActive(false);
        gameWnd.SetActive(true);
        GameManager.Instance.StartSet();
    }
    public void Tutorial()
    {
        SoundManager.Instance.PlaySound(ESoundSources.BUTTON);


        menuWnd.SetActive(false);
        gameWnd.SetActive(true);
        GameManager.Instance.tutorialWnd.SetActive(true);
        GameManager.Instance.tutorialNum = -1;
        GameManager.Instance.isTutorial = true;
        GameManager.Instance.Tutorial();
    }
    public void TutorialNext()
    {
        SoundManager.Instance.PlaySound(ESoundSources.BUTTON);

        GameManager.Instance.Tutorial();
    }
    public void VideoBtn()
    {
        SoundManager.Instance.PlaySound(ESoundSources.BUTTON);

        // 광고 넣기
        GameManager.Instance.Money += 2000;
    }
     
    public void WndBtnSet(int num)
    {
        SoundManager.Instance.PlaySound(ESoundSources.BUTTON);

        foreach (var item in wnds)
        {
            if (item == wnds[num])
            {
                item.SetActive(true);
            }
            else item.SetActive(false);
        }
    }
}
