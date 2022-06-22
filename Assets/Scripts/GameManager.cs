using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    [Header("플레이어")]
    [SerializeField] private GameObject player;
    [SerializeField] private ParticleSystem playerSpawnPc;
    [SerializeField] public int playerSpd;
    [SerializeField] private ShotArea shotArea;

    [Header("점수")]
    private int score;
    [SerializeField] private Text scoreTxt;
    private float highScore;
    [SerializeField] private Text highScoreTxt;

    [Header("돈")]
    private int money;
    [SerializeField] private Text moneyTxt;

    [Header("게임오버")]
    [SerializeField] private GameObject gameOverWnd;
    public bool isGameOver = false;
    
    [Header("조이스틱")]
    public GameObject joystick;
    public RectTransform touchArea; 
    public Image outerPad; 
    public Image innerPad;

    [Header("레벨")]
    [SerializeField] private Text lv;
    private int level;
    [SerializeField] private Image expSlider;
    private float exp;
    public float maxExp;

    [Header("업그레이드")]
    [SerializeField] private GameObject upgradeWnd;
    public bool isUpgrade =false;
    public float Exp
    {
        get { return exp; }
        set 
        {
            exp += value;
            if (exp>=maxExp)
            {
                exp -= maxExp;
                maxExp += 10;
                level++;
                lv.text = level.ToString();
                expSlider.fillAmount = exp / maxExp;
                isUpgrade = true;
                player.SetActive(false);
                upgradeWnd.SetActive(true);
                joystick.SetActive(false);
                player.transform.position = Vector3.zero;
            }
            expSlider.fillAmount = exp / maxExp;
        }
    }
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

        //게임 시작시 최고점수 가져오기
        highScore = PlayerPrefs.GetFloat("HighScore");
        highScoreTxt.text = highScore.ToString();
       
        // 게임 시작시 저장한 돈 가져오기
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
        joystick.SetActive(true);
        isGameOver = false;
        //경험치 초기화
        level = 1;
        lv.text = level.ToString();
        exp = 0;
        maxExp = 10;
        expSlider.fillAmount = exp;

        //점수 초기화
        score = 0;
        scoreTxt.text = score.ToString();
        highScore = PlayerPrefs.GetFloat("HighScore");
        highScoreTxt.text = highScore.ToString();
     

        //플레이어 스폰
        Instantiate(playerSpawnPc).transform.position = Vector3.zero;
        player.SetActive(true);
        player.transform.position = Vector3.zero;

        // 스텟 초기화
        shotArea.ResetState();
    }
    public void SetDie()
    {
        isGameOver = true;
        joystick.SetActive(false);

        //최고점수 기록
        if (score> PlayerPrefs.GetFloat("HighScore"))
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
            highScoreTxt.text = highScore.ToString();
        }

        gameOverWnd.SetActive(true);
        
    }
}
