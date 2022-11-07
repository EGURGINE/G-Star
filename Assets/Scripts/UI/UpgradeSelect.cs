using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UpgradeSelect : MonoBehaviour
{
    [SerializeField] List<GameObject> UPGRADE;
    [SerializeField] List<GameObject> lastBtn;
    [SerializeField] List<GameObject> upgrade;
    [SerializeField] List<GameObject> choice;

    GameObject[] choiceCheck = new GameObject[2];
    [SerializeField] private Transform[] choice_Pos;

    [SerializeField] private GameObject btns;
    public void ResetChoice()
    {
        // upgrade 리스트 지우고 새로 채우기
        upgrade.Clear();
        for (int i = 0; i < UPGRADE.Count; i++)
        {
            upgrade.Add(UPGRADE[i]);
        }
    }
    public void Choice()
    {
        //고르기전 버튼들 끄기
        for (int i = 0; i < btns.transform.childCount; i++)
        {
            btns.transform.GetChild(i).gameObject.SetActive(false);
        }

        //랜덤으로 버튼 뽑기
        for (int i = 0; i < 2; i++)
        {
            choiceCheck[i] = upgrade[Random.Range(0, upgrade.Count - 1)];
            upgrade.Remove(choiceCheck[i]);
            choice.Add(choiceCheck[i]);
        }

        //뽑은 버튼 켜기
        for (int i = 0; i < 2; i++)
        {
            choice[i].SetActive(true);
            choice[i].GetComponent<UpgradeBtn>().mIcon.SetActive(GameManager.Instance.isStartingAbility);
            choice[i].transform.position = choice_Pos[i].transform.position;
        }
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
        //반복 버튼인지 체크
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
            if (_this == choiceCheck[0]) upgrade.Add(choiceCheck[1]);
            else upgrade.Add(choiceCheck[0]);
        }
        
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

        this.GetComponent<UpgradeSelect>().Push();
        GameManager.Instance.isUpgrade = false;
        GameManager.Instance.PlayerSpawn();
        GameManager.Instance.isStartingAbility = false;

        GameManager.Instance.NextLevel();
        GameManager.Instance.expSlider.DOKill();
        GameManager.Instance.expSlider.DOFade(1, 0.1f);

        Spawner.Instance.enemySpawnTime = 0;

        this.gameObject.SetActive(false);
    }
}
