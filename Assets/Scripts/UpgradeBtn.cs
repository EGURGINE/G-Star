using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBtn : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject upgradeWnd;
    public void DmgUpBtn()
    {
        GameManager.Instance.isUpgrade = false;
        ShotArea.Instance.dmg++;
        GameManager.Instance.joystick.SetActive(true);
        upgradeWnd.SetActive(false);
        player.SetActive(true);
        player.transform.position = Vector3.zero;
    }
    public void SpdUpBtn()
    {
        GameManager.Instance.isUpgrade = false;
        GameManager.Instance.playerSpd+=5;
        GameManager.Instance.joystick.SetActive(true);
        upgradeWnd.SetActive(false);
        player.SetActive(true);
        player.transform.position = Vector3.zero;
    }
    public void ShotSpdUpBtn()
    {
        GameManager.Instance.isUpgrade = false;
        ShotArea.Instance.shotSpd -= 0.1f;
        GameManager.Instance.joystick.SetActive(true);
        upgradeWnd.SetActive(false);
        player.SetActive(true);
        player.transform.position = Vector3.zero;
    }
}
