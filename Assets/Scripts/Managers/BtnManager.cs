using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class BtnManager : MonoBehaviour
{
    [Header("Ã¢µé")]
    [SerializeField] private GameObject menuWnd;
    [Space(10f)]
    [SerializeField] private GameObject[] wnds; 
    [Space(10f)]
    [SerializeField] private GameObject gameWnd;
    [SerializeField] private GameObject gameOverWnd;
    [SerializeField] private Setting set;
    public void ReTryBtn()
    {
        SoundManager.Instance.PlaySound(ESoundSources.BUTTON);

        if (set.isSetting == true)
        {
            set.IngameWndBtn();
            GameManager.Instance.isGameOver = true;
            GameManager.Instance.player.gameObject.SetActive(false);
            BasicEnemy[] obj = FindObjectsOfType<BasicEnemy>();
            foreach (var item in obj)
            {
                item.PushObj();
            }
        }
        gameOverWnd.SetActive(false);

        if (GameManager.Instance.isTutorial)
        {
            Tutorial();
        }
        else
        GameManager.Instance.StartSet();
    }
    public void MenuBtn()
    {
        SoundManager.Instance.PlaySound(ESoundSources.BUTTON);

        if (set.isSetting == true)
        {
            set.IngameWndBtn();
            GameManager.Instance.isGameOver = true;
            GameManager.Instance.player.gameObject.SetActive(false);

        }
        GameManager.Instance.tutorialWnd.SetActive(false);
        GameManager.Instance.isGameOver = true;
        GameManager.Instance.isTutorial = false;
        CameraSetting.Instance.MainPost();
        gameOverWnd.SetActive(false);
        GameManager.Instance.upSelect.ChoiceReset();
        GameManager.Instance.upSelect.gameObject.SetActive(false);
        GameManager.Instance.observerManager.NotifyObservers();
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
        if (num == 1) GameManager.Instance.player.SC.SkinDisplay();
    }
}
