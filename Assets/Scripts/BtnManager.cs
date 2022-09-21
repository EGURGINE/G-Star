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
        gameOverWnd.SetActive(false);
        GameManager.Instance.StartSet();
    }
    public void MenuBtn()
    {
        gameOverWnd.SetActive(false);
        gameWnd.SetActive(false);
        menuWnd.SetActive(true);
        GameManager.Instance.joystick.SetActive(false);
    }
    public void StartBtn()
    {
        menuWnd.SetActive(false);
        gameWnd.SetActive(true);
        GameManager.Instance.StartSet();
    }
    public void Tutorial()
    {
        menuWnd.SetActive(false);
        gameWnd.SetActive(true);
        GameManager.Instance.tutorialWnd.SetActive(true);
        GameManager.Instance.tutorialNum = -1;
        GameManager.Instance.isTutorial = true;
        GameManager.Instance.Tutorial();
    }
    public void TutorialNext()
    {
        GameManager.Instance.Tutorial();
    }
    public void VideoBtn()
    {
        // 광고 넣기
        GameManager.Instance.Money += 2000;
    }
     
    public void WndBtnSet(int num)
    {
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
