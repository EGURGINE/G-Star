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

    public SaveState(int _money, float _highScore, float bgm, float sfx, int skinIndex, bool isbool)
    {
        money = _money;
        highScore = _highScore;
        bgmVolum = bgm;
        sfxVolum = sfx;
        isSkinIndex = skinIndex;
        for (int i = 0; i < skinIsBuy.Length; i++)
        {
            skinIsBuy[i] = isbool;
        }
    }

    public int money;
    public float highScore;

    public float bgmVolum;
    public float sfxVolum;

    public int isSkinIndex;
    public bool[] skinIsBuy; 
}

public class DataManager : Singleton<DataManager>
{
    [SerializeField] private SaveState myState;

    [SerializeField] private SkinCheker skinCheker;

    [SerializeField] private SkinData[] skinData;

    private static string SavePath =>
        Application.persistentDataPath + "/saves/";

    private void Start()
    {
        Save();
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

        string json = JsonUtility.ToJson(myState);

        string fileName = "DataSave";
        string path = Application.dataPath + "/" + fileName + ".Json";

        File.WriteAllText(path, json);
    }

    public void Load()
    {
        if (!Directory.Exists(SavePath))
        {
            Directory.CreateDirectory(SavePath);
        }

        string fileName = "DataSave";
        string path = Application.dataPath + "/" + fileName + ".Json";
        string json = File.ReadAllText(path);

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
    }
}
