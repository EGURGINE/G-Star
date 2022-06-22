using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    [Header("�÷��̾�")]
    [SerializeField] private GameObject player;
    [SerializeField] private ParticleSystem playerSpawnPc;
    [SerializeField] public int playerSpd;
    [SerializeField] private ShotArea shotArea;

    [Header("����")]
    private int score;
    [SerializeField] private Text scoreTxt;
    private float highScore;
    [SerializeField] private Text highScoreTxt;

    [Header("��")]
    private int money;
    [SerializeField] private Text moneyTxt;

    [Header("���ӿ���")]
    [SerializeField] private GameObject gameOverWnd;
    public bool isGameOver = false;
    
    [Header("���̽�ƽ")]
    public GameObject joystick;
    public RectTransform touchArea; 
    public Image outerPad; 
    public Image innerPad;

    [Header("����")]
    [SerializeField] private Text lv;
    private int level;
    [SerializeField] private Image expSlider;
    private float exp;
    public float maxExp;

    [Header("���׷��̵�")]
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

        //���� ���۽� �ְ����� ��������
        highScore = PlayerPrefs.GetFloat("HighScore");
        highScoreTxt.text = highScore.ToString();
       
        // ���� ���۽� ������ �� ��������
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
        //����ġ �ʱ�ȭ
        level = 1;
        lv.text = level.ToString();
        exp = 0;
        maxExp = 10;
        expSlider.fillAmount = exp;

        //���� �ʱ�ȭ
        score = 0;
        scoreTxt.text = score.ToString();
        highScore = PlayerPrefs.GetFloat("HighScore");
        highScoreTxt.text = highScore.ToString();
     

        //�÷��̾� ����
        Instantiate(playerSpawnPc).transform.position = Vector3.zero;
        player.SetActive(true);
        player.transform.position = Vector3.zero;

        // ���� �ʱ�ȭ
        shotArea.ResetState();
    }
    public void SetDie()
    {
        isGameOver = true;
        joystick.SetActive(false);

        //�ְ����� ���
        if (score> PlayerPrefs.GetFloat("HighScore"))
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
            highScoreTxt.text = highScore.ToString();
        }

        gameOverWnd.SetActive(true);
        
    }
}
