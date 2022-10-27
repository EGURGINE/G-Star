using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBottomUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> upUI = new List<GameObject>();


    public void StartSet()
    {
        foreach (var item in upUI)
        {
            item.SetActive(false);
        }
    }

}
