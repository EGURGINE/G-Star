using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public enum BtnType
{
    damage,
    speed,
    shotSpeed,
    doubleBullet,
    doubleShot,
    RadialShot,
    BulletSpeed,
    ShotRange,
    lazer,
    QuadBullet,
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
        SoundManager.Instance.PlaySound(ESoundSources.BUTTON);

        GameManager.Instance.isUpgrade = false;
        Ability();
        GameManager.Instance.joystick.SetActive(true);
        upgradeWnd.GetComponent<UpgradeSelect>().Check(this.gameObject);
        upgradeWnd.SetActive(false);
        player.SetActive(true);
        player.transform.position = Vector3.zero;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Spawner.Instance.enemySpawnTime = 0;
        GameManager.Instance.NextLevel();
        GameManager.Instance.expSlider.DOKill();
        GameManager.Instance.expSlider.DOFade(1, 0.1f);
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
                GameManager.Instance.player.playerSpd += 0.5f;
                break;
            case BtnType.shotSpeed:
                ShotArea.Instance.shotSpd -= 0.005f;
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
                ShotArea.Instance.bulletSpd = 8;
                break;
            case BtnType.ShotRange:
                ShotArea.Instance.ShotRangeUp();
                break;
            case BtnType.lazer:
                break;
            case BtnType.QuadBullet:
                PlayerData.Instance.data[PlayerSkills.QuadShot] = true;
                break;
            case BtnType.Piercing:
                PlayerData.Instance.data[PlayerSkills.Piercing] = true;
                break;
            case BtnType.BackShot:
                PlayerData.Instance.data[PlayerSkills.BackShot] = true;
                break;
            case BtnType.Score:
                GameManager.Instance.Score += 50;
                break;
            case BtnType.Money:
                GameManager.Instance.Money += 10;
                break;
        }
    }
}
