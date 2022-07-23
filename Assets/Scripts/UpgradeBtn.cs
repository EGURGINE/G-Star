using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum BtnType
{
    damage,
    speed,
    shotSpeed,
    doubleBullet,
    doubleShot,
    RadialShot,
    lazer,
    QuadShot,
    Piercing,
    BackShot
}

public class UpgradeBtn : MonoBehaviour
{
    [SerializeField] private BtnType type;
    [SerializeField] GameObject player;
    [SerializeField] GameObject upgradeWnd;
    [SerializeField] GameObject playerData;

    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => UPBtn());
    }
    public void UPBtn()
    {
        GameManager.Instance.isUpgrade = false;
        Ability();
        GameManager.Instance.joystick.SetActive(true);
        upgradeWnd.GetComponent<UpgradeSelect>().Check(this.gameObject);
        print("gelgel");
        upgradeWnd.SetActive(false);
        player.SetActive(true);
        player.transform.position = Vector3.zero;
        gameObject.SetActive(false);
    }

    private void Ability()
    {
        switch (type)
        {
            case BtnType.damage:
                ShotArea.Instance.dmg++;
                break;
            case BtnType.speed:
                GameManager.Instance.playerSpd += 1;
                break;
            case BtnType.shotSpeed:
                ShotArea.Instance.shotSpd -= 0.1f;
                break;
            case BtnType.doubleShot:   
                playerData.GetComponent<PlayerData>().PlayerSkill[0] = 1;
                break;
            case BtnType.doubleBullet:
                playerData.GetComponent<PlayerData>().PlayerSkill[1] = 1;
                break;
            case BtnType.RadialShot:
                playerData.GetComponent<PlayerData>().PlayerSkill[2] = 1;
                break;
            case BtnType.lazer:
                break;
            case BtnType.QuadShot:
                break;
            case BtnType.Piercing:
                break;
            case BtnType.BackShot:
                break;
        }
    }
}
