using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Setting : MonoBehaviour
{
    [SerializeField] Slider Bgm;
    [SerializeField] TextMeshProUGUI bgmValue;
    [SerializeField] Slider Sfx;
    [SerializeField] TextMeshProUGUI sfxValue;
    [SerializeField] GameObject settingWnd;
    public bool isSetting = false;

    private void Start()
    {
        Bgm.value = SoundManager.Instance.BGMVolum * 200;
        Sfx.value = SoundManager.Instance.SFXVolum * 100;
    }
    public void SetBgmValue(float value)
    {
        SoundManager.Instance.BGMVolum = value / 200;
        bgmValue.text = value.ToString() + "%";
        SoundManager.Instance.bgm.volume = value / 200;
    }
    public void SetSfxValue(float value)
    {
        SoundManager.Instance.SFXVolum = value / 100;
        sfxValue.text = value.ToString() + "%";
    }

    public void ResetBtn()
    {
        Bgm.value = 50;
        Sfx.value = 50;
        bgmValue.text = 50.ToString() + "%";
        sfxValue.text = 50.ToString() + "%";
    }

    public void ExitBtn()
    {
        Application.Quit();
    }

    public void IngameWndBtn()
    {
        if (isSetting == false)
        {
            isSetting = true;
            Time.timeScale = 0f;
        }
        else
        {
            isSetting = false;
            Time.timeScale = 1f;
        }
        settingWnd.SetActive(isSetting);
        UpgradeBottomUI.Instance.UISeeOn(isSetting);
    }
}
