using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class GameManager : Singleton<GameManager>
{

    [Header("플레이어")]
    public Player player;

    [Header("점수")]
    private int score;
    [SerializeField] private Text scoreTxt;
    public float highScore;
    [SerializeField] private Text highScoreTxt;

    [Header("돈")]
    private int money;
    [SerializeField] private Text moneyTxt;

    [Header("게임오버")]
    [SerializeField] private GameObject gameOverWnd;
    public bool isGameOver = false;

    [Header("부활하기")]
    [SerializeField] private GameObject reviveWndObj;
    public ReviveWnd reviveWnd;
    private bool isRevive;

    [Header("레벨")]
    [SerializeField] private Text lv;
    [SerializeField] private int level;
    public Image expSlider;
    private float exp;
    public float maxExp;

    [Header("업그레이드")]
    public UpgradeSelect upSelect;
    public bool isUpgrade = false;

    [Header("튜토리얼")]
    public bool isTutorial;
    private bool isLevelupTrue;
    public GameObject tutorialWnd;
    [SerializeField] private TextMeshProUGUI tutorialTxt;
    public int tutorialNum;
    public Button tutorialNextBtn;
    [SerializeField] GameObject btnManager;

    [Header("매니저")]
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
                //레벨업 연출
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

        //게임 시작시 최고점수 가져오기
        highScore = DataManager.Instance.myState.highScore;
        highScoreTxt.text = highScore.ToString();

        // 게임 시작시 저장한 돈 가져오기
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
    }// 레벨 디자인
    public void NextLevel()
    {
        exp = 0;
        maxExp *= 1.5f;
        expSlider.fillAmount = 0;
    } //레벨 셋팅
    private void LevelProduction()
    {
        expSlider.DOFade(0.5f,0.5f).SetLoops(-1,LoopType.Yoyo);
    }// 레벨 연출

    private void UpgradeWndOn()
    {
        isUpgrade = true;
        upSelect.gameObject.SetActive(true);
        upSelect.GetComponent<UpgradeSelect>().Choice();
    }//업그레이드 창 열기
    public void StartSet()
    {
        //경험치 초기화
        level = 1;
        lv.text = level.ToString();
        exp = 0;
        maxExp = 30;
        expSlider.fillAmount = 0;


        time = 0;
        isRevive = false;
        //점수 초기화
        score = 0;
        scoreTxt.text = score.ToString();
        highScoreTxt.text = highScore.ToString();

        // 스텟 초기화
        player.playerSpd = 0.5f;
        player.shotArea.ResetState();

        // 능력들 초기화
        isStartingAbility = true;
        PlayerData.Instance.StartAbility = 0;
        for (int i = 0; i < (int)PlayerSkills.End; i++)
        {
            PlayerData.Instance.data[(PlayerSkills)i] = false;
        }
        upSelect.GetComponent<UpgradeSelect>().ResetChoice();
        upSelect.gameObject.SetActive(false);
        //스포너 초기화
        Spawner.Instance.spawnEnemyTypeNum = 1;
        Spawner.Instance.enemySpawnNum = 4;
        Spawner.Instance.enemySpawnTime = 0;
        Spawner.Instance.spawnDelay = 3;

        //하단 UI 초기화
        UpgradeBottomUI.Instance.StartSet();

        //카메라 셋팅
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

            //타이머 셋팅
        }
        isGameOver = isTutorial == true ? true : false;

        
    }//시작 셋팅

    public void PlayerSpawn()
    {

        CameraSetting.Instance.MainPost();
        Instantiate(player.playerSpawnPc).transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
        player.gameObject.GetComponent<SpriteRenderer>().sprite = player.SC.isSkin.image;
        player.transform.position = Vector3.zero;
        player.transform.rotation = Quaternion.Euler(Vector3.zero);

    }//플레이어 스폰
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
                //튜토리얼 조건
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
    }//튜토리얼

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

        if (isTutorial) // 튜토리얼 일때
        {
            isGameOver = false;
            player.gameObject.SetActive(false);
            Instantiate(player.playerSpawnPc).transform.position = Vector3.zero;
            player.gameObject.SetActive(true);
            ShotArea.Instance.Enemys.Clear();
            player.transform.position = Vector3.zero;
            player.transform.Rotate(Vector3.zero);
        }
        else // 인게임 일때
        {
            dieAD.ADCheck();
            CameraSetting.Instance.DiePost();
            isGameOver = true;
            player.gameObject.SetActive(false);
            //최고점수 기록
            if (score > highScore)
            {
                highScore = score;
                highScoreTxt.text = highScore.ToString();
            }

            gameOverWnd.SetActive(true);
            //광고
        }

    }//게임 오버 셋팅

    private void OnApplicationQuit()
    {
        DOTween.KillAll();
        StopAllCoroutines();
    }
}
