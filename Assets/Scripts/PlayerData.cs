using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerSkills
{
    doubleBullet,
    doubleShot,
    RadialShot,
    lazer,
    QuadShot,
    Piercing,
    BackShot,
    End
}
public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; set; }

    public Dictionary<PlayerSkills, bool> data = new Dictionary<PlayerSkills, bool>();
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        for (int i = 0; i < (int)PlayerSkills.End; i++)
        {
            data.Add((PlayerSkills)i, false);
        }
    }
}