using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

public class SkinCheker : MonoBehaviour
{
    private readonly int price = 20000;
    public SkinData isSkin;
    public int isSkinIndex { get; set; }

    [AssetList]
    public List<SkinData> skins;

    [SerializeField] 
    private SkinData selectSkin;
    [SerializeField] 
    private Image exSkinImage;
    [SerializeField]
    private TextMeshProUGUI skinName;
    [SerializeField]
    private TextMeshProUGUI selectBtnTxt;
    [SerializeField]
    private TextMeshProUGUI priceBtnTxt;
    [SerializeField]
    private Image moneyImage;

    int num;
    private void Start()
    {
        int dataIndex = DataManager.Instance.myState.isSkinIndex;
        isSkin = skins[dataIndex];
        selectSkin = skins[0];
        startSetSkinData();
        SkinDisplay();
        num = selectSkin.index;
    }

    private void startSetSkinData()
    {
        for (int i = 0; i < 7; i++)
        {
            skins[i].isBuy = DataManager.Instance.myState.skinIsBuy[i];
        }
    }
    public void LeftBtn()
    {
        SoundManager.Instance.PlaySound(ESoundSources.BUTTON);

        if (selectSkin.index <= 0) selectSkin = skins[(skins.Count - 1)];
        else selectSkin = skins[(selectSkin.index -1)];

        SkinDisplay();

    }
    public void RightBtn()
    {
        SoundManager.Instance.PlaySound(ESoundSources.BUTTON);

        if (selectSkin.index >= (skins.Count -1)) selectSkin = skins [0];
        else selectSkin = skins[(selectSkin.index + 1)];

        SkinDisplay();

    }
    public void SkinDisplay()
    {
        exSkinImage.sprite = selectSkin.image;
        skinName.text = selectSkin.name;

        if (selectSkin.isBuy)
        {
            moneyImage.gameObject.SetActive(false);
            priceBtnTxt.gameObject.SetActive(false);
            selectBtnTxt.gameObject.SetActive(selectSkin.isBuy);



            if (isSkin != selectSkin) selectBtnTxt.text = "SELECT";
            else selectBtnTxt.text = "SELECTED";
        } 
        else
        {
            moneyImage.gameObject.SetActive(true);
            priceBtnTxt.gameObject.SetActive(true);
            priceBtnTxt.text = price.ToString();

            if (GameManager.Instance.Money < price) priceBtnTxt.color = Color.red;
            else priceBtnTxt.color = Color.white;
            selectBtnTxt.gameObject.SetActive(selectSkin.isBuy);
        }
    }
    public void SelectBtn()
    {
        if (selectSkin.isBuy == false)
        {
            if (GameManager.Instance.Money < price)
            {
                SoundManager.Instance.PlaySound(ESoundSources.BLOCKED);
            }
            else
            {
                SoundManager.Instance.PlaySound(ESoundSources.BUY);
                GameManager.Instance.Money -= price;
                selectSkin.isBuy = true;
            }

        }
        else
        {
            if (isSkin != selectSkin)
            {
                SoundManager.Instance.PlaySound(ESoundSources.BUTTON);
                isSkin = selectSkin;
                selectBtnTxt.text = "SELECTED";
            }
        }

        SkinDisplay();
    }
}
