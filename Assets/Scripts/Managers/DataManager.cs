using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening.CustomPlugins;
using System.Runtime.InteropServices;
using System.IO;

[Serializable]
public class SaveState
{
    public int money;
    public float highScore;

    public float bgmVolum;
    public float sfxVolum;

    public int isSkinIndex;
    public bool[] skinIsBuy = new bool[7]; 
}

public class DataManager : Singleton<DataManager>
{
    [SerializeField] private SaveState myState = new SaveState();

    [SerializeField] private SkinCheker skinCheker;

    [SerializeField] private SkinData[] skinData;

    string json;
    private void Start()
    {
        Save();
        Load();
    }
    public void Save()
    {
        myState.money = GameManager.Instance.Money;
        myState.highScore = GameManager.Instance.highScore;

        myState.bgmVolum = SoundManager.Instance.BGMVolum;
        myState.sfxVolum = SoundManager.Instance.SFXVolum;

        myState.isSkinIndex = skinCheker.isSkin.index;

        for (int i = 0; i < skinData.Length; i++)
        {
            myState.skinIsBuy[i] = skinData[i].isBuy;
        }

        json = JsonUtility.ToJson(myState);
        print(json);
    }

    public void Load()
    {
        myState = JsonUtility.FromJson<SaveState>(json);

        GameManager.Instance.Money = myState.money;
        GameManager.Instance.highScore = myState.highScore;

        SoundManager.Instance.BGMVolum = myState.bgmVolum;
        SoundManager.Instance.SFXVolum = myState.sfxVolum;

        skinCheker.isSkinIndex = myState.isSkinIndex;

        for (int i = 0; i < skinData.Length; i++)
        {
           skinData[i].isBuy = myState.skinIsBuy[i];
        }

        print("load");
    }
}
