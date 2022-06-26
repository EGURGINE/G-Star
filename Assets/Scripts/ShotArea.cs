using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ShotArea : MonoBehaviour
{
    public static ShotArea Instance { get; set; }
    [SerializeField] private Transform shotPos;
    [SerializeField] private Transform leftPos, rightPos;
    [SerializeField] private Bullet[] bullets;
    public int dmg;
    public float bulletSpd;
    public float shotSpd;
    private float cnt;
    public List<GameObject> Enemys = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.isGameOver || GameManager.Instance.isUpgrade)
        {
            Enemys.Clear();
            Debug.Log(Enemys[0]);
        }
        if (Enemys != null && cnt >= shotSpd)
        {
            cnt = 0;
            Shot();
        }
        cnt += Time.deltaTime;
    }
    private void Shot()
    {
        if (Enemys.FirstOrDefault() != null&&Enemys[0].GetComponent<BasicEnemy>().isHit)
        {
            shotPos.LookAt(Enemys[0].transform.position);
            leftPos.LookAt(Enemys[0].transform.position + Vector3.left);
            rightPos.LookAt(Enemys[0].transform.position + Vector3.right);

            BulletSet();
            if (PlayerData.Instance.PlayerSkill[0]>0)
            {
                Invoke("MultiShot",0.2f);
            }
        }
    }
    private void BulletSet()
    {
        if (PlayerData.Instance.PlayerSkill[1]>0)
        {
            Bullet bullet_1 = Instantiate(bullets[0]);
            bullet_1.transform.position = shotPos.position+new Vector3(0.2f,0,0);
            bullet_1.SetBullet(dmg, bulletSpd, shotPos.forward);

            Bullet bullet_2 = Instantiate(bullets[0]);
            bullet_2.transform.position = shotPos.position + new Vector3(-0.2f, 0, 0);
            bullet_2.SetBullet(dmg, bulletSpd, shotPos.forward);
        }
        else
        {
            Bullet bullet = Instantiate(bullets[0]);
            bullet.transform.position = shotPos.position;
            bullet.SetBullet(dmg, bulletSpd, shotPos.forward);
        }


        if (PlayerData.Instance.PlayerSkill[2] > 0)
        {
            Bullet bullet_1 = Instantiate(bullets[0]);
            bullet_1.transform.position = shotPos.position;
            bullet_1.SetBullet(dmg, bulletSpd, leftPos.forward);

            Bullet bullet_2 = Instantiate(bullets[0]);
            bullet_2.transform.position = shotPos.position;
            bullet_2.SetBullet(dmg, bulletSpd, rightPos.forward);
        }
       
    }
    private void MultiShot()
    {
        BulletSet();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemys.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemys.Remove(collision.gameObject);
        }
    }
    public void ResetState()
    {
        dmg = 1;
        shotSpd = 0.2f;
        bulletSpd = 5;
        if (Enemys.Count >= 1)
        {
            Enemys.Clear();
        }
        for (int i = 0; i < PlayerData.Instance.PlayerSkill.Count; i++)
        {
            PlayerData.Instance.PlayerSkill[i] = 0;
        }
    }
}
