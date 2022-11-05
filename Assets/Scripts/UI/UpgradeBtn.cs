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
    QuadBullet,
    Piercing,
    BackShot,
    Boom,
    Score,
    Money
}

public class UpgradeBtn : MonoBehaviour
{
    public BtnType type;
    private GameObject player => GameManager.Instance.player.gameObject;
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

        //�ɷ�
        Ability();

        //�÷��̾� ��ȯ
        player.SetActive(true);
        player.transform.position = Vector3.zero;
        player.transform.localEulerAngles = Vector3.zero;

        GameManager.Instance.NextLevel();
        GameManager.Instance.expSlider.DOKill();
        GameManager.Instance.expSlider.DOFade(1, 0.1f);
       
        Spawner.Instance.enemySpawnTime = 0;
        
        UpgradeBottomUI.Instance.OnUI(type);
        
        //��ư üũ
        upgradeWnd.GetComponent<UpgradeSelect>().Check(this.gameObject);
        upgradeWnd.SetActive(false);
        
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
                ShotArea.Instance.bulletShotSpd -= 0.005f;
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
            case BtnType.QuadBullet:
                PlayerData.Instance.data[PlayerSkills.QuadShot] = true;
                break;
            case BtnType.Piercing:
                PlayerData.Instance.data[PlayerSkills.Piercing] = true;
                break;
            case BtnType.BackShot:
                PlayerData.Instance.data[PlayerSkills.BackShot] = true;
                break;
            case BtnType.Boom:
                PlayerData.Instance.data[PlayerSkills.Boom] = true;
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
