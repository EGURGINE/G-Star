using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeBottomUI : Singleton<UpgradeBottomUI>
{
    [SerializeField] private List<Sprite> upUI = new List<Sprite>();
    [SerializeField] private GameObject basicUI;

    public void StartSet()
    {
        for (int i = 0; i < this.transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);
    }
    public void OnUI(BtnType type)
    {
        if (type == BtnType.Score || type == BtnType.Money) return;

        GameObject go = Instantiate(basicUI,this.transform);
        go.transform.GetChild(0).GetComponent<Image>().sprite = upUI[((int)type)];
    }

}
