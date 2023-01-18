using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ReviveWnd : MonoBehaviour
{
    [SerializeField] private Image countNum;
    [SerializeField] private Sprite[] nums;
    private bool isCnt;
    private float cntNum;
    private int cnt;

    private void OnEnable()
    {
        isCnt = true;
    }
    private void FixedUpdate()
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

    public void SkipBtn()
    {
        GameManager.Instance.SetDie();
    }
}
