using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    private float score;
    [SerializeField] private Text scoreTxt;
    private float highScore;
    [SerializeField] private Text highScoreTxt;
    private int money;
    [SerializeField] private Text moneyTxt;
    private void Awake()
    {
        Instance = this;
        StartSet();
    }
    public void SetMoney(int _Money)
    {
        Debug.Log(_Money);
        money += _Money;
        moneyTxt.text = money.ToString();
    }
    public void SetScore(int _score)
    {
        score += _score;
        scoreTxt.text = score.ToString();
    }
    void StartSet()
    {
        score = 0;
        scoreTxt.text = score.ToString();
        highScore = PlayerPrefs.GetFloat("HighScore");
        highScoreTxt.text = highScore.ToString();
        money = PlayerPrefs.GetInt("Money");
        moneyTxt.text = money.ToString();
    }
    public void SetDie()
    {
        if(score> PlayerPrefs.GetFloat("HighScore"))
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
            highScoreTxt.text = highScore.ToString();
        }
        PlayerPrefs.SetInt("Money", money);
    }
}
