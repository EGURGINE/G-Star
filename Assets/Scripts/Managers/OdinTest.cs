using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OdinTest : MonoBehaviour
{

    [AssetList]
    public List<SkinData> num1 = new List<SkinData>();

    [Space(10f)]

    [AssetList(Tags = "UI")]
    public List<GameObject> num2 = new List<GameObject>();
    [Space(10f)]


    [BoxGroup("Some Group")]
    public int a;
    [BoxGroup("Some Group2")]
    public int b;
    [BoxGroup("Some Group")]
    public int c;

    [Space(10f)]

    [ColorPalette]
    public Color color;

    [ColorPalette("Autumn")]
    public Color color2;

    [Space(10f)]

    [EnableIf("someBool")]
    public int abc;

    [Space(10f)]

    [FoldoutGroup("Group a")]
    public string group_a;
    [FoldoutGroup("Group a")]
    public int group_b;
    [FoldoutGroup("Group b")]
    public string group_c;

    [Space(10f)]

    [OnValueChanged("StringValueChange")]
    public string string_a;
    private void StringValueChange()
    {
        print("Change");
    }



    [Button(Name ="Button name")]
    private void MyButton()
    {
        print("Btn");
    }
    [Space(10f)]

    public Quaternion quaternion;
    public Vector3 vector;

    [Space(10f)]

    [ValidateInput("ValidateString", "String has to be 'a'")]
    public string validate;

    private bool ValidateString(string val)
    {
        return val == "a";
    }

    [Space(10f)]

    [TabGroup("Group_1")]
    public int num_01;
    [TabGroup("Group_1")]
    public int num_02;
    [TabGroup("Group_2")]
    public int num_03;
    [TabGroup("Group_2")]
    public int num_04;

    [Space(10f)]

    [ValueDropdown("Values")]
    public string valueDropdown;

    private List<string> Values()
    {
        return new List<string>()
        {
            "abc",
            "123"
        };
    }
}
