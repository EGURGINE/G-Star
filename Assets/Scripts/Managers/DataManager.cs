using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Sirenix.OdinInspector;

[Serializable]
public class SaveState
{
    public int money;
    public float highScore;

    public float bgmVolum;
    public float sfxVolum;

    public int isSkinIndex;
    public List<bool> skinIsBuy = new List<bool>(); 
}

public class DataManager : Singleton<DataManager>
{
    public SaveState myState = new SaveState();

    [SerializeField] private SkinCheker skinCheker;

    [AssetList]
    [SerializeField] private SkinData[] skinData;

    string filePath;
    private void Awake()
    {
        //저장 경로
        filePath = Application.persistentDataPath + "/GameDataText.txt";
        Load();
    }
    /// <summary>
    /// 초기화
    /// </summary>
    private void ResetSave()
    {
        myState.money = 0;
        myState.highScore = 0;
        
        myState.bgmVolum = 50;
        myState.sfxVolum = 50;

        myState.isSkinIndex = 0;

        for (int i = 0; i < 7; i++)
        {
            myState.skinIsBuy.Add(false);
        }
        myState.skinIsBuy[0] = true;
        Save();
        Load();
    }
    /// <summary>
    /// 저장
    /// </summary>
    public void Save()
    {
        myState.money = GameManager.Instance.Money;
        myState.highScore = GameManager.Instance.highScore;

        myState.bgmVolum = SoundManager.Instance.BGMVolum;
        myState.sfxVolum = SoundManager.Instance.SFXVolum;

        myState.isSkinIndex = skinCheker.isSkin.index;


        for (int i = 0; i < 7; i++)
        {
            myState.skinIsBuy[i] = skinData[i].isBuy;
        }

        string jData = JsonUtility.ToJson(myState);
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jData);
        string code = System.Convert.ToBase64String(bytes);

        File.WriteAllText(filePath, code);
    }
    /// <summary>
    /// 불러오기
    /// </summary>
    public void Load()
    {
        if (!File.Exists(filePath)) { ResetSave(); return; }

        string code = File.ReadAllText(filePath);

        byte[] bytes = System.Convert.FromBase64String(code);
        string jData = System.Text.Encoding.UTF8.GetString(bytes);
        myState = JsonUtility.FromJson<SaveState>(jData);
    }

    /// <summary>
    /// 게임 꺼지면 저장
    /// </summary>
    private void OnApplicationQuit()
    {
        Save();
    }

}
