using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ESoundSources
{
    BGM,
    BUTTON,
    SHOT,
    MONEY,
    LEVEL,
    DIE,
    BLOCKED,
    BUY,
    END
}
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    private List<AudioClip> audioSources = new List<AudioClip>();

    public float BGMVolum;
    public float SFXVolum;

    public AudioSource bgm;
    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < ((int)ESoundSources.END); i++)
        {
            audioSources.Add(Resources.Load<AudioClip>("Audio/"+ ((ESoundSources)i).ToString()));
        }
        BGMVolum = DataManager.Instance.myState.bgmVolum;
        SFXVolum = DataManager.Instance.myState.sfxVolum;
        PlaySound(ESoundSources.BGM);
    }

    public void PlaySound(ESoundSources source)
    {
        
        GameObject go = new GameObject("sound");

        AudioSource audio = go.AddComponent<AudioSource>();
        audio.clip = audioSources[((int)source)];

        if (source == ESoundSources.BGM)
        {
            bgm = audio;
            audio.volume = BGMVolum;
            audio.loop = true;
        }
        else audio.volume = SFXVolum;
        audio.Play();

        if(source != ESoundSources.BGM)
        Destroy(go, audio.clip.length);
    }

}
