using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeBottomUI : Singleton<UpgradeBottomUI>
{
    [SerializeField] private List<Sprite> upUI = new List<Sprite>();
    [SerializeField] private Sprite etcUI;
    [SerializeField] private GameObject basicUI;

    public void StartSet()
    {
        int numOfChild = this.transform.childCount;
        for (int i = 0; i < numOfChild; i++)
            Destroy(transform.GetChild(i));
    }
    public void OnUI(BtnType type)
    {
        int numOfChild = this.transform.childCount;
        if (numOfChild >= 12)
        {
            return;
        }
        GameObject go = Instantiate(basicUI,this.transform);
        if (numOfChild == 12)
        {
            go.transform.GetChild(0).GetComponent<Image>().sprite = etcUI;
        }
        else go.transform.GetChild(0).GetComponent<Image>().sprite = upUI[((int)type)];
    }

}
