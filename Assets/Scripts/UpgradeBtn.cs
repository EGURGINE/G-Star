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
    BulletSpeed,
    lazer,
    QuadShot,
    Piercing,
    BackShot,
    Score,
    Money
}

public class UpgradeBtn : MonoBehaviour
{
    public BtnType type;
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
                PlayerData.Instance.data[PlayerSkills.doubleShot] = true;
                break;
            case BtnType.doubleBullet:
                PlayerData.Instance.data[PlayerSkills.doubleBullet] = true;
                break;
            case BtnType.RadialShot:
                PlayerData.Instance.data[PlayerSkills.RadialShot] = true;
                break;
            case BtnType.BulletSpeed:
                ShotArea.Instance.bulletSpd = 10;
                break;
            case BtnType.lazer:
                break;
            case BtnType.QuadShot:
                break;
            case BtnType.Piercing:
                break;
            case BtnType.BackShot:
                PlayerData.Instance.data[PlayerSkills.BackShot] = true;
                break;
            case BtnType.Score:
                GameManager.Instance.Score = 50;
                break;
            case BtnType.Money:
                GameManager.Instance.Money = 10;
                break;
        }
    }
}
