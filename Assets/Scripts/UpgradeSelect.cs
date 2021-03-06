using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSelect : MonoBehaviour
{
    [SerializeField] List<GameObject> UPGRADE;
    [SerializeField] List<GameObject> upgrade;
    [SerializeField] List<GameObject> choice;

    GameObject[] choiceCheck = new GameObject[2];
    [SerializeField] private Transform[] choice_Pos;

    public void ResetChoice()
    {
        upgrade.Clear();
        foreach (var item in UPGRADE)
        {
            upgrade.Add(item);
        }
    }
    public void Choice()
    {
        for (int i = 0; i < 2; i++)
        {
            choiceCheck[i] = upgrade[Random.Range(0, upgrade.Count - 1)];
            upgrade.Remove(choiceCheck[i]);
            choice.Add(choiceCheck[i]);
        }

        for (int i = 0; i < choice.Count; i++)
        {
            choice[i].SetActive(true);
            choice[i].transform.position = choice_Pos[i].transform.position;
        }
    }

    public void Check(GameObject _this)
    {
        if (_this == choiceCheck[0]) { upgrade.Add(choiceCheck[1]); print("1"); }
        else if (_this == choiceCheck[1]) { upgrade.Add(choiceCheck[0]); print("2"); }
        choice.Clear();
    }
}
