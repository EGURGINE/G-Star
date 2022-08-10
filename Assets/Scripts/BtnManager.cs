using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnManager : MonoBehaviour
{
    [SerializeField] private GameObject menuWnd;
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
        // ±¤°í ³Ö±â
        GameManager.Instance.Money = 2000;
    }
}
