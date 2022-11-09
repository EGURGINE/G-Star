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
    [SerializeField] private GameObject upgradeWnd;
    [SerializeField] private GameObject playerData;
    public GameObject mIcon;
    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => UPBtn());
    }
    public void UPBtn()
    {
        SoundManager.Instance.PlaySound(ESoundSources.BUTTON);

        if (GameManager.Instance.isStartingAbility && GameManager.Instance.Money < 1000) return;

        this.transform.DOKill();
            //능력
            Ability();
        upgradeWnd.GetComponent<UpgradeSelect>().ResetCount();
        
        UpgradeBottomUI.Instance.OnUI(type);
        
        //버튼 체크
        upgradeWnd.GetComponent<UpgradeSelect>().Check(gameObject);
        if (GameManager.Instance.isStartingAbility)
        {
            GameManager.Instance.Money -= 1000;
            PlayerData.Instance.StartAbility += 1;
            if (PlayerData.Instance.StartAbility >= 3)
            {
                GameManager.Instance.isStartingAbility = false;
            }
        }
    }

    private void OnEnable()
    {
        transform.localScale = Vector3.one;
        float posY = transform.localPosition.y + 30;
        transform.DOLocalMoveY(posY, 0.5f).SetLoops(-1,LoopType.Yoyo);
    }
    private void Ability()
    {
        switch (type)
        {
            case BtnType.damage:
                GameManager.Instance.player.shotArea.dmg++;
                break;
            case BtnType.speed:
                GameManager.Instance.player.playerSpd += 0.5f;
                break;
            case BtnType.shotSpeed:
                GameManager.Instance.player.shotArea.bulletShotSpd -= 0.005f;
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
                GameManager.Instance.player.shotArea.bulletSpd = 8;
                break;
            case BtnType.ShotRange:
                GameManager.Instance.player.shotArea.ShotRangeUp();
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
