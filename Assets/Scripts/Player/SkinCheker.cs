using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SkinCheker : MonoBehaviour
{
    private readonly int price = 20000;
    public SkinData isSkin;
    public int isSkinIndex { private get; set; }

    public List<SkinData> skins = new List<SkinData>();
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
    private void Start()
    {
        selectSkin = skins[0];
        isSkin = skins[isSkinIndex];
        SkinDisplay();
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
        
        if (selectSkin.index >= (skins.Count -1)) selectSkin = skins[0];
        else selectSkin = skins[(selectSkin.index + 1)];

        SkinDisplay();

    }
    private void SkinDisplay()
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
            selectBtnTxt.gameObject.SetActive(selectSkin.isBuy);
        }
    }
    public void SelectBtn()
    {
        SoundManager.Instance.PlaySound(ESoundSources.BUTTON);

        if (selectSkin.isBuy == false && GameManager.Instance.Money >= price)
        {
            GameManager.Instance.Money -= price;
            selectSkin.isBuy = true;
        }
        else if (selectSkin.isBuy && isSkin != selectSkin)
        {
            isSkin = selectSkin;
            selectBtnTxt.text = "SELECTED";
            PlayerPrefs.SetInt("IsSkinIndex", isSkin.index);
        }


        SkinDisplay();
    }
}
