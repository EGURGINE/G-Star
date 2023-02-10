using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class ClearWnd : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI enemyKillCountTxt;
    [SerializeField] private TextMeshProUGUI getMoneyTxt;
    [SerializeField] private TextMeshProUGUI clearCountTxt;

    [Button(Name = "Button name")]
    public void WndSet()
    {
        print("set");

        GameManager.Instance.Money += 500;

        scoreTxt.text = $"Score : {GameManager.Instance.Score}";
        enemyKillCountTxt.text = $"Enemy Kill : {GameManager.Instance.enemyKill}";
        getMoneyTxt.text = $"Get Money : {GameManager.Instance.getMoney}";
        clearCountTxt.text = $"Clear Count : {GameManager.Instance.ClearCount}";

        UpgradeBottomUI.Instance.gameObject.SetActive(true);
    }
}
