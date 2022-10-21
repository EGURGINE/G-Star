using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    [Header("�÷��̾�")]
    public Player player;


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
    public bool isUpgrade = false;

    [Header("Ʃ�丮��")]
    public bool isTutorial;
    private bool isLevelupTrue;
    public GameObject tutorialWnd;
    [SerializeField] private TextMeshProUGUI tutorialTxt;
    public int tutorialNum;
    public Button tutorialNextBtn;
    [SerializeField] GameObject btnManager;
    public float Exp
    {
        get { return exp; }
        set
        {
            if (isTutorial && isLevelupTrue == false) return;
            exp = value;
            if (exp >= maxExp)
            {
                SoundManager.Instance.PlaySound(ESoundSources.LEVEL);
                exp -= maxExp;
                maxExp += 10;
                level++;
                lv.text = level.ToString();
                expSlider.fillAmount = exp / maxExp;
                isUpgrade = true;
                player.gameObject.SetActive(false);
                upgradeWnd.SetActive(true);
                upgradeWnd.GetComponent<UpgradeSelect>().Choice();
                joystick.GetComponent<Joystick>().Stop();
                if (isTutorial && tutorialNum == 2) tutorialNextBtn.gameObject.SetActive(true);
                //joystick.SetActive(false);
            }
            expSlider.fillAmount = exp / maxExp;
        }
    }
    public int Money
    {
        get { return money; }
        set
        {
            money = value;
            PlayerPrefs.SetInt("Money", money);
            moneyTxt.text = money.ToString();
        }
    }
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
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
    public void StartSet()
    {
        isGameOver = isTutorial == true ? true : false;
        Spawner.Instance.Spawn();
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
        Instantiate(player.playerSpawnPc).transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
        player.gameObject.GetComponent<SpriteRenderer>().sprite = player.SC.isSkin.image;
        player.transform.position = Vector3.zero;
        player.transform.Rotate(Vector3.zero);

        joystick.SetActive(true);
        // ���� �ʱ�ȭ
        player.playerSpd = 3;
        player.shotArea.ResetState();
        for (int i = 0; i < (int)PlayerSkills.End; i++)
        {
            PlayerData.Instance.data[(PlayerSkills)i] = false;
        }
        upgradeWnd.GetComponent<UpgradeSelect>().ResetChoice();

        Spawner.Instance.spawnEnemyNum = 4;
    }//���� ����
    public void Tutorial()
    {
        tutorialNum++;
        switch (tutorialNum)
        {
            case 0:
                tutorialNextBtn.gameObject.SetActive(false);
                tutorialTxt.text = "Drag screen to move.";
                StartSet();

                //Ʃ�丮�� ����
                isLevelupTrue = false;
                Spawner.Instance.spawnEnemyNum = 2;
                break;
            case 1:
                tutorialNextBtn.gameObject.SetActive(false);
                tutorialTxt.text = "Approach the enemy and shoot.";
                isGameOver = false;
                Spawner.Instance.Spawn();
                break;
            case 2:
                tutorialNextBtn.gameObject.SetActive(false);
                tutorialTxt.text = "Collet gems and level up.";
                isLevelupTrue = true;
                break;
            case 3:
                tutorialTxt.text = "This concludes the tutorial";
                break;
            default:
                tutorialWnd.SetActive(false);
                isGameOver = true;
                isTutorial = false;
                btnManager.GetComponent<BtnManager>().MenuBtn();
                player.gameObject.SetActive(false);
                joystick.SetActive(false);
                break;
        }
    }//Ʃ�丮��
    public void SetDie()
    {
        SoundManager.Instance.PlaySound(ESoundSources.DIE);
        if (isTutorial) // Ʃ�丮�� �϶�
        {
            isGameOver = true;
            isGameOver = false;
            player.gameObject.SetActive(false);
            Instantiate(player.playerSpawnPc).transform.position = Vector3.zero;
            player.gameObject.SetActive(true);
            player.transform.position = Vector3.zero;
            player.transform.Rotate(Vector3.zero);
        }
        else // �ΰ��� �϶�
        {
            isGameOver = true;
            joystick.GetComponent<Joystick>().Stop();
            // joystick.SetActive(false);
            player.gameObject.SetActive(false);
            //�ְ����� ���
            if (score > PlayerPrefs.GetFloat("HighScore"))
            {
                highScore = score;
                PlayerPrefs.SetFloat("HighScore", highScore);
                highScoreTxt.text = highScore.ToString();
            }

            gameOverWnd.SetActive(true);

        }

    }//���� ���� ����
}
