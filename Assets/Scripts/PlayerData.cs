using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    public float dmg;

    public List<int> PlayerSkill = new List<int>();
    
}
