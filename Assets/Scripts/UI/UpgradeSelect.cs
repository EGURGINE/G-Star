using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeSelect : MonoBehaviour
{
    [SerializeField] List<GameObject> UPGRADE;
    [SerializeField] List<GameObject> lastBtn;
    [SerializeField] List<GameObject> upgrade;
    [SerializeField] List<GameObject> choice;

    GameObject[] choiceCheck = new GameObject[2];
    [SerializeField] private Transform[] choice_Pos;

    [SerializeField] private GameObject btns;

    [SerializeField] private Image countNum;
    [SerializeField] private Sprite[] nums;
    private bool isCnt;
    private float cntNum;
    private int cnt;

    [SerializeField] private GameObject levelUpTxtObj;
    public void ResetChoice()
    {
        // upgrade 리스트 지우고 새로 채우기
        upgrade.Clear();
        for (int i = 0; i < UPGRADE.Count; i++)
        {
            upgrade.Add(UPGRADE[i]);
        }
    }

    public void ChoiceReset()
    {
        choice.Clear();
    }
    public void Choice()
    {
        CameraSetting.Instance.UpgradePost();

        BtnMove();
        // 고르기전 버튼들 끄기
        for (int i = 0; i < btns.transform.childCount; i++)
        {
            btns.transform.GetChild(i).gameObject.SetActive(false);
        }

        // 랜덤으로 버튼 뽑기
        for (int i = 0; i < 2; i++)
        {
            choiceCheck[i] = upgrade[Random.Range(0, upgrade.Count - 1)];
            upgrade.Remove(choiceCheck[i]);
            choice.Add(choiceCheck[i]);
        }

        // 뽑은 버튼 켜기
        for (int i = 0; i < 2; i++)
        {
            choice[i].SetActive(true);
            choice[i].GetComponent<UpgradeBtn>().mIcon.SetActive(GameManager.Instance.isStartingAbility);
            choice[i].transform.position = choice_Pos[i].transform.position;
        }
    }

    private void BtnMove()
    {
        btns.transform.localScale = Vector3.one;
        float posY = btns.transform.localPosition.y + 30;
        btns.transform.DOLocalMoveY(posY, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    public void Push()
    {
        foreach (var item in choiceCheck)
        {
            upgrade.Add(item);
        }
        choice.Clear();
    }
    public void Check(GameObject _this)
    {
        btns.transform.DOKill();
        btns.transform.localPosition = Vector3.zero;
        int checkNum = 0;
        //반복 버튼인지 체크

        if (_this == choiceCheck[0]) checkNum = 1;
        else checkNum = 0;

        //선택한 버튼이 스코어나 돈이면 다시 넣기
        if (_this.GetComponent<UpgradeBtn>().type == BtnType.Score ||
            _this.GetComponent<UpgradeBtn>().type == BtnType.Money)
        {
            foreach (var item in choiceCheck)
            {
                upgrade.Add(item);
            }
        }
        else
        {
            upgrade.Add(choiceCheck[checkNum]); 
        }
        choiceCheck[checkNum].gameObject.SetActive(false);
        _this.transform.DOMove(new Vector3(0,0.5f,0), 0.5f).OnComplete(
            ()=> _this.transform.DOScale(new Vector3(1.2f,1.2f,1),0.5f).OnComplete(
                () =>
                {
                    UpgradeBottomUI.Instance.OnUI(_this.GetComponent<UpgradeBtn>().type);

                    if (GameManager.Instance.isStartingAbility)
                        transform.GetComponent<UpgradeSelect>().Choice();
                    else
                    {

                        GameManager.Instance.isUpgrade = false;
                        //GameManager.Instance.PlayerSpawn();
                        GameManager.Instance.player.gameObject.SetActive(true);
                        CameraSetting.Instance.MainPost();
                        GameManager.Instance.NextLevel();
                        GameManager.Instance.expSlider.DOKill();
                        GameManager.Instance.expSlider.DOFade(1, 0.1f);
                        Spawner.Instance.enemySpawnTime = 0;
                        StartCoroutine(GameManager.Instance.LevelDesign());
                        gameObject.SetActive(false);
                    }
                }
                )
            );
        choice.Clear();

        //만약 남은 버튼수가 2개 미만이면 반복 버튼 추가
        if (upgrade.Count < 2)
        {
            foreach (var item in lastBtn)
            {
                upgrade.Add(item);
            }
        }
    }

    public void SkipBtn()
    {

        isCnt = false;
        this.GetComponent<UpgradeSelect>().Push();
        GameManager.Instance.isUpgrade = false;
        GameManager.Instance.PlayerSpawn();
        GameManager.Instance.isStartingAbility = false;

        GameManager.Instance.NextLevel();
        GameManager.Instance.expSlider.DOKill();
        GameManager.Instance.expSlider.DOFade(1, 0.1f);

        Spawner.Instance.enemySpawnTime = 0;
        StartCoroutine(GameManager.Instance.LevelDesign());
        CameraSetting.Instance.MainPost();
        this.gameObject.SetActive(false);
        
    }
    public void ResetCount()
    {
        cnt = 0;
        cntNum = 0;
        countNum.sprite = nums[0];
    }
    private void Count()
    {
        if (GameManager.Instance.isStartingAbility)
        {
            countNum.gameObject.SetActive(GameManager.Instance.isStartingAbility);

        }
        else countNum.gameObject.SetActive(GameManager.Instance.isStartingAbility);

        cnt = 0;
        cntNum = 0;
        countNum.sprite = nums[0];
        isCnt = true;
    }

    private void FixedUpdate()
    {
        if (isCnt == true)
        {
            cntNum += Time.deltaTime;
            if (cntNum >= 1)
            {
                cnt++;
                cntNum = 0;

                if (cnt >= 5) SkipBtn();
                countNum.sprite = nums[cnt];
            }
        }
    }
    private void OnEnable()
    {
        if (GameManager.Instance.isStartingAbility)
            Count();
        countNum.gameObject.SetActive(GameManager.Instance.isStartingAbility);
        levelUpTxtObj.SetActive(!GameManager.Instance.isStartingAbility);
    }
}
