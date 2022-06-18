using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    [SerializeField] private GameObject player;

    private int score;
    [SerializeField] private Text scoreTxt;
    private float highScore;
    [SerializeField] private Text highScoreTxt;
    private int money;
    [SerializeField] private Text moneyTxt;
    [SerializeField] private GameObject gameOverWnd;
    public bool isGameOver = false;
    [SerializeField] GameObject joystick;
    public RectTransform touchArea; 
    public Image outerPad; 
    public Image innerPad;

    public int Money
    {
        get { return money; }
        set 
        {
            money += value;
            PlayerPrefs.SetInt("Money", money);
            moneyTxt.text = money.ToString();
        }
    }
    public int Score
    {
        get { return score; }
        set 
        {
            score += value;
            scoreTxt.text = score.ToString();

        }
    }
    private void Awake()
    {
        Instance = this;
        isGameOver = true;
        highScore = PlayerPrefs.GetFloat("HighScore");
        highScoreTxt.text = highScore.ToString();
        money = PlayerPrefs.GetInt("Money");
        moneyTxt.text = money.ToString();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchArea.transform.position = Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            innerPad.transform.position = Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            outerPad.transform.position = Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        }
    }
    public void StartSet()
    {
        isGameOver = false;
        score = 0;
        joystick.SetActive(true);
        scoreTxt.text = score.ToString();
        highScore = PlayerPrefs.GetFloat("HighScore");
        highScoreTxt.text = highScore.ToString();
        money = PlayerPrefs.GetInt("Money");
        moneyTxt.text = money.ToString();
        player.SetActive(true);
        player.transform.position = Vector3.zero;
    }
    public void SetDie()
    {
        isGameOver = true;
        joystick.SetActive(false);
        if (score> PlayerPrefs.GetFloat("HighScore"))
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
            highScoreTxt.text = highScore.ToString();
        }
        gameOverWnd.SetActive(true);
    }
}
