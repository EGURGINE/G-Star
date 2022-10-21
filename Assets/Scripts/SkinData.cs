using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skin Datas", menuName = "Scriptable Object/Skin Data", order = int.MaxValue)]
public class SkinData : ScriptableObject
{
    public int index;
    public new string name;
    public Sprite image;
    public bool isBuy;
}
