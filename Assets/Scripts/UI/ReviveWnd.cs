using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ReviveWnd : MonoBehaviour
{
    [SerializeField] private Image countNum;
    [SerializeField] private Sprite[] nums;
    private bool isCnt;
    private float cntNum;
    private int cnt;
    private readonly int price = 500;
    [SerializeField] private TextMeshProUGUI priceTxt;
    private void OnEnable()
    {
        isCnt = true;

        if (GameManager.Instance.Money >= price) priceTxt.color = Color.white;
        else priceTxt.color = Color.red;

    }
    private void Update()
    {
        if (isCnt == false) return;
        cntNum += Time.deltaTime;
        if (cntNum >= 1)
        {
            cnt++;
            cntNum = 0;

            if (cnt >= 5) SkipBtn();
            countNum.sprite = nums[cnt];
        }
    }

    private void OnDisable()
    {
        isCnt = false;
        cnt = 0;
        countNum.sprite = nums[cnt];
    }
    public void MoneyReviveBtn()
    {
        if (GameManager.Instance.Money < price) return;
        GameManager.Instance.Money -= price;
        Revive();
    }

    public void Revive()
    {
        GameManager.Instance.isGameOver = false;
        GameManager.Instance.player.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    public void SkipBtn()
    {
        GameManager.Instance.SetDie();
        gameObject.SetActive(false);

    }
}
