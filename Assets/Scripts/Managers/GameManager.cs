using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class GameManager : Singleton<GameManager>
{

    [Header("�÷��̾�")]
    public Player player;

    [Header("����")]
    private int score;
    [SerializeField] private Text scoreTxt;
    public float highScore;
    [SerializeField] private Text highScoreTxt;

    [Header("��")]
    private int money;
    [SerializeField] private Text moneyTxt;

    [Header("���ӿ���")]
    [SerializeField] private GameObject gameOverWnd;
    public bool isGameOver = false;

    [Header("��Ȱ�ϱ�")]
    [SerializeField] private GameObject reviveWndObj;
    public ReviveWnd reviveWnd;
    private bool isRevive;

    [Header("����")]
    [SerializeField] private Text lv;
    [SerializeField] private int level;
    public Image expSlider;
    private float exp;
    public float maxExp;

    [Header("���׷��̵�")]
    public UpgradeSelect upSelect;
    public bool isUpgrade = false;

    [Header("Ʃ�丮��")]
    public bool isTutorial;
    private bool isLevelupTrue;
    public GameObject tutorialWnd;
    [SerializeField] private TextMeshProUGUI tutorialTxt;
    public int tutorialNum;
    public Button tutorialNextBtn;
    [SerializeField] GameObject btnManager;

    [Header("�Ŵ���")]
    public ObserverPattern.ObserverData observerManager;
    public BtnManager btnGM;
    public FrontAD dieAD;
    public bool isStartingAbility;
    public float Exp
    {
        get { return exp; }
        set
        {
            if (isTutorial == true && isLevelupTrue == false || isUpgrade == true) return;
            exp = value;
            expSlider.fillAmount = exp / maxExp;
            if (exp >= maxExp)
            {
                //������ ����
                observerManager.NotifyObservers();
                SoundManager.Instance.PlaySound(ESoundSources.LEVEL);
                
                level++;
                lv.text = level.ToString();
                LevelProduction();

                player.gameObject.SetActive(false);

                UpgradeWndOn();

                if (isTutorial && tutorialNum == 2) tutorialNextBtn.gameObject.SetActive(true);
            }
        }
    }
    public int Money
    {
        get { return money; }
        set
        {
            money = value;
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
        isGameOver = true;

        //���� ���۽� �ְ����� ��������
        highScore = DataManager.Instance.myState.highScore;
        highScoreTxt.text = highScore.ToString();

        // ���� ���۽� ������ �� ��������
        money = DataManager.Instance.myState.money;
        moneyTxt.text = money.ToString();

    }

    private float time;
    [SerializeField] private float[] waveTime = new float[7];

    private void FixedUpdate()
    {
        if (isGameOver == false && isUpgrade == false) LevelDesign();
    }
    private void LevelDesign()
    {
        time += Time.deltaTime;
       
        if (time > waveTime[5])
        {
            Spawner.Instance.enemySpawnNum = 7;
            Spawner.Instance.spawnDelay = 2f;

        }
        else if (time > waveTime[4])
        {
            Spawner.Instance.spawnEnemyTypeNum = 6;
            Spawner.Instance.enemySpawnNum = 6;
            Spawner.Instance.spawnDelay = 2.5f;
            Spawner.Instance.spawnEnemyTypeNum = 4;
        }
        else if (time > waveTime[3])
        {
            Spawner.Instance.enemySpawnNum = 5;
            Spawner.Instance.spawnDelay = 2.6f;
            Spawner.Instance.spawnEnemyTypeNum = 3;

        }
        else if (time > waveTime[2])
        {
            Spawner.Instance.enemySpawnNum = 5;
        }
        else if (time > waveTime[1])
        {
            Spawner.Instance.spawnDelay = 2.7f;
        }
        else if (time > waveTime[0])
        {
            Spawner.Instance.spawnEnemyTypeNum = 2;
        }
        //else Spawner.Instance.spawnEnemyTypeNum = 6;
    }// ���� ������
    public void NextLevel()
    {
        exp = 0;
        maxExp *= 1.5f;
        expSlider.fillAmount = 0;
    } //���� ����
    private void LevelProduction()
    {
        expSlider.DOFade(0.5f,0.5f).SetLoops(-1,LoopType.Yoyo);
    }// ���� ����

    private void UpgradeWndOn()
    {
        isUpgrade = true;
        upSelect.gameObject.SetActive(true);
        upSelect.GetComponent<UpgradeSelect>().Choice();
    }//���׷��̵� â ����
    public void StartSet()
    {
        //����ġ �ʱ�ȭ
        level = 1;
        lv.text = level.ToString();
        exp = 0;
        maxExp = 30;
        expSlider.fillAmount = 0;


        time = 0;
        isRevive = false;
        //���� �ʱ�ȭ
        score = 0;
        scoreTxt.text = score.ToString();
        highScoreTxt.text = highScore.ToString();

        // ���� �ʱ�ȭ
        player.playerSpd = 0.5f;
        player.shotArea.ResetState();

        // �ɷµ� �ʱ�ȭ
        isStartingAbility = true;
        PlayerData.Instance.StartAbility = 0;
        for (int i = 0; i < (int)PlayerSkills.End; i++)
        {
            PlayerData.Instance.data[(PlayerSkills)i] = false;
        }
        upSelect.GetComponent<UpgradeSelect>().ResetChoice();
        upSelect.gameObject.SetActive(false);
        //������ �ʱ�ȭ
        Spawner.Instance.spawnEnemyTypeNum = 1;
        Spawner.Instance.enemySpawnNum = 4;
        Spawner.Instance.enemySpawnTime = 0;
        Spawner.Instance.spawnDelay = 3;

        //�ϴ� UI �ʱ�ȭ
        UpgradeBottomUI.Instance.StartSet();

        //ī�޶� ����
        Camera.main.transform.DOMove(new Vector3(0, 0, -10), 0.01f);
        CameraSetting.Instance.MainPost();


        if (isTutorial == false)
        {
            if (money >= 1000) UpgradeWndOn();
            else
            {
                isStartingAbility = false;
                PlayerSpawn();
            }

            //Ÿ�̸� ����
        }
        isGameOver = isTutorial == true ? true : false;

        
    }//���� ����

    public void PlayerSpawn()
    {

        CameraSetting.Instance.MainPost();
        Instantiate(player.playerSpawnPc).transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
        player.gameObject.GetComponent<SpriteRenderer>().sprite = player.SC.isSkin.image;
        player.transform.position = Vector3.zero;
        player.transform.rotation = Quaternion.Euler(Vector3.zero);

    }//�÷��̾� ����
    public void Tutorial()
    {
        tutorialNum++;
        switch (tutorialNum)
        {
            case 0:
                tutorialNextBtn.gameObject.SetActive(false);
                tutorialTxt.text = "Drag screen to move.";
                StartSet();
                PlayerSpawn();
                //Ʃ�丮�� ����
                isLevelupTrue = false;
                Spawner.Instance.enemySpawnNum = 2;
                isStartingAbility = false;
                break;
            case 1:
                tutorialNextBtn.gameObject.SetActive(false);
                tutorialTxt.text = "Approach the enemy and shoot.";
                Spawner.Instance.spawnEnemyTypeNum = 1;
                Spawner.Instance.spawnDelay = 3;
                Spawner.Instance.enemySpawnTime = 2;
                isGameOver = false;
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
                observerManager.NotifyObservers();
                break;
        }
    }//Ʃ�丮��

    public void SetRevive()
    {
        observerManager.NotifyObservers();

        if (isRevive == false)
        {
            player.gameObject.SetActive(false);
            isRevive = true;
            isGameOver = true;
            reviveWndObj.SetActive(true);
        }
        else SetDie();

    }
    public void SetDie()
    {
        SoundManager.Instance.PlaySound(ESoundSources.DIE);
        observerManager.NotifyObservers();

        if (isTutorial) // Ʃ�丮�� �϶�
        {
            isGameOver = false;
            player.gameObject.SetActive(false);
            Instantiate(player.playerSpawnPc).transform.position = Vector3.zero;
            player.gameObject.SetActive(true);
            ShotArea.Instance.Enemys.Clear();
            player.transform.position = Vector3.zero;
            player.transform.Rotate(Vector3.zero);
        }
        else // �ΰ��� �϶�
        {
            dieAD.ADCheck();
            CameraSetting.Instance.DiePost();
            isGameOver = true;
            player.gameObject.SetActive(false);
            //�ְ����� ���
            if (score > highScore)
            {
                highScore = score;
                highScoreTxt.text = highScore.ToString();
            }

            gameOverWnd.SetActive(true);
            //����
        }

    }//���� ���� ����

    private void OnApplicationQuit()
    {
        DOTween.KillAll();
        StopAllCoroutines();
    }
}
